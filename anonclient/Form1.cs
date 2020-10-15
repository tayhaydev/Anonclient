using System;
using System.Windows.Forms;
using System.Net;
using System.Runtime.InteropServices;
using System.Drawing;

namespace AnonFile_Uploader
{    

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                string url = Helper.ApiKey();
                string filePath = "";
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filePath = ofd.FileName;
                }

                using (var webClient = new WebClient())
                {
                    var response = webClient.UploadFile(url, filePath);
                    string stg = System.Text.Encoding.UTF8.GetString(response);
                    richTextBox1.Text += Helper.Pars(stg, "full\":\"", "\",") + "\n";
                    string clip = richTextBox1.Text;
                    string clip1 = clip.Remove(clip.Length - 1, 1);
                    Clipboard.SetText(clip1);
                }
            }
            catch
            { }
        }
        private void label2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.FromArgb(192, 0, 192);
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.ForeColor = Color.FromArgb(192, 0, 192);
            button1.BackColor = Color.FromArgb(29, 29, 29);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.FromArgb(255, 255, 255);
            button1.BackColor = Color.FromArgb(29, 29, 29);
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void panel1_Paint(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.ActiveControl = null;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0XA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            }
        }
    }
}
