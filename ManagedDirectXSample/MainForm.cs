using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagedDirectX
{
    /// <summary>
    /// メインフォーム
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// 描画空間
        /// </summary>
        private DrawingWorld drawingWord = null;

        /// <summary>
        /// 枠
        /// </summary>
        private DrawingFrame frame = null;

        /// <summary>
        /// XYZ軸
        /// </summary>
        private DrawingXYZAxis xyzAxis = null;

        /// <summary>
        /// ボール1
        /// </summary>
        private DrawingSphere sphere1 = null;

        /// <summary>
        /// ボール2
        /// </summary>
        private DrawingSphere sphere2 = null;

        /// <summary>
        /// ボールの高さ計算用角度
        /// </summary>
        private double heightAngle = 0.0;

        /// <summary>
        /// テキスト
        /// </summary>
        private DrawingText text = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.drawingWord = new DrawingWorld(this.panelCanvas);
            if (!this.drawingWord.Initialize())
            {
                MessageBox.Show("そんなバカな！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.drawingWord.BackColor = Color.FromArgb(255, 30, 30, 30);

            this.frame = this.drawingWord.Factory.CreateFrame(20.0f, 15.0f, 12.0f, Color.Silver);
            this.xyzAxis = this.drawingWord.Factory.CreateXYZAxis(20.0f);
            this.sphere1 = this.drawingWord.Factory.CreateSphere(2.0f, 32, 32, Color.SlateBlue);
            this.sphere2 = this.drawingWord.Factory.CreateSphere(1.0f, 32, 32, Color.GreenYellow);
            this.sphere1.Location = new Point3D(5.0f, 0.0f, 3.0f);
            this.sphere2.Location = new Point3D(-3.0f, 0.0f, -1.0f);
            this.text = this.drawingWord.Factory.CreateText(12, "ＭＳ ゴシック", Color.Crimson);

            this.backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// クローズイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.backgroundWorker.IsBusy)
            {
                this.backgroundWorker.CancelAsync();
            }

            this.drawingWord.Dispose();
            this.drawingWord = null;
        }

        /// <summary>
        /// 描画スレッド
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (this.backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                this.drawingWord.BeginScene();
                {
                    // 描画処理はここで！
                    this.drawingWord.Draw(this.frame);
                    this.drawingWord.Draw(this.xyzAxis);

                    this.heightAngle += 2.0;
                    if (360.0f <= this.heightAngle)
                    {
                        this.heightAngle -= 360.0;
                    }
                    this.sphere1.Location.Y = 4.0f * (float)System.Math.Sin((this.heightAngle / 180 * System.Math.PI)) - 3.0f;
                    this.sphere2.Location.Y = -8.0f * (float)System.Math.Sin((this.heightAngle / 180 * System.Math.PI)) + 2.0f;
                    this.drawingWord.Draw(this.sphere1);
                    this.drawingWord.Draw(this.sphere2);

                    this.text.Strings.Clear();
                    this.text.Strings.Add("Camera setting");
                    this.text.Strings.Add("θ：" + this.drawingWord.Camera.Theta + "°");
                    this.text.Strings.Add("φ：" + this.drawingWord.Camera.Phi + "°");
                    this.text.Strings.Add("Ｒ：" + string.Format("{0:F2}", this.drawingWord.Camera.Radius) + "°");
                    this.drawingWord.Draw(this.text);
                }
                this.drawingWord.EndScene();

                System.Threading.Thread.Sleep(1);
            }
        }
    }
}
