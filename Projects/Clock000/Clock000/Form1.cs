using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock000
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();  // タイマー
        private System.Media.SoundPlayer player = null;

        string path = @"C:\Users\home\Music\koukaon.wav";
        int year = 0, month = 0, day = 0, hour = 0, minute = 0, second = 0;
        double i = 0,j=0,k=0;

        DateTime datetime_set;

        public Form1()
        {
            InitializeComponent();
            // 更新間隔 (ミリ秒)
            timer.Interval = 1000;
            // タイマ用のイベントハンドラを登録
            timer.Tick += new EventHandler(timer1_Tick);
            // タイマ ON
            timer.Start();


            //年（初期値）
            textBox1.Text = "2017";
            //月（初期値）
            textBox2.Text = "02";
            //日（初期値）
            textBox7.Text = "01";
            //時間（初期値）
            textBox3.Text = "06";
            //分（初期値）
            textBox5.Text = "00";
            //秒（初期値）
            textBox6.Text = "00";

        }

        // Tick イベントのイベントハンドラ
        private void timer1_Tick(object sender, EventArgs e)
        {
            // 現在時を取得
            DateTime datetime = DateTime.Now;

            // ラベルに現在時刻を表示
            //            label_time_now.Text = datetime.ToLongTimeString();
            label_time_now.Text = datetime.Year.ToString() + "年"
                                   + datetime.Month.ToString() + "月"
                                   + datetime.Day.ToString() + "日"
                                   + datetime.Hour.ToString() + "時"
                                   + datetime.Minute.ToString() + "分"
                                   + datetime.Second.ToString() + "秒";


            //タイマセットの初期値
            label_time_set.Text = datetime_set.ToLongTimeString();


            //現在の時間が設定の時間になった時の処理
            if (datetime.ToLongTimeString() == datetime_set.ToLongTimeString())
            {
                PlaySound(path);
                lb1.Text = "時間だよ";
            }
        }

        private void button5_Click(object sender, EventArgs e)  //ストップウォッチのスタート＆ストップボタン
        {
            if (timer2.Enabled == true) { this.button5.Text = "<start>"; this.timer2.Enabled = false; }
            else if (timer2.Enabled == false) { this.button5.Text = "<stop>"; this.timer2.Enabled = true; }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (i >= 60)         //何時間経過かを計算する
            {
                i %= 60;
                j=i/60;
            }
            if (j >= 60)    //何分経過かを計算する
            {
                j %= 60;
                k = j / 60; ;

            }
            i=i+0.1;                //一秒ずつ足していく
            this.label1.Text = k.ToString("00") + ":" + j.ToString("00") + ":" + i.ToString("00.0");  //経過した時間を表示する    
        }

        private void button6_Click(object sender, EventArgs e)
        {
            i = 0;
            this.label1.Text = "00:00:00.0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hour = int.Parse(textBox3.Text);
            minute = int.Parse(textBox5.Text);
            second = int.Parse(textBox6.Text);
            day = int.Parse(textBox7.Text);
            month = int.Parse(textBox2.Text);
            year = int.Parse(textBox1.Text);

            datetime_set = new DateTime(year, month, day, hour, minute, second);
            label_time_set.Text = datetime_set.ToLongTimeString();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //現在の時刻の取得
            DateTime datetime = DateTime.Now;

            //年を取得する。「2000」となる。
            textBox1.Text = datetime.Year.ToString();
            //            year = datetime.Year;
            //月を取得する。「9」となる。
            textBox2.Text = datetime.Month.ToString();
            //            month = datetime.Month;
            //日を取得する。「30」となる。
            textBox7.Text = datetime.Day.ToString();
            //            day = datetime.Day;
            //時間を取得する。
            textBox3.Text = datetime.Hour.ToString();
            //分を取得する。
            textBox5.Text = datetime.Minute.ToString();
            //秒を取得する。
            textBox6.Text = datetime.Second.ToString();
        }

  
        private void button3_Click(object sender, EventArgs e)
        {
            // OpenFileDialog の新しいインスタンスを生成する (デザイナから追加している場合は必要ない)
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            // ダイアログのタイトルを設定する
            openFileDialog1.Title = "ダイアログのタイトルをココに書く";
            // 初期表示するディレクトリを設定する
            openFileDialog1.InitialDirectory = @"C:\Users\home\Music";
            // 初期表示するファイル名を設定する
            openFileDialog1.FileName = "初期表示するファイル名をココに書く";
            // ファイルのフィルタを設定する
            openFileDialog1.Filter = "wav ファイル|*.wav;|すべてのファイル|*.*";
            // ファイルの種類 の初期設定を 2 番目に設定する (初期値 1)
            openFileDialog1.FilterIndex = 2;
            // ダイアログボックスを閉じる前に現在のディレクトリを復元する (初期値 false)
            openFileDialog1.RestoreDirectory = true;
            // 複数のファイルを選択可能にする (初期値 false)
            openFileDialog1.Multiselect = true;

            // ダイアログを表示し、戻り値が [OK] の場合は、選択したファイルを表示する
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = openFileDialog1.FileName;
                path = openFileDialog1.FileName;
            }

            // 不要になった時点で破棄する (正しくは オブジェクトの破棄を保証する を参照)
            openFileDialog1.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StopSound();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //再生
        private void PlaySound(string waveFile)
        {
            //音声が再生されている場合停止する
            if (player != null)
                StopSound();

            player = new System.Media.SoundPlayer(waveFile);
            player.Play(); //再生
        }

        //停止
        private void StopSound()
        {
            if (player != null)
            {
                player.Stop();
                player.Dispose();
                player = null;
            }
        }
    }
}
