using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Imaging.ComplexFilters;
using System.Drawing.Imaging;

namespace Web_camera_recoder
{
    public partial class Form3 : Form
    {
        

        public Form3()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sepia sp = new Sepia();
            pictureBox2.Image = sp.Apply((Bitmap)pictureBox1.Image);
        }

        private void hueModifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HueModifier hue = new HueModifier();
            pictureBox2.Image = hue.Apply((Bitmap)pictureBox1.Image);
        }

        private void rotateChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RotateChannels rc = new RotateChannels();
            pictureBox2.Image = rc.Apply((Bitmap)pictureBox2.Image);
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Invert iv = new Invert();
            pictureBox2.Image = iv.Apply((Bitmap)pictureBox2.Image);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  pictureBox2.Image.Save(@"D:\Test.Jpeg",ImageFormat.Jpeg);
            Bitmap bmpSave = (Bitmap)pictureBox2.Image;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "bmp";
            sfd.Filter = "Image files (*.bmp)|*.bmp|Image files (*.Jpeg)|*.Jpeg|All files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                bmpSave.Save(sfd.FileName, ImageFormat.Bmp);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
                
    }
}
