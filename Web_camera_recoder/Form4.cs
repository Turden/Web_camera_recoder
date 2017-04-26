using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.VFW;
using AForge.Video;
using AForge.Video.DirectShow;
using System.IO;
using System.Drawing.Imaging;

namespace Web_camera_recoder
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        Graphics g;
        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice FinalFrame;
        private void Form4_Load(object sender, EventArgs e)
        {
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in CaptureDevice)
            {
                comboBox1.Items.Add(Device.Name);
            }
            comboBox1.SelectedIndex = 0;
            FinalFrame = new VideoCaptureDevice();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FinalFrame = new VideoCaptureDevice(CaptureDevice[comboBox1.SelectedIndex].MonikerString);
            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.Start();
        }

        void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }
        private void Form4_FormClosing(object sender, FormClosedEventArgs e)
        {
            if (FinalFrame.IsRunning == true) FinalFrame.Stop();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = (Bitmap)pictureBox1.Image.Clone();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //pictureBox2.Image.Save(@"D:\Test.Jpeg", ImageFormat.Jpeg);
            Bitmap bmpSave = (Bitmap)pictureBox2.Image;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "bmp";
            sfd.Filter = "Image files (*.bmp)|*.bmp|Image files (*.Jpeg)|*.Jpeg|All files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                bmpSave.Save(sfd.FileName, ImageFormat.Bmp);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            FinalFrame.Stop();
        }

        
    }
}
