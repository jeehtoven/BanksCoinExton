using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BanksCoinExton
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public void GetRandomHexNumber()
        {
            Random random = new Random();
            int digits = 64;
            int count = 0;
            string fileName = DateTime.Today.ToString("d").Replace('/', '-');
            string path = @"C:\BanksCoin\hash\hash_" + fileName + ".txt";
            string elseResult = String.Empty;

            if (!Directory.Exists(@"C:\BanksCoin\hash")) Directory.CreateDirectory(@"C:\BanksCoin\hash");

            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
            {
                if (CheckFileForDuplicate(result) == String.Empty) File.AppendAllText(path, result + Environment.NewLine);
                //return result;
                else Console.WriteLine("Duplicate: " + result);
            }

            else
            {
                elseResult = result + random.Next(64).ToString("X");

                //MessageBox.Show("Result:" + result + random.Next(64).ToString("X"));
                if (CheckFileForDuplicate(result) == String.Empty) File.AppendAllText(path, elseResult + Environment.NewLine);
                else Console.WriteLine("Duplicate:" + elseResult);
            }

            //return result + random.Next(64).ToString("X");
            count++;
        }

        public string CheckFileForDuplicate(string result)
        {
            int counter = 0;
            string duplicate = String.Empty;
            string line;
            string path = @"C:\BanksCoin\hash";
            var txtFiles = Directory.EnumerateFiles(path, "*.txt");
            foreach (string currentFile in txtFiles)
            {
                // Read the file and display it line by line.  
                System.IO.StreamReader file =
                    new System.IO.StreamReader(currentFile);
                while ((line = file.ReadLine()) != null)
                {
                    if (line == result) duplicate = line;
                    else duplicate = String.Empty;
                    //System.Console.WriteLine(line);
                    counter++;
                }

                file.Close();
            }

            return duplicate;

        }
    }
}
