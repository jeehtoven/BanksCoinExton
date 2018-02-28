using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BanksCoinExton
{
    public class GeneralLedger
    {
        public void JournalEntry(string sender, string recipient, int amount)
        {
            string timestamp = DateTime.Now.ToString();
            string fileName = DateTime.Today.ToString("d").Replace('/', '-');
            string path = @"C:\BanksCoin\gl\gl_" + fileName + ".txt";
            string usedHashPath = @"C:\BanksCoin\gl\usedHash\usedHash.txt";
            string checkHash;
            int count = 0;
            while ((checkHash = checkUsedHash()) != String.Empty && count != 1)
            {
                File.AppendAllText(path, timestamp + ";" + sender + ";" + recipient + ";" + amount + ";" + checkHash + Environment.NewLine);
                File.AppendAllText(usedHashPath, checkHash + Environment.NewLine);
                count++;
            }

            if (checkHash != String.Empty) MessageBox.Show("BanksCoin" + Environment.NewLine + "----------" + Environment.NewLine + "Time of Transaction: " + timestamp + Environment.NewLine + "Sender: " + sender + Environment.NewLine + "Recipient: " + recipient + Environment.NewLine + "Amount: " + amount + Environment.NewLine + "Hash: " + checkHash + Environment.NewLine + "Have a good day!");
            else
            {
                checkHash = checkUsedHash();
                MessageBox.Show("BanksCoin" + Environment.NewLine + "----------" + Environment.NewLine + "Time of Transaction: " + timestamp + Environment.NewLine + "Sender: " + sender + Environment.NewLine + "Recipient: " + recipient + Environment.NewLine + "Amount: " + amount + Environment.NewLine + "Hash: " + checkHash + Environment.NewLine + "Have a good day!");
            }
        }

        public string checkUsedHash()
        {
            //Get list of files
            string[] filePaths = Directory.GetFiles(@"C:\BanksCoin\hash");
            string usedHashPath = @"C:\BanksCoin\gl\usedHash\usedHash.txt";

            //int counter = 0;
            string line;
            string lineBack = String.Empty;
            bool flag = false;

            //if (!File.Exists(usedHashPath))
            //{
            //Directory.CreateDirectory(@"C:\BanksCoin\gl\usedHash");
            //}
            //System.IO.StreamReader hashFile = new System.IO.StreamReader(usedHashPath);
            //string hashLine;

            foreach (string filePathName in filePaths)
            {
                var query = from line1 in File.ReadLines(filePathName)
                            join line2 in File.ReadLines(usedHashPath)
                            on line1 equals line2
                            select line1;

                var commonLines = query.ToList();

                System.IO.StreamReader file = new System.IO.StreamReader(filePathName);

                if (commonLines.Count == 0)
                {
                    lineBack = file.ReadLine();
                    flag = true;
                }

                else
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        foreach (string line2 in commonLines)
                        {
                            if (line2 != line) return line2;
                        }
                    }
                }

                file.Close();

            }



            //hashFile.Close();
            if (flag == true) return lineBack;
            else return String.Empty;
        }
    }
}
