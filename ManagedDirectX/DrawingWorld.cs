using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace ManagedDirectX
{
    /// <summary>
    /// 描画空間
    /// </summary>
    public class DrawingWorld : IDisposable
    {
        /// <summary>
        /// Direct3D デバイス
        /// </summary>
        private Device device = null;

        /// <summary>
        /// 描画対象物
        /// </summary>
        private Control canvas = null;

        /// <summary>
        /// カメラ
        /// </summary>
        private Camera camera = null;

        /// <summary>
        /// カメラアクセサ
        /// </summary>
        public Camera Camera { get { return this.camera; } }

        /// <summary>
        /// 照明
        /// </summary>
        private Lighting lighting = null;

        /// <summary>
        /// 描画アイテム工場
        /// </summary>
        private DrawingFactory factory = null;

        /// <summary>
        /// 描画アイテム工場アクセサ
        /// </summary>
        public DrawingFactory Factory
        {
            get { return this.factory; }
        }

        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// 描画スレッド
        /// </summary>
        private BackgroundWorker backgroundWorker;

        /// <summary>
        /// 描画中かどうか？
        /// </summary>
        public bool IsBusy { get { return this.backgroundWorker.IsBusy; } }

        /// <summary>
        /// 描画時イベント
        /// </summary>
        public event EventHandler Drawing;

        /// <summary>
        /// 描画時イベント発行
        /// </summary>
        /// <param name="e"></param>
        private void OnDrawing(EventArgs e)
        {
            if (this.Drawing != null)
            {
                this.Drawing(this, e);
            }
        }

        /// <summary>
        /// フレームレート（fps）
        /// </summary>
        public int FrameRate { private get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="canvas">描画対象物</param>
        public DrawingWorld(Control canvas)
        {
            this.backgroundWorker = new BackgroundWorker();
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += this.BackgroundWorkerDoWork;

            this.FrameRate = 1;

            this.canvas = canvas;
            this.BackColor = Color.LightSteelBlue;

            this.canvas.MouseMove += (object sender, MouseEventArgs e) =>
                {
                    if (this.camera == null) return;
                    this.camera.InputMouseMove(e.Location, e.Button);
                };
            this.canvas.MouseWheel += (object sender, MouseEventArgs e) =>
                {
                    if (this.camera == null) return;
                    this.camera.InputMouseWheel(e.Delta);
                };
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <returns>デバイス作成の成功or失敗</returns>
        public bool Initialize()
        {
            var param = new PresentParameters();
            param.Windowed = true;
            param.SwapEffect = SwapEffect.Discard;
            param.EnableAutoDepthStencil = true;
            param.AutoDepthStencilFormat = DepthFormat.D24S8;

            try
            {
                // ハードウェアによる頂点処理．最高パフォーマンス．
                this.device = new Device(0, DeviceType.Hardware, this.canvas.Handle, CreateFlags.HardwareVertexProcessing, param);
            }
            catch (Exception)
            {
                try
                {
                    // ソフトウェアによる頂点処理．
                    this.device = new Device(0, DeviceType.Hardware, this.canvas.Handle, CreateFlags.SoftwareVertexProcessing, param);
                }
                catch (Exception)
                {
                    try
                    {
                        // ソフトウェアによる頂点処理．低パフォーマンス
                        this.device = new Device(0, DeviceType.Reference, this.canvas.Handle, CreateFlags.SoftwareVertexProcessing, param);
                    }
                    catch (Exception)
                    {
                        // デバイス作成不可
                        return false;
                    }
                }
            }

            this.camera = new Camera(device, 20.0f, 300.0f, 30.0f);
            this.lighting = new Lighting(device);
            this.factory = new DrawingFactory(device);

            this.device.RenderState.CullMode = Cull.None;

            return true;
        }

        /// <summary>
        /// リソース破棄
        /// </summary>
        public void Dispose()
        {
            if (this.device != null)
            {
                this.device.Dispose();
                this.device = null;
            }
        }

        /// <summary>
        /// 描画スタート
        /// </summary>
        public void Start()
        {
            if (!this.backgroundWorker.IsBusy)
            {
                this.backgroundWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public void Pause()
        {
            if (this.backgroundWorker.IsBusy)
            {
                this.backgroundWorker.CancelAsync();
            }
        }

        /// <summary>
        /// 描画スレッド
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                var start = DateTime.Now;

                if (this.backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                this.BeginScene();
                this.OnDrawing(new EventArgs());
                this.EndScene();

                var msT = 1000.0 / this.FrameRate;
                while (true)
                {
                    var span = DateTime.Now - start;
                    if (msT < span.TotalMilliseconds) break;
                    System.Threading.Thread.Sleep(1);
                }
            }
        }

        /// <summary>
        /// 描画前処理
        /// </summary>
        private void BeginScene()
        {
            if (this.device == null) return;

            this.camera.Update(this.device);

            this.device.Clear(ClearFlags.Target | ClearFlags.ZBuffer | ClearFlags.Stencil, this.BackColor, 1.0f, unchecked((int)0xFFFFFFFF));
            this.device.BeginScene();
        }

        /// <summary>
        /// 描画後処理
        /// </summary>
        private void EndScene()
        {
            if (this.device == null) return;

            this.device.EndScene();
            this.device.Present();
        }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="drawable">描画アイテム</param>
        public void Draw(IDrawable drawable)
        {
            if (this.device == null) return;

            drawable.Draw(this.device);
        }
    }
}
