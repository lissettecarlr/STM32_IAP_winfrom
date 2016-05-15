using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STM32_IAP_winfrom
{
    public partial class Form1 : Form
    {
        Thread Thr_Rcv;
        int bagNumber;
        byte[] binchar = new byte[] { };
        public Form1()
        {
            InitializeComponent();
            bagNumber = 0;
        }

        private void Button_inputFile_Click(object sender, EventArgs e)
        {
            string Mytext = "";
            int fileLen; //文件长度
            OpenFileDialog ProcedureFile = new OpenFileDialog();
            ProcedureFile.Filter = "All files (*.*) | *.*| Bin files(*.bin) | *.bin";  //选择格式
            ProcedureFile.RestoreDirectory = true; //控制对话框在关闭之前是否恢复当前目录 
            ProcedureFile.FilterIndex = 1; //在对话框中选择的文件筛选器的索引，如果选第一项就设为1

            if (ProcedureFile.ShowDialog() == DialogResult.OK)
            {
                string fName = ProcedureFile.FileName;  //读取文件的地址
                                                        //  File.Open(fName, FileMode.Append);
                                                        //   string data = File.ReadAllText(fName);
                                                        //    ProShow.Text = data;

                FileStream Myfile = new FileStream(fName, FileMode.Open, FileAccess.Read);
                BinaryReader binreader = new BinaryReader(Myfile);

                fileLen = (int)Myfile.Length;//获取bin文件长度
                if (fileLen % 1024 == 0)
                    bagNumber = fileLen / 1024;
                else
                    bagNumber = fileLen / 1024 + 1;

                label_fileSize.Text = fileLen.ToString(); //显示文件大小
                label_bogSum.Text = bagNumber.ToString();//显示包数
                binchar = binreader.ReadBytes(fileLen);

                 //foreach (byte j in binchar)
                 //{
                       
                 //}
                 binreader.Close();
            }
        }

        private void Button_inputDownload_Click(object sender, EventArgs e)
        {
            byte[] downLoad = new byte[9];
            downLoad[0] = 0xff;
            downLoad[1] = 0xEE;
            downLoad[2] = 0xDD;
            downLoad[3] = 0xBB;
            downLoad[4] = 0xAA;
            downLoad[5] = 0x00;
            downLoad[6] = 0x01;
            downLoad[7] = 0x02;
            downLoad[8] = 0x03;
            serialCom1.SendBytes(downLoad, downLoad.Length);
        }
    }
}
