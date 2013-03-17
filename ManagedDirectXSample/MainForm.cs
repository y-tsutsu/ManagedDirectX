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
    public partial class MainForm : Form
    {
        /// <summary>
        /// 描画空間
        /// </summary>
        private DrawingWorld drawingWord = null;

        /// <summary>
        /// 描画アイテム群
        /// </summary>
        private List<DrawingSphere> spheres = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            this.drawingWord = new DrawingWorld(this.panelCanvas);
            if (!this.drawingWord.Initialize())
            {
                MessageBox.Show("そんなバカな！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.spheres = new List<DrawingSphere>();
            spheres.Add(this.drawingWord.Factory.CreateSphere(1.0f, 32, 32));

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

                foreach (var item in this.spheres)
                {
                    item.Color = Color.Red;
                    this.drawingWord.Draw(item);
                }

                this.drawingWord.EndScene();
                System.Threading.Thread.Sleep(1);
            }
        }
    }
}
