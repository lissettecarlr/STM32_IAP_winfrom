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
        int packetSum;
        byte[] binchar = new byte[] { };
        int state;//当前程序运行状态   0 :程序开始   1：准备下载  2：下载中  3：结束
        bool RvcMsgError = false;
        bool allowSend = false; //下载 与暂停下载
        bool packetgError = false; //包错误
        bool checkError = false; //校验错误
        int packetNumber = 1; //当前发送数据包号
        int timeoutSend = 0;

        public delegate void MyInvoke(string str);
        public delegate void GetTextHandler();

        public Form1()
        {
            InitializeComponent();
            packetSum = 0;
            state = 0;
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
                    packetSum = fileLen / 1024;
                else
                    packetSum = fileLen / 1024 + 1;

                label_fileSize.Text = fileLen.ToString(); //显示文件大小
                label_bogSum.Text = packetSum.ToString();//显示包数
                binchar = binreader.ReadBytes(fileLen);

    
                binreader.Close();
                //初始化
                packetNumber = 1;
                RvcMsgError = false;
                allowSend = false; //下载 与暂停下载
                packetgError = false; //包错误
                checkError = false; //校验错误
                state = 0;

                label_downState.Text = "未下载";
                label_setbacks.Text = "0/" + packetSum.ToString();
                Label_show1.Text = "未进入下载模式";
                richTextBox_show.Text = "";
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
            //MessageBox.Show(downLoad.Length.ToString());

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thr_Rcv = new Thread(new ThreadStart(this.Run));
            Thr_Rcv.Start();
        }
        private void Run()
        {
            byte[] head = new byte[2];
            while (true)
            {
                if(serialCom1.ReadBytes(ref head, 2))//如果读取到了包头
                {
                     if(head[0]== 0xbb &&head[1] == 0xaa) //如果是信息应答包
                    {
                        byte[] body = new byte[3];
                        Timeout msg_outtime = new Timeout(5);

                        while (true)
                        {            
                            if(msg_outtime.IsTimeout())
                            {
                                MyInvoke mi = new MyInvoke(updataRvcUI_boxshow);
                                this.BeginInvoke(mi, new Object[] { "信息包应答超时！！！" });
                                RvcMsgError = true;
                                break;//超时退出
                            }       
                            if(serialCom1.ReadBytes(ref body, 3))
                            {
                                if(body[0]+body[1] ==body[2]) //校验和
                                {
                                    if(body[0] == 0x01) //下位机接收成功了信息包
                                    {
                                        allowSend = true;
                                        state = 2;  
                                     
                                        MyInvoke mi = new MyInvoke(updataRvcUI_boxshow);
                                        this.BeginInvoke(mi, new Object[] { "信息包接收成功，开始传输数据" });

                                    }
                                    else //下位机接收信息包出错
                                    {
                                        
                                        RvcMsgError = true;
                                        MyInvoke mi = new MyInvoke(updataRvcUI_boxshow);
                                        this.BeginInvoke(mi, new Object[] { "下位机接收信息包失败" });

                                    }
                                    Array.Clear(body, 0, body.Length); //清空数组
                                    break;
                                }
                                else
                                {
                                    RvcMsgError = true;
                                    break;
                                }
                            }
                        }
                    }
                    else if(head[0] == 0xbb && head[1] ==0xbb)//如果是数据应答包
                    {
                        byte[] body = new byte[3];
                        Timeout data_outtime = new Timeout(5);
                        while(true)
                        {
                            if (data_outtime.IsTimeout())
                            {
                                MyInvoke mi = new MyInvoke(updataRvcUI_boxshow);
                                this.BeginInvoke(mi, new Object[] { "超时！！！" });
                                break;//超时退出
                            }
                            if (serialCom1.ReadBytes(ref body, 3))
                            {
                                if (body[0] + body[1] == body[2]) //校验和
                                {
                                    timeoutSend = 0;//将超时重传数置零
                                   if (body[0] == 2) //如果下位机校验错误
                                    {
                                        //重传 body[1]号包
                                        
                                        packetNumber = body[1]; //从该包从新开始传输
                                        MyInvoke mi = new MyInvoke(updataRvcUI_boxshow);
                                        this.BeginInvoke(mi, new Object[] { "发送包校验错误" });
                                        checkError = true;
                                    } 
                                   else if(body[0] ==3) //如果包号错误
                                    {
                                        //重传 body[1]号包
                                     
                                        packetNumber = body[1]; //从该包从新开始传输
                                        MyInvoke mi = new MyInvoke(updataRvcUI_boxshow);
                                        this.BeginInvoke(mi, new Object[] { "发送包号错误" });
                                        packetgError = true;
                                    }
                                   else //如果接收成功
                                    {                                       
                                        label_setbacks.Invoke(new GetTextHandler(updataRvcUI_setbacks)); //显示进度 
                                        packetNumber++;
                                        allowSend = true;

                                    }
                                    Array.Clear(body,0,body.Length); //清空数组
                                    break;
                                }
                                else
                                {
                                    //校验失败 这里不知道下位机是不是正确接收？
                                    break;
                                }
                            }
                        }

                    }
                    else if(head[0]==0xbb && head[1] == 0xcc)//如果是状态包
                    {

                    }
                    else if(head[0] == 0xbb && head[1] == 0xff) //进入IAP程序后会发送此信息
                    {
                        Timeout start_outtime = new Timeout(5);
                        while (!serialCom1.ReadBytes(ref head, 2)) //如果又读取到两个字节
                        {
                            if (start_outtime.IsTimeout())
                                break;
                        }
                        if (head[0] == 0xbb && head[1] == 0xff)
                        {
                            state = 1;//进入准备下载状态
                            MyInvoke mi = new MyInvoke(updataRvcUI_Label_show1);
                            this.BeginInvoke(mi, new Object[] { "下位机已经进入下载状态，允许下载" });
                        }
                        else
                            state = 0;

                    }
                    else { }
             
                }
                else
                {
                    Array.Clear(head, 0, head.Length); //清空数组                  
                }
            }
        }
        private void updataRvcUI_Label_show1(string str)
        {
            Label_show1.Text = str;
        }
        private void updataRvcUI_boxshow(string str)
        {
            richTextBox_show.AppendText(str);
            richTextBox_show.AppendText("\n");
        }

        private void updataRvcUI_setbacks()
        {
            label_setbacks.Text = packetNumber.ToString() + "/" + packetSum.ToString();
        }

        private void updataRvcUI_downloadState(string str)
        {
            label_downState.Text = str;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialCom1.Close();
            Environment.Exit(0);
        }

        private void richTextBox_show_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox_show.Text.CompareTo("") != 0)
            {
                richTextBox_show.SelectionStart = richTextBox_show.Text.Length - 1;
                richTextBox_show.ScrollToCaret();
            }
        }

        private void Button_download_Click(object sender, EventArgs e)
        {
            if(state ==1)
            {
                byte[] a =System.BitConverter.GetBytes(packetSum);
                SendMsgPacket(0x01,a[0]); //发送信息包
                
                Thread Send_Thread = new Thread(Send_Thread_run); //也可简写为new Thread(ThreadMethod);                
                Send_Thread.Start(); //启动线程 
                        
            }
            else
            {
                MessageBox.Show("请先进入下载模式！");
            }
        }

        private void SendMsgPacket(byte type,byte sum)
        {
            byte[] msg = new byte[6];
            msg[0] = 0xFF;
            msg[1] = 0xaa;
            msg[2] = type;
            msg[3] = sum;
            msg[4] = 0x00;
            msg[5] = 0x00;

            for (int i = 2; i < 5; i++)
                msg[5] += msg[i];

            serialCom1.SendBytes(msg, msg.Length);
        }

        private void SendDataPacket(byte[,] data,int packetSumber)
        {
            //减一是为了对其数组的零位
            byte[] sumber = System.BitConverter.GetBytes(packetSumber);
            byte[] datraPacket = new byte[1029];
            datraPacket[0] = 0xFF;
            datraPacket[1] = 0xbb;
            datraPacket[2] = 0x01;
            datraPacket[3] = sumber[0];
            for(int i = 4;i<1028;i++)
            {
                datraPacket[i] = data[packetSumber-1, i-4];
            }
            datraPacket[1028] = 0x00; //清零校验位
            for (int j = 2; j<1028;j++)
            {
                datraPacket[1028] +=datraPacket[j];
            }
            serialCom1.SendBytes(datraPacket, datraPacket.Length);
        }


        void Send_Thread_run()
        {
            //分包
            byte[,] packet = new byte[packetSum, 1024];//定义一个二维数组 来分包保存数据
            for (int n = 0; n < packetSum; n++)
            {
                for (int i = 0; i < 1024; i++)
                {

                    packet[n, i] = binchar[n * 1024 + i];
                    if ((binchar.Length - 1) == (n * 1024 + i)) //传送完成
                    {
                        for (int j = i + 1; j < 1024; j++)
                        {
                            packet[n, j] = 0xff;
                        }
                        n = packetSum;
                        break;
                    }
                }

            }
            //最后一包由于可能不满，所以不能像上面一样填充
           //for(int i = 0; i<(binchar.Length - (packetSum-1)*1024),i++)



            Timeout timeout = new Timeout(5);
            Timeout RcvMsgTimeout = new Timeout(5);
            packetNumber = 1;

            while (true)
            {
                if (state == 1)
                {
                    if (RvcMsgError == true)//如果信息包出错了
                    {
                        MyInvoke mi = new MyInvoke(updataRvcUI_boxshow);
                        this.BeginInvoke(mi, new Object[] { "信息包通讯失败，请重新下载" });

                        state = 1;
                        RvcMsgError = false;
                        break;
                    }
                    if(RcvMsgTimeout.IsTimeout())
                    {
                        MyInvoke mi = new MyInvoke(updataRvcUI_boxshow);
                        this.BeginInvoke(mi, new Object[] { "信息包应答超时，请重新下载" });
                        break;
                    }
                }
                else
                {
                    if (allowSend)
                    {
                        //label_downState.Text = "下载中";
                        MyInvoke mi = new MyInvoke(updataRvcUI_downloadState);
                        this.BeginInvoke(mi, new Object[] { "下载中" });
                        //timeout.updataLastTime();
                        SendDataPacket(packet, packetNumber); //发送一包数据
                                                              //记录时间

                        allowSend = false;

                        //Thread.Sleep(500); //延时500毫秒
                    }
                    if (packetgError || checkError) //如果是包号出错 或者是校验出错
                    {
                        packetgError = false;
                        checkError = false;
                        allowSend = true;
                    }

                    if (packetNumber > packetSum)//全部包都传送了
                    {
                        MyInvoke mi1 = new MyInvoke(updataRvcUI_downloadState);
                        this.BeginInvoke(mi1, new Object[] { "下载完成" });

                        MyInvoke mi2 = new MyInvoke(updataRvcUI_boxshow);
                        this.BeginInvoke(mi2, new Object[] { "下载完成，将自动运行新程序" });


                        state = 3;//下载结束
                        break;
                    }

                }
                //if(timeout.IsTimeout())
                //{
                //    allowSend = true;
                //    if (packetNumber > 1 && timeoutSend ==0) //超时重传数为0,在接收包处置零
                //        packetNumber--;//重传上一个包
                //    if (++timeoutSend>3)
                //    {
                //        state = 1;
                //        packetNumber = 1;
                //        label_downState.Text = "停止下载";
                //        break;
                //       // 3次超时重传后退出下载
                //    }

                //    //超时
                //}

                //if (state != 2) //如果状态不是下载中了 就直接退出下载
                //{
                //    packetNumber = 1;
                //    label_downState.Text = "停止下载";
                //    break;
                //}

            }
        }
    }
}
