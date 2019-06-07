using OCR.TesseractWrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
namespace MultiRename
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string d = textBox_Path.Text;
            if (!Directory.Exists(d))
            {
                MessageBox.Show("目录" + d + "不存在");
                return;
            }
            if (!d.EndsWith("\\")) d += "\\";
            string d1 = d + "new\\";
            if (!Directory.Exists(d1))
                Directory.CreateDirectory(d1);
            Regex reg = new Regex(@"\d+\.\d\d", RegexOptions.Compiled);
            foreach (string f in Directory.GetFiles(textBox_Path.Text))
            {
                if (!f.ToLower().EndsWith(".png") && !f.ToLower().EndsWith(".jpg") && !f.ToLower().EndsWith(".bmp")) continue;
                Bitmap bmp = new Bitmap(f);
                TesseractProcessor process = new TesseractProcessor();
                process.SetPageSegMode(ePageSegMode.PSM_SINGLE_LINE);
                process.Init(System.Environment.CurrentDirectory + "\\", "chi_sim", (int)eOcrEngineMode.OEM_DEFAULT);
                try
                {
                    string result = process.Recognize(bmp);
                    Match m = reg.Match(result);
                    if (m.Success)
                    {
                        string amount = m.ToString();
                        File.Copy(f, d1 + amount + Path.GetExtension(f), true);
                    }
                    else
                    {
                        File.Copy(f, d1 + Path.GetFileName(f), true);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("识别文件出错:" + f);
                    File.Copy(f, d1 + Path.GetFileName(f), true);
                }
                bmp.Dispose();
            }
            System.Diagnostics.Process.Start(d1);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox_Path.Text = System.IO.Path.GetDirectoryName(ofd.FileName);
            }
        }
    }
}
