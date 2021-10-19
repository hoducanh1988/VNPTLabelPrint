using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Automation {

    public class MQTT {
        string brokerAddress;
        int brokerPort;
        string userName;
        string passWord;
        public string dataReceived;
        string topic;
        MqttClient mqttClient;

        public MQTT(string _brokerAddress, int _brokerPort, string _userName, string _passWord, string _topic) {
            this.brokerAddress = _brokerAddress;
            this.brokerPort = _brokerPort;
            this.userName = _userName;
            this.passWord = _passWord;
            this.topic = _topic;
            dataReceived = "";
        }
        public bool Connect() {
            try {
                mqttClient = new MqttClient(this.brokerAddress, this.brokerPort, false, null, null, MqttSslProtocols.None);
                mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
                string a = Guid.NewGuid().ToString();
                mqttClient.Connect(Guid.NewGuid().ToString(), this.userName, this.passWord);
                return true;
            }
            catch (Exception EX) {
                string a = EX.ToString();
                return false;
            }
        }
        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) {
            dataReceived += Encoding.UTF8.GetString(e.Message);
        }
        public bool Subcribe() {
            try {
                if (mqttClient == null) return false;
                mqttClient.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                return true;
            }
            catch {
                return false;
            }
        }
        public bool UnSubcribe() {
            try {
                if (mqttClient == null) return false;
                mqttClient.Unsubscribe(new string[] { topic });
                return true;
            }
            catch {
                return false;
            }
        }
        public bool Publish(string message) {
            try {
                if (mqttClient == null) return false;
                mqttClient.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
                return true;
            }
            catch {
                return false;
            }
        }
        public void ResetData() { dataReceived = ""; }
        public bool Disconnect() {
            try {
                if (mqttClient == null) return true;
                mqttClient.Disconnect();
                return true;
            }
            catch {
                return false;
            }
        }
    }
}
