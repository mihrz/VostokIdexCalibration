using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GitHub.secile.Video;

namespace UsbCameraForms
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // find device.
            var devices = UsbCamera.FindDevices();
            if (devices.Length == 0) return; // no device.

            // get video format.
            var cameraIndex = 0;
            var formats = UsbCamera.GetVideoFormat(cameraIndex);

            // select the format you want.
            foreach (var item in formats) Console.WriteLine(item);
            var format = formats[0];

            // create instance.
            var camera = new UsbCamera(cameraIndex, format);
            // this closing event handler make sure that the instance is not subject to garbage collection.
            this.FormClosing += (s, ev) => camera.Release(); // release when close.
            
            // to show preview, there are 3 ways.
            // 1. use SetPreviewControl. (works light, recommended.)
            camera.SetPreviewControl(pictureBox1.Handle, pictureBox1.ClientSize);
            
            pictureBox1.Resize += (s, ev) => camera.SetPreviewSize(pictureBox1.ClientSize); // support resize.

            //// Set the picture location equal to the drop point.


            // 2. use Timer and GetBitmap().
            /*var timer = new System.Timers.Timer(1000 / 30) { SynchronizingObject = this };
            timer.Elapsed += (s, ev) => pictureBox1.Image = camera.GetBitmap();
            timer.Start();
            this.FormClosing += (s, ev) => timer.Stop();*/

            // 3. subscribe PreviewCaptured.
            /*camera.PreviewCaptured += (bmp) =>
            {
                // called by worker thread, you have to call cross-thread control in a thread-safe way.
                pictureBox1.Invoke((Action)(() =>
                {
                    pictureBox1.Image = bmp;
                }));
            };*/

            // start.
            camera.Start();

            // get bitmap.
            //button1.Click += (s, ev) => pictureBox2.Image = camera.GetBitmap();

            // still image
            //if (camera.StillImageAvailable)
            //{
            //    button2.Click += (s, ev) => camera.StillImageTrigger();
            //    camera.StillImageCaptured += bmp => pictureBox2.Image = bmp;
            //}
        }



        //public void DrawLinePoint(PaintEventArgs e)
        //{

        //    // Create pen.
        //    Pen blackPen = new Pen(Color.Black, 3);

        //    // Create points that define line.
        //    Point point1 = new Point(425, 300);
        //    Point point2 = new Point(475, 300);

        //    // Draw line to screen.
        //    e.Graphics.DrawLine(blackPen, point1, point2);

        //    Point point3 = new Point(450, 275);
        //    Point point4 = new Point(450, 325);

        //    // Draw line to screen.
        //    e.Graphics.DrawLine(blackPen, point3, point4);
        //}

        //protected void pictureBox1_Paint(object sender, PaintEventArgs e)
        //{
        //    //base.OnPaint(e);
        //    DrawLinePoint(e);
        //}


        double x01 = 0;
        double y01 = 0;

       
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double a0 = double.Parse(textBox5.Text);
                double a = double.Parse(textBox1.Text);
                double b = double.Parse(textBox2.Text);
                x01 = (a0 - (a - b));
                x01 = Math.Round(x01,3);
                label6.Text = "x0_1="+ x01.ToString();
            }

            catch { }


            try
            {
                double a0 = double.Parse(textBox6.Text);
                double a = double.Parse(textBox3.Text);
                double b = double.Parse(textBox4.Text);

                y01 = (a0 - (a - b));
                y01 = Math.Round(y01, 3);
                label7.Text = "y0_1=" + y01.ToString();
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox5.Text = x01.ToString();
            textBox6.Text = y01.ToString();


        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {

             double x1 = Math.Round(double.Parse(textBox1.Text),2);
            double y1 = Math.Round(double.Parse(textBox3.Text), 2);
                textBox7.Text = "g1 x" + x1.ToString() + " y" + y1.ToString() + "f10000";

                                }
            catch { }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
