using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBKey
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        public byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        RijndaelManaged myRijndael = new RijndaelManaged();
        string todaysz = "";

        string getbios()
        {
            string serialNumber = string.Empty;
            ManagementObjectSearcher MOS = new ManagementObjectSearcher(" Select * From Win32_BIOS ");
            foreach (ManagementObject getserial in MOS.Get())
            {
                serialNumber = getserial["SerialNumber"].ToString();
            }
            return serialNumber;
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            //if (File.Exists(Application.StartupPath + @"\license.lic") || Properties.Settings.Default.time != "0")
            //{
            //    this.Hide();

            //}
            //else
            //{
                todaysz = Convert.ToString(GetFastestNISTDate());
                backgroundWorker1.RunWorkerAsync();
            //}
           
        }

        private void Btn_genkey_Click(object sender, EventArgs e)
        {
            if(txt_hwid.Text != "" && txt_day.Text != "")
            {
                string gethwid = txt_hwid.Text;
                string getday = txt_day.Text;
                myRijndael.Key = Encoding.Unicode.GetBytes(gethwid);
                myRijndael.IV = IV;
                var date = DateTime.Now;
                DateTime newDate = date.AddDays(Convert.ToDouble(getday));
                string result = RandomString(5) + ":" + gethwid + ":" + newDate.ToShortDateString() + ":" + RandomString(5);
                byte[] encrypted = EncryptStringToBytes(result, myRijndael.Key, myRijndael.IV);
                //txt_key.Text = String.Join("", Array.ConvertAll(encrypted, byteValue => byteValue.ToString()));
                txt_key.Text = Convert.ToBase64String(encrypted);
            }
        }

        private void Txt_day_TextChanged(object sender, EventArgs e)
        {
            int myInt;
            bool isNumerical = int.TryParse(txt_day.Text, out myInt);
            if (isNumerical != true || myInt > 9999999)
            {
                MessageBox.Show("This field can enter only number!","NBKey", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_day.Text = "";
                btn_genkey.Enabled = false;
            }
            else
            {
                btn_genkey.Enabled = true;
            }
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;

        }

        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            string plaintext = null;
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

        private void Btn_checkkey_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] encrypted = Convert.FromBase64String(txt_key.Text);
                string roundtrip = DecryptStringFromBytes(encrypted, Encoding.Unicode.GetBytes(txt_hwid.Text), IV);
                string[] authorsList = roundtrip.Split(':');
                DateTime today = Convert.ToDateTime(todaysz).Date;
                DateTime d2 = Convert.ToDateTime(authorsList[2]);
                string hwid = Convert.ToString(authorsList[1]);
                double day = (d2 - today).TotalDays;
                if (day < 0)
                {
                    MessageBox.Show("Your key has expired "+ day, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (authorsList[1] == getbios())
                    {
                        //MessageBox.Show("Name: "+ Environment.MachineName + "\r\nHwid: " + hwid + " \r\nExpir date: " + Convert.ToString(authorsList[2]) /*"\r\nDays left: " + day.ToString()*/, "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //string createText = "Hello and Welcome" + Environment.NewLine;
                        if (Properties.Settings.Default.time.ToString()!= "0")
                        {
                            if(!File.Exists(Application.StartupPath + @"\license.lic"))
                            {
                                File.WriteAllText(Application.StartupPath + @"\license.lic", Properties.Settings.Default.time);
                                Dashboard ds = new Dashboard();
                                ds.Show();
                                this.Hide();
                            }
                            else
                            {
                                Dashboard ds = new Dashboard();
                                ds.Show();
                                this.Hide();
                            }
                            
                        }
                        else
                        {
                            File.WriteAllText(Application.StartupPath + @"\license.lic", Global.Encrypt(Convert.ToString(day * 24 * 60 * 60)));
                            Properties.Settings.Default.time = Global.Encrypt(Convert.ToString(day * 24 * 60 * 60));
                            Properties.Settings.Default.Save();
                            Dashboard ds = new Dashboard();
                            ds.Show();
                            this.Hide();
                        }
                        
                       
                    }
                    else
                    {
                        MessageBox.Show("Your hardware Id not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public static DateTime GetFastestNISTDate()
        {
            var result = DateTime.MinValue;
            // Initialize the list of NIST time servers
            // http://tf.nist.gov/tf-cgi/servers.cgi
            string[] servers = new string[] {
"nist1-ny.ustiming.org",
"nist1-nj.ustiming.org",
"nist1-pa.ustiming.org",
"time-a.nist.gov",
"time-b.nist.gov",
"nist1.aol-va.symmetricom.com",
"nist1.columbiacountyga.gov",
"nist1-chi.ustiming.org",
"nist.expertsmi.com",
"nist.netservicesgroup.com"
};

            // Try 5 servers in random order to spread the load
            Random rnd = new Random();
            foreach (string server in servers.OrderBy(s => rnd.NextDouble()).Take(5))
            {
                try
                {
                    // Connect to the server (at port 13) and get the response
                    string serverResponse = string.Empty;
                    using (var reader = new StreamReader(new System.Net.Sockets.TcpClient(server, 13).GetStream()))
                    {
                        serverResponse = reader.ReadToEnd();
                    }

                    // If a response was received
                    if (!string.IsNullOrEmpty(serverResponse))
                    {
                        // Split the response string ("55596 11-02-14 13:54:11 00 0 0 478.1 UTC(NIST) *")
                        string[] tokens = serverResponse.Split(' ');

                        // Check the number of tokens
                        if (tokens.Length >= 6)
                        {
                            // Check the health status
                            string health = tokens[5];
                            if (health == "0")
                            {
                                // Get date and time parts from the server response
                                string[] dateParts = tokens[1].Split('-');
                                string[] timeParts = tokens[2].Split(':');

                                // Create a DateTime instance
                                DateTime utcDateTime = new DateTime(
                                    Convert.ToInt32(dateParts[0]) + 2000,
                                    Convert.ToInt32(dateParts[1]), Convert.ToInt32(dateParts[2]),
                                    Convert.ToInt32(timeParts[0]), Convert.ToInt32(timeParts[1]),
                                    Convert.ToInt32(timeParts[2]));

                                // Convert received (UTC) DateTime value to the local timezone
                                result = utcDateTime.ToLocalTime();

                                return result;
                                // Response successfully received; exit the loop

                            }
                        }

                    }

                }
                catch
                {
                    // Ignore exception and try the next server
                }
            }
            return result;
        }
        
        public bool checkinternet()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!backgroundWorker1.CancellationPending)
            {
                
                if (!checkinternet())
                {
                    MessageBox.Show("Can not connect to server, \r\nPlease check your internet connection!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Exit();
                }
                Thread.Sleep(100000);
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            txt_hwid.Text = getbios();
            //if(File.Exists(Application.StartupPath +@"\license.lic") || Properties.Settings.Default.time != "0")
            //{
            //    Dashboard ds = new Dashboard();
            //    ds.Show();
                
            //}
        }
    }
}
