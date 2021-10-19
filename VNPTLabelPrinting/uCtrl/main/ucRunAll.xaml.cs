using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using VNPTLabelPrinting.Function.Custom;
using VNPTLabelPrinting.Function.Global;
using VNPTLabelPrinting.sWndw;
using System.Windows.Threading;

namespace VNPTLabelPrinting.uCtrl {
    /// <summary>
    /// Interaction logic for ucRunAll.xaml
    /// </summary>
    public partial class ucRunAll : UserControl {
        //Khai bao bien o day .....................//
        List<string> list_func = new List<string>();
        CSharpCodeProvider provider;
        CompilerParameters parameters;
        Assembly assembly;
        string[] setting;
        string code = "", name_space = "", name_class = "";
        volatile bool isReady = false;
        volatile bool isRunning = false;
        DispatcherTimer timer_focus;
        //...


        public ucRunAll() {
            InitializeComponent();
            myGlobal.myTesting.Clear();
            this.DataContext = myGlobal.myTesting;
            myGlobal.datagridResult.Clear();
            this.dg_result.ItemsSource = myGlobal.datagridResult;
            this.loadCodeFromTextFile();
            this.provider = new CSharpCodeProvider();
            this.parameters = new CompilerParameters();
            this.cbb_ischeck.ItemsSource = new List<string>() { "Yes", "No" };
            this.loadSettingFile();

            getListBarcode(out myGlobal.listBarcode);
            addInputBarcode();

            timer_focus = new DispatcherTimer();
            timer_focus.Interval = TimeSpan.FromMilliseconds(myGlobal.myTesting.Interval);
            timer_focus.Tick += (s, e) => { 
                timer_focus_ticker();
                timer_focus.Interval = TimeSpan.FromMilliseconds(myGlobal.myTesting.Interval);
            };
            timer_focus.Start();
        }

        #region sub function

        private void timer_focus_ticker() {
            isReady = true;
            if (myGlobal.isPause == false) {
                foreach (UIElement child in this.inputBarcodeViewer.Children) {
                    if (child is ucItemInputBarcode) {
                        //set focus
                        if ((child as ucItemInputBarcode).data_cxt.isInputted == false) {
                            (child as ucItemInputBarcode).txtContent.Focus();
                            isReady = false;
                            break;
                        }
                    }
                    if (child is ucItemInputQR) {
                        //set focus
                        if ((child as ucItemInputQR).data_cxt.isInputted == false) {
                            (child as ucItemInputQR).txtContent.Focus();
                            isReady = false;
                            break;
                        }
                    }
                    if (child is ucItemInputListBarcode) {
                        //set focus
                        if ((child as ucItemInputListBarcode).data_cxt.isInputted == false) {
                            (child as ucItemInputListBarcode).txtContent.Focus();
                            isReady = false;
                            break;
                        }
                    }
                }
            }
            else {
                isReady = false;
            }

            //check
            if (isReady == true && isRunning == false) {
                Thread t = new Thread(new ThreadStart(() => {
                    isRunning = true;
                    startPrintLabel();

                    //reset display
                    Dispatcher.Invoke(new Action(() => {
                        getListBarcode(out myGlobal.listBarcode);
                        addInputBarcode();
                        isReady = false;
                        isRunning = false;
                    }));
                }));
                t.IsBackground = true;
                t.Start();
            }
        }

        private void startPrintLabel() {
            Stopwatch st = new Stopwatch();
            st.Start();

            Dispatcher.Invoke(new Action(() => { foreach (var item in myGlobal.datagridResult) item.itemInput = item.itemOutput = item.itemResult = ""; }));
            myGlobal.myTesting.waitParam();
            Dispatcher.Invoke(new Action(() => { addInstruction(); }));

            bool r = false, total_result = true;

            //load setting file
            r = loadSettingFile(); if (!r) goto END;
            //add library dll file
            r = addLibrary(); if (!r) goto END;
            //compiler source code
            r = compilerSourceCode(); if (!r) goto END;
            //run test
            runAll(ref total_result);

        END:
            st.Stop();
            myGlobal.myTesting.logSystem += string.Format("-----------------------------------------------------------\n");
            myGlobal.myTesting.logSystem += $"> total result: { string.Format("{0}", total_result ? "Passed" : "Failed") }\n";
            myGlobal.myTesting.logSystem += $"> total time: {st.ElapsedMilliseconds} ms\n";

            //show total result
            bool ___ = total_result ? myGlobal.myTesting.passParam() : myGlobal.myTesting.failParam();

            //show input barcode
            foreach (var item in myGlobal.listBarcode) {
                myGlobal.myTesting.inputBarcode += $"{item.Name.ToUpper().Replace("_", "").Replace("BARCODE", "").Trim()} : {item.Content}\n";
            }

            //save log detail
            saveLogDetail(total_result);
        }

