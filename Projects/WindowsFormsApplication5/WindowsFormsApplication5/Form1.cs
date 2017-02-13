using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Setting();
        }

        //コントロールの様々な設定
        private void Setting()
        {
            label1.Font = new Font(label1.Font.FontFamily, 22, label1.Font.Style); //ラベルのフォントサイズを変更

            //タイマーをスタート
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 現在時を取得
            DateTime datetime_now = DateTime.Now;
            DateTime datetime_set = new DateTime(datetime_now.Year, datetime_now.Month, datetime_now.Day, 23, 59, 0);
            int time = 0;

            //残り時間を秒に変換
            time = (((datetime_set.Hour - datetime_now.Hour) * 3600) + ((datetime_set.Minute - datetime_now.Minute) * 60) + (datetime_set.Second - datetime_now.Second));

            //残り時間を表示
            label1.Text = "残り " + time / 3600 + ":" + (time % 3600) / 60 + ":" + (time % 3600) % 60;

            if (datetime_now.ToLongTimeString() == datetime_set.ToLongTimeString())
            {
                label1.Text = "時間です。";
                //タイマー停止
                timer1.Stop();
            }
        }
    }
}

    
