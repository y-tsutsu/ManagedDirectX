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
    using MovingModel;

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
        /// テキスト
        /// </summary>
        private DrawingText text = null;

        /// <summary>
        /// ボール
        /// </summary>
        private DrawingSphere[] sphere = null;

        /// <summary>
        /// 球の移動モデル
        /// </summary>
        private MovingSphere[] movingSphere = null;

        /// <summary>
        /// 枠の反射モデル
        /// </summary>
        private ReflectingFrame reflectingFrame = null;

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

            this.xyzAxis = this.drawingWord.Factory.CreateXYZAxis(20.0f);
            this.text = this.drawingWord.Factory.CreateText(12, "ＭＳ ゴシック", Color.Crimson);

            this.sphere = new[]
                {
                    this.drawingWord.Factory.CreateSphere(1.0f, 32, 32, Color.SlateBlue),
                    this.drawingWord.Factory.CreateSphere(1.3f, 32, 32, Color.YellowGreen),
                    this.drawingWord.Factory.CreateSphere(1.5f, 32, 32, Color.DeepPink)
                };
            this.movingSphere = new[]
                {
                    new MovingSphere(new Point3D(), 1.0f),
                    new MovingSphere(new Point3D(), 1.5f),
                    new MovingSphere(new Point3D(), 2.0f)
                };

            this.frame = this.drawingWord.Factory.CreateFrame(20.0f, 15.0f, 12.0f, Color.Silver);
            this.reflectingFrame = new ReflectingFrame(20.0f, 15.0f, 12.0f);

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
                    this.drawingWord.Draw(this.xyzAxis);

                    for (int i = 0; i < this.sphere.Length; i++)
                    {
                        this.movingSphere[i].Move();
                        this.reflectingFrame.ReflectBy(this.movingSphere[i]);
                        this.sphere[i].Location = this.movingSphere[i].Location;
                        this.drawingWord.Draw(this.sphere[i]);
                    }

                    this.drawingWord.Draw(this.frame);

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

        /// <summary>
        /// カメラのX方向（プラス）移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonXPlus_Click(object sender, EventArgs e)
        {
            this.drawingWord.Camera.MoveXPlus();
        }

        /// <summary>
        /// カメラのX方向（マイナス）移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonXMinus_Click(object sender, EventArgs e)
        {
            this.drawingWord.Camera.MoveXMinus();
        }

        /// <summary>
        /// カメラのY方向（プラス）移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonYPlux_Click(object sender, EventArgs e)
        {
            this.drawingWord.Camera.MoveYPlus();
        }

        /// <summary>
        /// カメラのY方向（マイナス）移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonYMinus_Click(object sender, EventArgs e)
        {
            this.drawingWord.Camera.MoveYMinus();
        }
    }
}