        private void loadCodeFromTextFile() {
            //Automation.Encryption encryption = new Automation.Encryption(myGlobal.myTesting.fileProduct);
            //code = encryption.readSource();
            code = File.ReadAllText(myGlobal.myTesting.fileProduct);
            string[] buffer = code.Split('\n');

            //read station
            myGlobal.myTesting.productName = buffer.Where(x => x.ToLower().Contains("product=")).FirstOrDefault().Split('=')[1].Trim();
            myGlobal.myTesting.Station = buffer.Where(x => x.ToLower().Contains("station=")).FirstOrDefault().Split('=')[1].Trim();

            //read app title
            string version = buffer.Where(x => x.ToLower().Contains("version=")).FirstOrDefault().Split('=')[1].Trim();
            string buildtime = buffer.Where(x => x.ToLower().Contains("buildtime=")).FirstOrDefault().Split('=')[1].Trim();
            string copyright = buffer.Where(x => x.ToLower().Contains("copyright=")).FirstOrDefault().Split('=')[1].Trim();
            myGlobal.myTesting.appTitle = $"Version: {version} - Build time: {buildtime} - {copyright}";

            //read namespace and class
            name_space = buffer.Where(x => x.ToLower().Contains("namespace")).FirstOrDefault().Replace("namespace", "").Replace("{", "").Replace("\r", "").Trim();
            name_class = buffer.Where(x => x.ToLower().Contains("class")).FirstOrDefault().Replace("class", "").Replace("public", "").Replace("{", "").Replace("\r", "").Trim();

            //read public function
            int i = 0;
            foreach (var x in buffer) {
                if (x.Contains("public bool") && x.Contains("(")) {
                    string s = x.Replace("{", "").Replace("public bool", "").Trim();
                    string ck = buffer[i - 1].Replace("//", "").Replace("[", "").Replace("]", "").Replace("\r", "").Trim();
                    list_func.Add(s);
                    Dispatcher.Invoke(new Action(() => { myGlobal.datagridResult.Add(new ResultInfo() { itemName = s.Split('(')[0].Trim(), isCheck = ck, itemResult = "", itemInput = "", itemOutput = "" }); }));
                }
                i++;
            }
        }

        private bool loadSettingFile() {
            try {
                //show info to log
                myGlobal.myTesting.logSystem += string.Format("> App info: {0}\n", myGlobal.myTesting.appTitle);
                myGlobal.myTesting.logSystem += string.Format("> product name: {0}\n", myGlobal.myTesting.productName);
                myGlobal.myTesting.logSystem += string.Format("> product station: {0}\n", myGlobal.myTesting.Station);
                myGlobal.myTesting.logSystem += string.Format("-----------------------------------------------------------\n");

                //load setting
                setting = File.ReadAllLines(myGlobal.myTesting.fileSetting);

                //show setting to log
                foreach (var line in setting) myGlobal.myTesting.logSystem += string.Format("> {0}\n", line);
                myGlobal.myTesting.logSystem += string.Format("-----------------------------------------------------------\n");

                return true;
            }
            catch { return false; }
        }

        private bool addLibrary() {
            try {
                // Reference to System.Drawing library
                parameters.ReferencedAssemblies.Add("System.Drawing.dll");
                parameters.ReferencedAssemblies.Add("System.Core.dll");
                parameters.ReferencedAssemblies.Add("System.dll");
                parameters.ReferencedAssemblies.Add("Automation.dll");
                return true;
            }
            catch { return false; }
        }

