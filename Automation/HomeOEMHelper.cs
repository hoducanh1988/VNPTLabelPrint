using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Automation {
    public class HomeOEMHelper {

        MQTT mqttSubcribe;
        MQTT mqttPublish;

        public HomeOEMHelper(string _brokerAddress, string _brokerPort, string _userName, string _passWord, string eui64ID) {
            mqttSubcribe = new MQTT(_brokerAddress, int.Parse(_brokerPort), _userName, _passWord, $"gw/{eui64ID}/devicejoined");
            mqttPublish = new MQTT(_brokerAddress, int.Parse(_brokerPort), _userName, _passWord, $"gw/{eui64ID}/commands");
        }

        public string GetEUI64ID(string Timout) {
            int count = 0;
            string ret = "";

            try {
                while (count < int.Parse(Timout)) {
                    if (mqttSubcribe.dataReceived.Contains("\"eui64\"")) {
                        try {
                            foreach (var tmp in mqttSubcribe.dataReceived.Split('{')) {
                                if (tmp.Contains("eui64")) {
                                    ret = tmp.Split(',')[0].Split(':')[1].Replace("\"", "").Replace("\r", "").Replace("\n", "").Trim();
                                    ret = ret.ToUpper().Split(new string[] { "0X" }, StringSplitOptions.None)[1];
                                }
                            }
                            break;
                        }
                        catch {
                            break;
                        }
                    }
                    Thread.Sleep(1000);
                    count++;
                }
            }
            catch { }
            return ret;
        }


        public bool Connect(out string error) {
            bool ret = false;
            error = "";
            try {
                //------------Connect mqttSubcribe --------------//
                if (!mqttSubcribe.Connect()) { error = "Fail Connect to Brocker"; goto END; }
                if (!mqttPublish.Connect()) { error = "Fail Connect to Brocker"; goto END; }
                //------------ Subcribe--------------------------//
                if (!mqttSubcribe.Subcribe()) { error = "Fail Subcribe to Brocker"; goto END; }
                if (!mqttPublish.Subcribe()) { error = "Fail Subcribe to Brocker"; goto END; }
                //-------------void EnableGatewayZigbee-------------------------------------
                EnableGatewayZigbee();
                ret = true;
            }
            catch { ret = false; }

        END:
            return ret;
        }


        private void EnableGatewayZigbee() {
            try {
                string message = "{\"commands\":[{\"command\":\"plugin network-creator-security open-network\",\"postDelayMs\":100}]}";
                mqttPublish.Publish(message);
            }
            catch { return; }
        }


        private void DisableGatewayZigbee() {
            try {
                string message = "{\"commands\":[{\"command\":\"plugin network-creator-security close-network\",\"postDelayMs\":100}]}";
                mqttPublish.Publish(message);
            }
            catch { return; }
        }


        public void Disconnect() {
            try {
                DisableGatewayZigbee();
                mqttPublish.UnSubcribe();
                mqttSubcribe.UnSubcribe();
                //-----------------------
                mqttSubcribe.ResetData();
                mqttSubcribe.Disconnect();
                //------------------------
                mqttPublish.Disconnect();
                mqttPublish.ResetData();
            }
            catch { return; }

        }

    }
}
