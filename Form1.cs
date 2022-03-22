using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace myXmlParser
{
    public partial class Form1 : Form
    {
        double tempSumm = 0;
        string[] files = new string[10000];

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            files = new string[10000];
            tempSumm = 0;

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    files = Directory.GetFiles(fbd.SelectedPath);

                    System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                }
            }

            foreach (var item in files)
            {
                tempSumm += Checker(item);
            }
            tempSumm = Math.Round(tempSumm, 2, MidpointRounding.AwayFromZero);
            richTextBox2.Text = tempSumm.ToString();

        }
        public double Checker(string filePath)
        {
            XmlDocument A = new XmlDocument();
            A.Load(filePath);
            double tempSum = 0;
            XmlNodeList nodeList = A.GetElementsByTagName("ALLCOST");
            foreach (XmlElement element in nodeList)
            {
                string temp = element.InnerText;
                tempSum += Convert.ToDouble(temp.Replace('.', ','));
            }
            return tempSum;
        }
    }

    
}
