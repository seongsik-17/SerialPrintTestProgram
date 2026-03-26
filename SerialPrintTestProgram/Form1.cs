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
			ZplTestBtn.Enabled = false;

			cmbIp.Items.Clear();
			cmbIp.Items.AddRange(new string[] { "10.8.38.212", "192.168.0.211" });
			cmbIp.SelectedIndex = 0;

			cmbPort.Items.Clear();
			cmbPort.Items.Add(13890);
			cmbPort.SelectedIndex = 0;

			cmbSerial.Items.Clear();
			cmbSerial.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
			if (cmbSerial.Items.Count > 0)
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
				ZplTestBtn.Enabled = true;
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
				ZplTestBtn.Enabled = true;
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
				catch (Exception err)
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

		private void connResetbtn_Click(object sender, EventArgs e)
		{
			//연결 객체들 전부다 초기화 시킴
			if (tcpClient != null) { tcpClient.Close(); tcpClient.Dispose(); }
			if (sp != null) { sp.Close(); sp.Dispose(); }
			//Disable된 버튼들 다시 활성화 시키고
			tcpConnBtn.Enabled = true;
			serialConnBtn.Enabled = true;
			sendBtn.Enabled = false;
			tcpConnBtn.Text = "연결";
			serialConnBtn.Text = "연결";
			currentConnectionType = 0;
		}

		private void ZplTestBtn_Click(object sender, EventArgs e)
		{
			//기본 양식 받으면 넣기
			textBox1.Text = "^XA\r\n^CI28\r\n^CWK, E:KFONT3.FNT\r\n\r\n^FO10,10^GFA,44100,44100,98,,:::::01oQF8,03oQFC,::03ChJ0FCmI03C,:::::::::::::::::::::::::::::::::::::::::03CL07JFCK07KF8I03OFEL0FCmI03C,03CL0LFK07KFEI03OFEL0FCmI03C,03CK01MFJ0MFC003OFEL0FCmI03C,03CK01MF8001MFE003OFEL0FCmI03C,03CK01MFE001NF803OFEL0FCmI03C,03CK01NF001NFC03OFEL0FCmI03C,:03CK01NF801NFE03OFEL0FCmI03C,03CK01FFC01IFC01FFE003IFK0FFEP0FCmI03C,03CK01FF800IFC01FFE001IFK0FFEP0FCmI03C,03CK01FF8003FFC01FFEI07FFK0FFEP0FCmI03C,03CK01FF8003FFE01FFEI07FFK0FFEP0FCmI03C,03CK01FF8001FFE01FFEI07FF8J0FFEP0FCmI03C,03CK01FF8I0IF01FFEI07FF8J0FFEP0FCmI03C,03CK01FF8I0IF01FFEI03FF8J0FFEP0FCmI03C,:::03CK01FF8I0IF01FFEI07FFK0FFEP0FCmI03C,::03CK01FF8001FFE01FFE001FFEK0FFEP0FCmI03C,03CK01FF8003FFC01FFE003FFCK0FFEP0FCmI03C,03CK01FF8003FFC01NFCK0FFEP0FCmI03C,03CK01FF8003FFC01NF8K0FFEP0FCmI03C,03CK01FF800IFC01MFEL0FFEP0FCmI03C,03CK01FFC03IF801MFCL0FFEP0FCmI03C,03CK01NF001MFM0FFEP0FCmI03C,03CK01NF001LFEM0FFEP0FCmI03C,03CK01MFC001MF8L0FFEP0FCmI03C,03CK01MFC001MFCL0FFEP0FCmI03C,03CK01MFI01FFE03FFCL0FFEP0FCmI03C,03CK01LFCI01FFE01FFEL0FFEP0FCmI03C,03CK01KFEJ01FFE007FFL0FFEP0FCmI03C,03CK01KFK01FFE007FFL0FFEP0FCmI03C,03CK01FFCM01FFE003FF8K0FFEP0FCmI03C,03CK01FF8M01FFE001FFCK0FFEP0FCmI03C,::03CK01FF8M01FFEI0FFCK0FFEP0FCmI03C,03CK01FF8M01FFEI0FFEK0FFEP0FCmI03C,03CK01FF8M01FFEI07FFK0FFEP0FCmI03C,:::03CK01FF8M01FFEI03FF8J0FFEP0FCmI03C,03CK01FF8M01FFEI03FFCJ0FFEP0FCmI03C,::03CK01FF8M01FFEI01FFEJ0FFEP0FCmI03C,03CK01FF8M01FFEJ0IFJ0FFEP0FCmI03C,:03CK01FF8N0FFEJ0IFJ07FEP0FCmI03C,03CK01FF8N07FCJ07FFJ07FEP0FCmI03C,03ChJ0FCmI03C,::::::::::::::::::::::::::::::::::::::03oPF3C,:::::03ChJ0FCmI03C,:::::::::::::::::::::::::::::::::::::::::::::::03CM03FF8T0IFK03FCN0FCmI03C,03CM0IFES01IF8J07FEN0FCmI03C,03CL07JFCR03IFCJ07FEN0FCmI03C,03CK01KFER03IFEJ07FEN0FCmI03C,03CK03LFR03JFJ07FEN0FCmI03C,03CK07LFR03JFJ07FEN0FCmI03C,03CK0MFR03JFJ07FEN0FCmI03C,:03CJ01IF00FFR03JF8I07FEN0FCmI03C,03CJ01FFE007FR03JFCI07FEN0FCmI03C,03CJ03FF8I0FR03JFCI07FEN0FCmI03C,03CJ03FF8I03R03JFCI07FEN0FCmI03C,03CJ07FF8V03FF3FEI07FEN0FCmI03C,03CJ07FF8V03FF3FFI07FEN0FCmI03C,:03CJ07FF8V03FF1FF8007FEN0FCmI03C,:03CJ07FFEV03FF0FFC007FEN0FCmI03C,:03CJ03IF8U03FF0FFC007FEN0FCmI03C,03CJ01IFCU03FF07FE007FEN0FCmI03C,03CJ01JFCT03FF03FE007FEN0FCmI03C,03CJ01JFET03FF03FE007FEN0FCmI03C,03CK0KFCS03FF03FE007FEN0FCmI03C,03CK07JFES03FF01FF007FEN0FCmI03C,03CK03KF8R03FF00FF807FEN0FCmI03C,03CK01KFCR03FF00FF807FEN0FCmI03C,03CL07KFR03FF00FF807FEN0FCmI03C,03CL03KFR03FF007FC07FEN0FCmI03C,03CM07JF8Q03FF003FE07FEN0FCmI03C,03CM03JFCQ03FF003FE07FEN0FCmI03C,03CN07IFCQ03FF003FE07FEN0FCmI03C,03CN03IFCQ03FF001FE07FEN0FCmI03C,03CO0IFEQ03FFI0FF07FEN0FCmI03C,03CO07IFQ03FFI0FF87FEN0FCmI03C,03CO03IFQ03FFI0FF87FEN0FCmI03C,03CO01IFQ03FFI07F87FEN0FCmI03C,03CP0IFQ03FFI03FC7FEN0FCmI03C,03CP0IFQ03FFI03FE7FEN0FCmI03C,03CP0IFQ03FFI01FE7FEN0FCmI03C,03CP0FFEQ03FFI01FE7FEN0FCmI03C,03CO01FFEQ03FFI01JFEN0FCmI03C,03CJ07J03FFCQ03FFJ0JFEN0FCmI03C,03CJ078I03FFCQ03FFJ0JFEN0FCmI03C,03CJ07FI0IFCQ03FFJ07IFEN0FCmI03C,03CJ07FC01IFCQ03FFJ07IFEN0FCmI03C,03CJ07MFR03FFJ03IFEN0FCmI03C,:03CJ07LFCR03FFJ01IFEN0FCmI03C,:03CJ03LFS03FFK0IFEN0FCmI03C,03CJ01KFES03FFK07FFEN0FCmI03C,03CK01IFET03FFK03FFCN0FCmI03C,03CL07FF8T01FEK01FF8N0FCmI03C,03ChJ0FCmI03C,:::::::::::::::::::::::::::::::::::::::::::::03CoPFC,03oQFC,:::03EoPFC,03CoO03C,::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::03oQFC,::01oQF8,,:::^FS\r\n\r\n^CF0,44,44\r\n^FO220,60^A0,80,80^FDTK-S110^FS\r\n^FO220,210^A0,70,60^FD08284762513032515^FS\r\n^BY3,5,80\r\n^FO65,340^BC^FD08284762513032515^FS\r\n                \r\n^XZ";
		}

		private void textBoxClrearBtn_Click(object sender, EventArgs e)
		{
			textBox1.Clear();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (tcpClient != null) { tcpClient.Close(); tcpClient.Dispose(); }
			if (sp != null) { sp.Close(); sp.Dispose(); }
		}

	}
}