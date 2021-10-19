using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Access = Microsoft.Office.Interop.Access;
using UtilityPack.Protocol;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Automation {
    public class MicrosoftAccessHelper {

        public MSAccessDB accessDB = null;
        public string Access_FileFullName = "";

        public MicrosoftAccessHelper(string access_file_fullname) {
            Access_FileFullName = access_file_fullname;
            accessDB = new MSAccessDB(Access_FileFullName);
        }

        public bool insertDataRow<T>(string table_name, T t) {
            bool r = false;
            try {
                if (!accessDB.IsConnected) accessDB.OpenConnection();
                Thread.Sleep(100);
                if (!accessDB.IsConnected) return false;
                r = accessDB.InsertDataToTable<T>(t, table_name);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return r;
        }

        public bool insertDataRow<T>(string table_name, T t, string ignore_column_name) {
            bool r = false;
            try {
                if (!accessDB.IsConnected) accessDB.OpenConnection();
                Thread.Sleep(100);
                if (!accessDB.IsConnected) return false;
                r = accessDB.InsertDataToTable<T>(t, table_name, ignore_column_name);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return r;
        }

        public bool deleteAllRow(string table_name) {
            bool r = false;
            try {
                if (!accessDB.IsConnected) accessDB.OpenConnection();
                Thread.Sleep(100);
                if (!accessDB.IsConnected) return false;
                r = accessDB.QueryDeleteOrUpdate(string.Format("DELETE FROM {0}", table_name));
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return r;
        }

        public List<T> selectDataRow<T>(string table_name, int row_quantity, string orderby_Field) where T : class, new() {
            List<T> listValue = null;
            try {
                if (!accessDB.IsConnected) accessDB.OpenConnection();
                Thread.Sleep(100);
                if (!accessDB.IsConnected) return null;
                listValue = accessDB.QueryDataReturnListObject<T>(string.Format("SELECT TOP {0} * FROM {1} ORDER BY {2} DESC", row_quantity, table_name, orderby_Field));
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return listValue;
        }


        public T selectDataRow<T>(string table_name, string search_field, string search_value) where T : class, new() {
            T value = null;
            try {
                if (!accessDB.IsConnected) accessDB.OpenConnection();
                Thread.Sleep(100);
                if (!accessDB.IsConnected) return null;
                value = accessDB.QueryDataReturnObject<T>(string.Format("SELECT * FROM {0} WHERE {1}='{2}';", table_name, search_field, search_value));
            }
            catch {
                value = null;
            }

            return value;
        }


        public List<T> selectDistinctDataRow<T>(string table_name, int row_quantity, string selected_Field) where T : class, new() {
            List<T> listValue = null;
            try {
                if (!accessDB.IsConnected) accessDB.OpenConnection();
                Thread.Sleep(100);
                if (!accessDB.IsConnected) return null;

                listValue = accessDB.QueryDataReturnListObject<T>(string.Format("SELECT DISTINCT TOP {0} {1} FROM {2}", row_quantity, selected_Field, table_name));
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return listValue;
        }


        public bool QueryData(string query_string) {
            bool r = false;
            try {
                if (!accessDB.IsConnected) accessDB.OpenConnection();
                Thread.Sleep(100);
                if (!accessDB.IsConnected) return false;
                r = accessDB.QueryDeleteOrUpdate(query_string);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return r;
        }

        public bool ExportQuery(string tableName, string locationToExportTo) {
            try {
                //init access file
                Access.Application oAccess = null;

                // Start a new instance of Access for Automation:
                oAccess = new Access.Application();
                oAccess.Visible = false;

                // Open a database in exclusive mode:
                oAccess.OpenCurrentDatabase(Access_FileFullName);

                //transfer access data to excel file
                oAccess.DoCmd.TransferSpreadsheet(Access.AcDataTransferType.acExport, Access.AcSpreadSheetType.acSpreadsheetTypeExcel12, tableName, locationToExportTo, true);

                //close database
                oAccess.CloseCurrentDatabase();
                oAccess.Quit();

                Marshal.ReleaseComObject(oAccess);

                return true;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool Close() {
            if (accessDB != null && accessDB.IsConnected) accessDB.Close();
            return true;
        }



    }
}
