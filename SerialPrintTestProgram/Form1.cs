using System.Net.Sockets;

namespace SerialPrintTestProgram
{
	public partial class Form1 : Form
	{
		private TcpClient tcpClient;
		private System.IO.Ports.SerialPort sp;
		private int currentConnectionType = 0; //0: None, 1: TCP, 2: Serial

		public Form1()
		{
			InitializeComponent();
			this.Text = "ZPL 프린터 테스트 프로그램";
			this.StartPosition = FormStartPosition.CenterScreen;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			sendBtn.Enabled = false;

			cmbIp.Items.Clear();
			cmbIp.Items.AddRange(new string[] { "10.8.38.212","192.168.0.211" });
			cmbIp.SelectedIndex = 0;

			cmbPort.Items.Clear();
			cmbPort.Items.Add(13890);
			cmbPort.SelectedIndex = 0;

			cmbSerial.Items.Clear();
			cmbSerial.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
			if(cmbSerial.Items.Count > 0)
			{
				cmbSerial.SelectedIndex = 0;
			}
		}

		private void tcpConnBtn_Click(object sender, EventArgs e)
		{
			//연결해서 정보 만들어주는 부분 싹다 구현하고 나중에 클래스로 분리시켜서 해당 클래스를 사용하는걸로 바꿔야할듯
			tcpConnBtn.Text = "연결중...";
			tcpConnBtn.Refresh();

			string ip = cmbIp.Text;
			int port;
			if (cmbPort.SelectedItem != null)
			{
				port = (int)cmbPort.SelectedItem;
			}
			else
			{
				MessageBox.Show("포트를 확인해주세요!");
				return;
			}

			try
			{
				//연결 시도
				tcpClient = new TcpClient();
				tcpClient.Connect(ip, port);
			}
			catch
			{
				//예외처리
				tcpClient.Close();
				tcpClient.Dispose();
				tcpConnBtn.Text = "연결";
				MessageBox.Show("연결에 실패했습니다!");

				return;
			}

			if (tcpClient != null)
			{
				sendBtn.Enabled = true;
				tcpConnBtn.Enabled = false;
				serialConnBtn.Enabled = false;
				currentConnectionType = 1;
				tcpConnBtn.Text = "연결됨";
			}
		}

		private void serialConnBtn_Click(object sender, EventArgs e)
		{
			string serialPortName = cmbSerial.Text;
			sp = new System.IO.Ports.SerialPort(serialPortName, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
			sp.Handshake = System.IO.Ports.Handshake.XOnXOff;
			try
			{
				sp.Open();
			}
			catch
			{
				sp.Close();
				sp.Dispose();
				MessageBox.Show(serialPortName + "연결에 실패했습니다!");
			}
			if (sp != null)
			{
				sendBtn.Enabled = true;
				tcpConnBtn.Enabled = false;
				serialConnBtn.Enabled = false;
				serialConnBtn.Text = "연결됨";
				serialConnBtn.Refresh();
				//현재 연결을 시리얼로 설정
				currentConnectionType = 2;
			}
		}

		private void sendBtn_Click(object sender, EventArgs e)
		{
			string zplMessage = textBox1.Text;
			if (string.IsNullOrWhiteSpace(zplMessage))
			{
				MessageBox.Show("출력할 내용을 입력해주세요!");
				return;
			}

			byte[] zplBytes = System.Text.Encoding.UTF8.GetBytes(zplMessage);

			//시리얼인지 TCP인지 구분 해야함
			if (currentConnectionType == 1)
			{
				try
				{
					
					NetworkStream stream = tcpClient.GetStream();
					stream.Write(zplBytes, 0, zplBytes.Length);

				}
				catch(Exception err)
				{
					MessageBox.Show(err.Message);
					return;
				}
			}
			else if (currentConnectionType == 2)
			{
				try
				{
					sp.Write(zplBytes, 0, zplBytes.Length);
				}
				catch
				{
					MessageBox.Show("시리얼 포트 열기에 실패했습니다!");
					return;
				}
			}

			MessageBox.Show("출력 성공!");
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (tcpClient != null) { tcpClient.Close();  tcpClient.Dispose(); }
			if (sp != null) { sp.Close();  sp.Dispose(); }
		}

		private void connResetbtn_Click(object sender, EventArgs e)
		{
			//연결 객체들 전부다 초기화 시킴
			if(tcpClient != null) { tcpClient.Close(); tcpClient.Dispose(); }
			if(sp != null) { sp.Close(); sp.Dispose(); }
			//Disable된 버튼들 다시 활성화 시키고
			tcpConnBtn.Enabled = true;
			serialConnBtn.Enabled = true;
			sendBtn.Enabled = false;
			tcpConnBtn.Text = "연결";
			serialConnBtn.Text = "연결";
			currentConnectionType = 0;
		}
	}
}