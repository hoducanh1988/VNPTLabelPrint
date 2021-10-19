using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Automation {

    public class Encryption {
        // Hold information related to the key and vector used during encryption/decryption
        private byte[] Key;
        private byte[] Vector;
        private string fileName;
        private Rijndael RijndaelAlg;
        private static readonly string Password = "VnPtT3cHnol0Gy";
        private static readonly byte[] Salt = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };

        // Constructor
        public Encryption(string file) {
            RijndaelAlg = Rijndael.Create();

            // Save the Key and Initialization Vector locally
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(Password, Salt);
            Key = pdb.GetBytes(32);
            Vector = pdb.GetBytes(16);

            fileName = file;
        }

        // Read the saved file source and return its string representation of the data.
        public string readSource() {
            string result = "";

            if (File.Exists(fileName)) {

                FileStream fStream = new FileStream(fileName, FileMode.Open);
                CryptoStream decryptStream = new CryptoStream(fStream, RijndaelAlg.CreateDecryptor(Key, Vector), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(decryptStream);


                try {
                    result = reader.ReadToEnd();
                }
                catch (Exception decryptEx) {
                    throw new Exception("Error decrypting source file. Error: " + decryptEx.Message);
                }
                finally {
                    reader.Close();
                    decryptStream.Close();
                    fStream.Close();
                }
            }
            else {
                throw new Exception("The source file could not be located. File name: " + fileName);
            }

            return result;
        }


        // Encrypt data and save it into the specified file of this class. 
        // During the process we also save the key and vector used for later decryption.
        public void saveSource(string data) {
            try {
                FileStream fStream = new FileStream(fileName, FileMode.OpenOrCreate);
                CryptoStream cryptStream = new CryptoStream(fStream, RijndaelAlg.CreateEncryptor(Key, Vector), CryptoStreamMode.Write);
                StreamWriter writer = new StreamWriter(cryptStream);

                try {
                    writer.Write(data);
                }
                catch (Exception streamException) {
                    throw new Exception("Error saving the data to file. Error: " + streamException.Message);
                }
                finally {
                    writer.Close();
                    cryptStream.Close();
                    fStream.Close();
                }
            }
            catch (Exception ex) {
                throw new Exception("There was a problem saving the data to file. Error: " + ex.Message);
            }
        }
    }

}
