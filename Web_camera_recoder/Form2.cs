using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Video.VFW;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Web_camera_recoder
{

    public partial class Form2 : Form
    {
        private FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        private bool recording = false;
        private bool paused = false;
        private PictureBox pictureBox1;
        private TabControl mainTabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private GroupBox groupBox1;
        private Button button1;
        private ListBox listBox1;
        private Button button2;
        private Label label1;
        private Timer timer1;
        private Button recordBtn;
        private Label statusTxt;
        private Button pauseBtn;
        private Button contBtn;
        private TrackBar hueBar;
        private Label label2;
        private TextBox hueText;
        private TextBox satText;
        private Label label3;
        private TrackBar satBar;
        private TextBox brightnessText;
        private Label label4;
        private TrackBar brightnessBar;
        private TextBox pixText;
        private Label label5;
        private TrackBar pixBar;
        private VideoCaptureDevice cam;
        private Bitmap frame;
        private AVIWriter aviWriter;
        public Form2()
        {
            this.InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            this.contBtn.Enabled = false;
            this.pauseBtn.Enabled = false;
            this.statusTxt.Text = "";
            this.aviWriter = new AVIWriter();
            this.aviWriter.FrameRate = 1000 / this.timer1.Interval;
            foreach (FilterInfo filterInfo in (CollectionBase)this.videoDevices)
                this.listBox1.Items.Add((object)filterInfo.Name);
            if (this.listBox1.Items.Count <= 0)
                return;
            this.listBox1.SelectedIndex = 0;
        }
        private void startWebcam(string deviceMoniker)
        {
            if (this.cam != null && this.cam.IsRunning)
                this.stopWebcam();
            this.cam = new VideoCaptureDevice(deviceMoniker);
            this.cam.DesiredFrameSize = this.pictureBox1.Size;
            this.cam.NewFrame += new NewFrameEventHandler(this.cam_NewFrame);
            this.cam.Start();
            this.timer1.Enabled = true;
        }

        private void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            this.frame = (Bitmap)eventArgs.Frame.Clone();
        }

        private void stopWebcam()
        {
            this.cam.SignalToStop();
            this.timer1.Enabled = false;
        }

       /* private void button1_Click(object sender, EventArgs e)
        {
            this.startWebcam(this.videoDevices[this.listBox1.SelectedIndex].MonikerString);
        }
        */
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.cam == null || !this.cam.IsRunning)
                return;
            this.cam.SignalToStop();
        }

       /* private void button2_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            this.videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in (CollectionBase)this.videoDevices)
                this.listBox1.Items.Add((object)filterInfo.Name);
            if (this.listBox1.Items.Count <= 0)
                return;
            this.listBox1.SelectedIndex = 0;
        }
        */
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.process();
        }

        private void process()
        {
            if (this.frame == null)
                return;
            Bitmap bitmap = this.frame;
            if (this.hueBar.Value != 0)
                new HueModifier(this.hueBar.Value).ApplyInPlace(bitmap);
            /*if (this.satBar.Value != 0)
                new SaturationCorrection(double)this.satBar.Value/100.0).ApplyInPlace(bitmap);
            if (this.brightnessBar.Value != 0)
                new BrightnessCorrection((double)this.brightnessBar.Value/100.0).ApplyInPlace(bitmap);
                */
            if (this.pixBar.Value != 0)
                new Pixellate(this.pixBar.Value).ApplyInPlace(bitmap);
            this.pictureBox1.Image = (Image)bitmap;
            if (!this.recording)
                return;
            if (this.paused)
            {
                this.statusTxt.Text = "Paused... " + (object)this.aviWriter.Position + " frames taken";
            }
            else
            {
                this.aviWriter.AddFrame(bitmap);
                this.statusTxt.Text = "Recording... " + (object)this.aviWriter.Position + " frames taken";
            }
        }

       /* private void button3_Click(object sender, EventArgs e)
        {
            if (this.recording)
            {
                this.aviWriter.Close();
                this.recordBtn.Text = "Start Recording";
                this.recording = false;
                this.statusTxt.Text = "Stopped.";
                this.contBtn.Enabled = false;
                this.pauseBtn.Enabled = false;
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Audio Video Interleave File | *.avi";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.statusTxt.Text = "Recording...";
                    this.aviWriter.Close();
                    string fileName1 = saveFileDialog.FileName;
                    AVIWriter aviWriter = this.aviWriter;
                    string fileName2 = fileName1;
                    Size size = this.pictureBox1.Size;
                    int width = size.Width;
                    size = this.pictureBox1.Size;
                    int height = size.Height;
                    aviWriter.Open(fileName2, width, height);
                    this.recordBtn.Text = "Stop Recording";
                    this.recording = true;
                    this.paused = false;
                    this.pauseBtn.Enabled = true;
                }
            }
        }
        */
       /* private void button4_Click(object sender, EventArgs e)
        {
            this.paused = true;
            this.contBtn.Enabled = true;
            this.pauseBtn.Enabled = false;
        }
        
        private void contBtn_Click(object sender, EventArgs e)
        {
            this.paused = false;
            this.contBtn.Enabled = false;
            this.pauseBtn.Enabled = true;
        }
        */
        
        private void button3_Click_1(object sender, EventArgs e)
        {
            this.startWebcam(this.videoDevices[this.listBox1.SelectedIndex].MonikerString);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            this.videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in (CollectionBase)this.videoDevices)
                this.listBox1.Items.Add((object)filterInfo.Name);
            if (this.listBox1.Items.Count <= 0)
                return;
            this.listBox1.SelectedIndex = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.paused = true;
            this.contBtn.Enabled = true;
            this.pauseBtn.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.paused = false;
            this.contBtn.Enabled = false;
            this.pauseBtn.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.recording)
            {
                this.aviWriter.Close();
                this.recordBtn.Text = "Start Recording";
                this.recording = false;
                this.statusTxt.Text = "Stopped.";
                this.contBtn.Enabled = false;
                this.pauseBtn.Enabled = false;
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Audio Video Interleave File | *.avi";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.statusTxt.Text = "Recording...";
                    this.aviWriter.Close();
                    string fileName1 = saveFileDialog.FileName;
                    AVIWriter aviWriter = this.aviWriter;
                    string fileName2 = fileName1;
                    Size size = this.pictureBox1.Size;
                    int width = size.Width;
                    size = this.pictureBox1.Size;
                    int height = size.Height;
                    aviWriter.Open(fileName2, width, height);
                    this.recordBtn.Text = "Stop Recording";
                    this.recording = true;
                    this.paused = false;
                    this.pauseBtn.Enabled = true;
                }
            }
        }
    }
}
