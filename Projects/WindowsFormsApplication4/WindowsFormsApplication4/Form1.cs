﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        static int winSize = 240;
        static int centerX = 120;
        static int centerY = 120;
        static int secHandLength = 100;
        static int minHandLength = 90;
        static int hourHandLength = 80;
        static int numPos = 105;
        static double DegrOf0h = Math.PI / 2.0;   // 0時の角度(ラジアン)
        static double DegrOf1h = Math.PI / 3.0;   // 1時の角度(ラジアン)
        static double DegrPerHour = Math.PI / 6.0;   // 1時間あたりの角度(ラジアン)
        static double DegrPerMin = Math.PI / 30.0;  // 1分あたりの角度(ラジアン)
        static double DegrPerSec = Math.PI / 30.0;  // 1秒あたりの角度(ラジアン)
        double theta = DegrOf0h;
        double sec, min, hour, secAng, minAng, hourAng;
        int secX, secY, minX, minY, hourX, hourY;

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeComponent();

            this.BackColor = SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(winSize, winSize);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.DoubleBuffered = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g;
            Pen pen;
            Font font;
            int x, y;

            g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            font = new Font("Arial", 9, FontStyle.Regular);

            theta = DegrOf1h;
            for (int i = 1; i <= 12; i++)
            {
                String num = i.ToString("D");
                x = centerX + (int)(Math.Cos(theta) * numPos);
                y = centerY - (int)(Math.Sin(theta) * numPos);
                SizeF size = g.MeasureString(num, font);
                g.DrawString(num, font, Brushes.Magenta, x - size.Width / 2, y - size.Height / 2);
                theta = theta - DegrPerHour;
            }

            g.DrawString(DateTime.Now.ToString(), font, Brushes.Magenta, 60, 160);

            sec = (double)(DateTime.Now.Second);
            min = (double)(DateTime.Now.Minute) + sec / 60.0;
            hour = (double)(DateTime.Now.Hour) + min / 60.0;

            secAng = DegrOf0h - DegrPerSec * sec;
            minAng = DegrOf0h - DegrPerMin * min;
            hourAng = DegrOf0h - DegrPerHour * hour;
            secX = centerX + (int)(Math.Cos(secAng) * secHandLength);
            secY = centerY - (int)(Math.Sin(secAng) * secHandLength);
            minX = centerX + (int)(Math.Cos(minAng) * minHandLength);
            minY = centerY - (int)(Math.Sin(minAng) * minHandLength);
            hourX = centerX + (int)(Math.Cos(hourAng) * hourHandLength);
            hourY = centerY - (int)(Math.Sin(hourAng) * hourHandLength);

            pen = new Pen(Color.Magenta, 1);
            g.DrawLine(pen, centerX, centerY, secX, secY);
            pen = new Pen(Color.Magenta, 2);
            g.DrawLine(pen, centerX, centerY, minX, minY);
            g.DrawLine(pen, centerX, centerY, hourX, hourY);

            font.Dispose();
            pen.Dispose();
        }


    }
}
