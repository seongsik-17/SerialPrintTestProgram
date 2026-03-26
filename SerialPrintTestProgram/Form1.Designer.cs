namespace SerialPrintTestProgram
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			label1 = new Label();
			label2 = new Label();
			cmbIp = new ComboBox();
			cmbPort = new ComboBox();
			tcpConnBtn = new Button();
			cmbSerial = new ComboBox();
			label3 = new Label();
			serialConnBtn = new Button();
			textBox1 = new TextBox();
			sendBtn = new Button();
			connResetbtn = new Button();
			ZplTestBtn = new Button();
			textBoxClrearBtn = new Button();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(93, 9);
			label1.Name = "label1";
			label1.Size = new Size(22, 20);
			label1.TabIndex = 1;
			label1.Text = "IP";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(253, 9);
			label2.Name = "label2";
			label2.Size = new Size(47, 20);
			label2.TabIndex = 1;
			label2.Text = "PORT";
			// 
			// cmbIp
			// 
			cmbIp.FormattingEnabled = true;
			cmbIp.Location = new Point(12, 32);
			cmbIp.Name = "cmbIp";
			cmbIp.Size = new Size(202, 28);
			cmbIp.TabIndex = 2;
			// 
			// cmbPort
			// 
			cmbPort.FormattingEnabled = true;
			cmbPort.Location = new Point(220, 32);
			cmbPort.Name = "cmbPort";
			cmbPort.Size = new Size(111, 28);
			cmbPort.TabIndex = 2;
			// 
			// tcpConnBtn
			// 
			tcpConnBtn.Location = new Point(337, 31);
			tcpConnBtn.Name = "tcpConnBtn";
			tcpConnBtn.Size = new Size(94, 29);
			tcpConnBtn.TabIndex = 3;
			tcpConnBtn.Text = "연결";
			tcpConnBtn.UseVisualStyleBackColor = true;
			tcpConnBtn.Click += tcpConnBtn_Click;
			// 
			// cmbSerial
			// 
			cmbSerial.FormattingEnabled = true;
			cmbSerial.Location = new Point(12, 96);
			cmbSerial.Name = "cmbSerial";
			cmbSerial.Size = new Size(202, 28);
			cmbSerial.TabIndex = 2;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(84, 73);
			label3.Name = "label3";
			label3.Size = new Size(45, 20);
			label3.TabIndex = 1;
			label3.Text = "COM";
			// 
			// serialConnBtn
			// 
			serialConnBtn.Location = new Point(337, 96);
			serialConnBtn.Name = "serialConnBtn";
			serialConnBtn.Size = new Size(94, 29);
			serialConnBtn.TabIndex = 3;
			serialConnBtn.Text = "연결";
			serialConnBtn.UseVisualStyleBackColor = true;
			serialConnBtn.Click += serialConnBtn_Click;
			// 
			// textBox1
			// 
			textBox1.Location = new Point(12, 131);
			textBox1.Multiline = true;
			textBox1.Name = "textBox1";
			textBox1.ScrollBars = ScrollBars.Both;
			textBox1.Size = new Size(762, 269);
			textBox1.TabIndex = 4;
			// 
			// sendBtn
			// 
			sendBtn.Location = new Point(680, 406);
			sendBtn.Name = "sendBtn";
			sendBtn.Size = new Size(94, 29);
			sendBtn.TabIndex = 3;
			sendBtn.Text = "전송";
			sendBtn.UseVisualStyleBackColor = true;
			sendBtn.Click += sendBtn_Click;
			// 
			// connResetbtn
			// 
			connResetbtn.Location = new Point(629, 14);
			connResetbtn.Name = "connResetbtn";
			connResetbtn.Size = new Size(145, 46);
			connResetbtn.TabIndex = 5;
			connResetbtn.Text = "연결 초기화";
			connResetbtn.UseVisualStyleBackColor = true;
			connResetbtn.Click += connResetbtn_Click;
			// 
			// ZplTestBtn
			// 
			ZplTestBtn.Location = new Point(629, 79);
			ZplTestBtn.Name = "ZplTestBtn";
			ZplTestBtn.Size = new Size(145, 46);
			ZplTestBtn.TabIndex = 5;
			ZplTestBtn.Text = "테스트 ZPL 출력";
			ZplTestBtn.UseVisualStyleBackColor = true;
			ZplTestBtn.Click += ZplTestBtn_Click;
			// 
			// textBoxClrearBtn
			// 
			textBoxClrearBtn.Location = new Point(12, 406);
			textBoxClrearBtn.Name = "textBoxClrearBtn";
			textBoxClrearBtn.Size = new Size(94, 29);
			textBoxClrearBtn.TabIndex = 3;
			textBoxClrearBtn.Text = "Clear";
			textBoxClrearBtn.UseVisualStyleBackColor = true;
			textBoxClrearBtn.Click += textBoxClrearBtn_Click;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(9F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(ZplTestBtn);
			Controls.Add(connResetbtn);
			Controls.Add(textBox1);
			Controls.Add(textBoxClrearBtn);
			Controls.Add(sendBtn);
			Controls.Add(serialConnBtn);
			Controls.Add(tcpConnBtn);
			Controls.Add(cmbPort);
			Controls.Add(cmbSerial);
			Controls.Add(cmbIp);
			Controls.Add(label2);
			Controls.Add(label3);
			Controls.Add(label1);
			Name = "Form1";
			Text = "asdfsd";
			FormClosing += Form1_FormClosing;
			Load += Form1_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private Label label1;
		private Label label2;
		private ComboBox cmbIp;
		private ComboBox cmbPort;
		private Button tcpConnBtn;
		private ComboBox cmbSerial;
		private Label label3;
		private Button serialConnBtn;
		private TextBox textBox1;
		private Button sendBtn;
		private Button connResetbtn;
		private Button ZplTestBtn;
		private Button textBoxClrearBtn;
	}
}
