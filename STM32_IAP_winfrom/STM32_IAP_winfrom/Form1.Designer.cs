namespace STM32_IAP_winfrom
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.serialCom1 = new SerialControl.SerialCom();
            this.Button_inputFile = new System.Windows.Forms.Button();
            this.Button_inputDownload = new System.Windows.Forms.Button();
            this.Button_download = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label_fileSize = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_bogSum = new System.Windows.Forms.Label();
            this.Label_show1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_setbacks = new System.Windows.Forms.Label();
            this.label_ = new System.Windows.Forms.Label();
            this.label_downState = new System.Windows.Forms.Label();
            this.richTextBox_show = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // serialCom1
            // 
            this.serialCom1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serialCom1.Location = new System.Drawing.Point(12, 12);
            this.serialCom1.Name = "serialCom1";
            this.serialCom1.Size = new System.Drawing.Size(240, 205);
            this.serialCom1.TabIndex = 0;
            // 
            // Button_inputFile
            // 
            this.Button_inputFile.Location = new System.Drawing.Point(280, 12);
            this.Button_inputFile.Name = "Button_inputFile";
            this.Button_inputFile.Size = new System.Drawing.Size(89, 23);
            this.Button_inputFile.TabIndex = 1;
            this.Button_inputFile.Text = "导入程序";
            this.Button_inputFile.UseVisualStyleBackColor = true;
            this.Button_inputFile.Click += new System.EventHandler(this.Button_inputFile_Click);
            // 
            // Button_inputDownload
            // 
            this.Button_inputDownload.Location = new System.Drawing.Point(280, 61);
            this.Button_inputDownload.Name = "Button_inputDownload";
            this.Button_inputDownload.Size = new System.Drawing.Size(89, 23);
            this.Button_inputDownload.TabIndex = 2;
            this.Button_inputDownload.Text = "进入下载模式";
            this.Button_inputDownload.UseVisualStyleBackColor = true;
            this.Button_inputDownload.Click += new System.EventHandler(this.Button_inputDownload_Click);
            // 
            // Button_download
            // 
            this.Button_download.Location = new System.Drawing.Point(280, 115);
            this.Button_download.Name = "Button_download";
            this.Button_download.Size = new System.Drawing.Size(89, 23);
            this.Button_download.TabIndex = 3;
            this.Button_download.Text = "下载程序";
            this.Button_download.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(384, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "文件大小:";
            // 
            // label_fileSize
            // 
            this.label_fileSize.AutoSize = true;
            this.label_fileSize.Location = new System.Drawing.Point(443, 17);
            this.label_fileSize.Name = "label_fileSize";
            this.label_fileSize.Size = new System.Drawing.Size(11, 12);
            this.label_fileSize.TabIndex = 5;
            this.label_fileSize.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(490, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "分包数:";
            // 
            // label_bogSum
            // 
            this.label_bogSum.AutoSize = true;
            this.label_bogSum.Location = new System.Drawing.Point(537, 17);
            this.label_bogSum.Name = "label_bogSum";
            this.label_bogSum.Size = new System.Drawing.Size(11, 12);
            this.label_bogSum.TabIndex = 7;
            this.label_bogSum.Text = "0";
            // 
            // Label_show1
            // 
            this.Label_show1.AutoSize = true;
            this.Label_show1.Location = new System.Drawing.Point(384, 66);
            this.Label_show1.Name = "Label_show1";
            this.Label_show1.Size = new System.Drawing.Size(89, 12);
            this.Label_show1.TabIndex = 8;
            this.Label_show1.Text = "未进入下载模式";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(384, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "下载进度:";
            // 
            // label_setbacks
            // 
            this.label_setbacks.AutoSize = true;
            this.label_setbacks.Location = new System.Drawing.Point(443, 120);
            this.label_setbacks.Name = "label_setbacks";
            this.label_setbacks.Size = new System.Drawing.Size(23, 12);
            this.label_setbacks.TabIndex = 10;
            this.label_setbacks.Text = "0/0";
            // 
            // label_
            // 
            this.label_.AutoSize = true;
            this.label_.Location = new System.Drawing.Point(490, 120);
            this.label_.Name = "label_";
            this.label_.Size = new System.Drawing.Size(53, 12);
            this.label_.TabIndex = 11;
            this.label_.Text = "下载状态";
            // 
            // label_downState
            // 
            this.label_downState.AutoSize = true;
            this.label_downState.Location = new System.Drawing.Point(549, 120);
            this.label_downState.Name = "label_downState";
            this.label_downState.Size = new System.Drawing.Size(41, 12);
            this.label_downState.TabIndex = 12;
            this.label_downState.Text = "未下载";
            // 
            // richTextBox_show
            // 
            this.richTextBox_show.Location = new System.Drawing.Point(280, 159);
            this.richTextBox_show.Name = "richTextBox_show";
            this.richTextBox_show.Size = new System.Drawing.Size(330, 58);
            this.richTextBox_show.TabIndex = 13;
            this.richTextBox_show.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 230);
            this.Controls.Add(this.richTextBox_show);
            this.Controls.Add(this.label_downState);
            this.Controls.Add(this.label_);
            this.Controls.Add(this.label_setbacks);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Label_show1);
            this.Controls.Add(this.label_bogSum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_fileSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Button_download);
            this.Controls.Add(this.Button_inputDownload);
            this.Controls.Add(this.Button_inputFile);
            this.Controls.Add(this.serialCom1);
            this.Name = "Form1";
            this.Text = "STM32_IAP上位机程序（By：lissettecarlr）";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SerialControl.SerialCom serialCom1;
        private System.Windows.Forms.Button Button_inputFile;
        private System.Windows.Forms.Button Button_inputDownload;
        private System.Windows.Forms.Button Button_download;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_fileSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_bogSum;
        private System.Windows.Forms.Label Label_show1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_setbacks;
        private System.Windows.Forms.Label label_;
        private System.Windows.Forms.Label label_downState;
        private System.Windows.Forms.RichTextBox richTextBox_show;
    }
}