        private bool compilerSourceCode() {
            try {
                // True - memory generation, false - external file generation
                parameters.GenerateInMemory = true;
                parameters.GenerateExecutable = true;
                CompilerResults results = provider.CompileAssemblyFromSource(parameters, this.code);

                if (results.Errors.HasErrors) {
                    StringBuilder sb = new StringBuilder();
                    foreach (CompilerError error in results.Errors) {
                        sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
                    }
                    throw new Exception(sb.ToString());
                }

                assembly = results.CompiledAssembly;
                return true;
            }
            catch (Exception ex) { 
                MessageBox.Show(ex.ToString(), "Error Compiler", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void getListBarcode(out List<ItemInputBarcode> items) {
            items = new List<ItemInputBarcode>();
            int i = 0;
            foreach (var f in list_func) {
                if (myGlobal.datagridResult[i].isCheck.ToLower().Equals("yes")) {
                    string f_para_name = f.Split('(')[1].Split(')')[0].Trim();
                    if (string.IsNullOrEmpty(f_para_name) == false) {
                        string[] bff = f_para_name.Split(',');
                        foreach (var bf in bff) {
                            string[] ss = bf.Split(' ');
                            string s = ss[ss.Length - 1];

                            if (s.Contains("barcode")) {
                                if (items.Count == 0) {
                                    ItemInputBarcode item = new ItemInputBarcode() { Name = s, Content = "" };
                                    items.Add(item);
                                }
                                else {
                                    var oit = items.Where(x => x.Name.ToLower().Equals(s.ToLower())).ToList();
                                    if (oit != null && oit.Count > 0) {
                                        //nothing
                                    }
                                    else {
                                        ItemInputBarcode item = new ItemInputBarcode() { Name = s, Content = "" };
                                        items.Add(item);
                                    }
                                }
                            }
                        }
                    }
                }
                i++;
            }
        }

        private string getSettingValue (string value_name) {
            string setting_str = setting.Where(x => x.ToLower().Contains(value_name)).FirstOrDefault();
            string setting_title = setting_str.Split('=')[0];
            string setting_value = setting_str.Replace($"{setting_title}=", "");
            return setting_value;
        }

        private void addInputBarcode() {
            this.inputBarcodeViewer.Children.Clear();
            int h = 0;
            foreach (var item in myGlobal.listBarcode) {
                if (item.Name.ToLower().Contains("2d")) h += 100;
                else if (item.Name.ToLower().Contains("list")) h += 220;
                else h += 52;
            }
            this.tabResultViewer.Margin = new Thickness(0, h, 0, 0);
            foreach (var item in myGlobal.listBarcode) {
                if (item.Name.ToLower().Contains("2d")) {
                    ucItemInputQR ucq = new ucItemInputQR(item);
                    this.inputBarcodeViewer.Children.Add(ucq);
                }
                else if (item.Name.ToLower().Contains("list")) { //list serial number
                    string qty = getSettingValue("quantity");
                    string pd = getSettingValue("setting_product_number");
                    string fac = getSettingValue("setting_factory");
                    string yr = getSettingValue("setting_production_year");
                    string hw = getSettingValue("setting_hardware_version");
                    int d = int.Parse(yr) - 13;
                    if (d < 10) yr = d.ToString();
                    if (d >= 10) yr = Convert.ToChar(d + 55).ToString();
                    string value_format = $"^{pd}{fac}{yr}[0-5][0-9]{hw}F[0-9,A-F][0-9,A-F][0-9,A-F][0-9,A-F][0-9,A-F][0-9,A-F]$";
                    string db_file = AppDomain.CurrentDomain.BaseDirectory + "DB\\" + getSettingValue("setting_db_file");
                    ucItemInputListBarcode ucl = new ucItemInputListBarcode(item, qty, value_format, db_file);
                    this.inputBarcodeViewer.Children.Add(ucl);
                }
                else {
                    ucItemInputBarcode uci = new ucItemInputBarcode(item);
                    this.inputBarcodeViewer.Children.Add(uci);
                }
            }
        }

        private void addInstruction() {
            string p = myGlobal.myTesting.productName.ToLower();
            string s = myGlobal.myTesting.Station.ToLower();
            if (s.Contains("tem đáy") && (p.Contains("smoke") || p.Contains("door") || p.Contains("motion") || p.Contains("temp"))) {
                this.tabResultViewer.Margin = new Thickness(0, 65 * myGlobal.listBarcode.Count + 40, 0, 0);
                ucInstruction uci = new ucInstruction();
                this.inputBarcodeViewer.Children.Add(uci);

                

            }
        }

        private bool runAll(ref bool total_result) {
            try {
                //
                Type program = assembly.GetType($"{name_space}.{name_class}");
                object classInstance = Activator.CreateInstance(program, null);
                PropertyInfo logsystem = program.GetProperty("logSystem");
                MethodInfo method = null;

                //test
                int i = 0;
                foreach (var func in list_func) {
                    if (myGlobal.datagridResult[i].isCheck.ToLower().Equals("yes")) {
                        Dispatcher.Invoke(new Action(() => { myGlobal.datagridResult[i].itemResult = "Waiting..."; }));

                        string function_name = func.Split('(')[0].Trim();
                        string function_parameter_name = func.Split('(')[1].Split(')')[0].Trim();
                        object[] function_parameter_value = null;

                        if (string.IsNullOrEmpty(function_parameter_name) == false) {
                            var buffer = function_parameter_name.Split(' ').Where(x => x.Contains("_")).ToArray();
                            function_parameter_value = new object[buffer.Length];

                            for (int k = 0; k < buffer.Length; k++) {
                                string s = buffer[k].Replace(",", "");
                                if (s.Contains("barcode")) {
                                    var item = myGlobal.listBarcode.Where(x => x.Name.Equals(s)).FirstOrDefault();
                                    function_parameter_value[k] = item.Content;
                                }
                                else if (s.Contains("actual")) {
                                    function_parameter_value[k] = new object() as string;
                                }
                                else {
                                    string setting_str = setting.Where(x => x.ToLower().Contains(s.ToLower())).FirstOrDefault();
                                    string setting_title = setting_str.Split('=')[0];
                                    string setting_value = setting_str.Replace($"{setting_title}=", "");
                                    function_parameter_value[k] = setting_value;
                                }
                            }

                            string std = "";
                            foreach (var f in function_parameter_value) {
                                std += string.Format("{0},", f);
                            }
                            Dispatcher.Invoke(new Action(() => { myGlobal.datagridResult[i].itemInput = std.Length >= 2 ? std.Substring(0, std.Length - 2) : std.Substring(0, std.Length - 1); }));
                        }

                        //check other function
                        method = program.GetMethod(function_name);
                        bool r = (bool)method.Invoke(classInstance, function_parameter_value);
                        myGlobal.myTesting.logSystem += (string)logsystem.GetValue(classInstance, null);

                        var barcode_dummy = myGlobal.listBarcode.Where(x => x.Name.Contains("barcode_dummy")).FirstOrDefault();
                        if (barcode_dummy != null) barcode_dummy.Content = function_parameter_value[function_parameter_value.Length - 1] as string;

                        Dispatcher.Invoke(new Action(() => {
                            myGlobal.datagridResult[i].itemOutput = function_parameter_value[function_parameter_value.Length - 1] is string ? (string)function_parameter_value[function_parameter_value.Length - 1] : "";
                            myGlobal.datagridResult[i].itemResult = r ? "Passed" : "Failed";
                        }));

                        if (!r) total_result = false;

                    }
                    i++;
                }

                return true;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); return false; }
        }

        private bool saveLogDetail(bool total_result) {
            string dir = $"{AppDomain.CurrentDomain.BaseDirectory}LogDetail\\{myGlobal.myTesting.productName.Replace(" ", "")}\\{myGlobal.myTesting.Station.Replace(" ", "").Trim() }\\{DateTime.Now.ToString("yyyy-MM-dd")}";
            if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);

            string f = "";
            foreach (var item in myGlobal.listBarcode) {
                if (item.Content.ToLower().Contains("eui") && item.Content.ToLower().Contains("sn")) f += item.Content.ToLower().Split(new string[] { "sn" }, StringSplitOptions.None)[1].Replace(":", "").Replace("\n", "").Replace("\r", "").Trim().ToUpper() + "_";
                else f += item.Content.Replace("-", "").Replace(" ", "").Trim() + "_";
            }

            f = $"{f}{DateTime.Now.ToString("HHmmss")}_{string.Format("{0}", total_result ? "Passed" : "Failed")}.txt";

            using (StreamWriter sw = new StreamWriter($"{dir}\\{f}", true, Encoding.Unicode)) {
                sw.WriteLine(myGlobal.myTesting.logSystem);
            }
            return true;
        }

        #endregion

    }
}
