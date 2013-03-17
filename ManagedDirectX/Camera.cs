using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace ManagedDirectX
{
    /// <summary>
    /// カメラ
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// 前回のマウスのカーソル位置
        /// </summary>
        private Point oldMouseLocation;

        /// <summary>
        /// 半径
        /// </summary>
        public float Radius { get; private set; }

        /// <summary>
        /// θ（X-Z）
        /// </summary>
        public float Theta { get; private set; }

        /// <summary>
        /// φ（X-Y）
        /// </summary>
        public float Phi { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="device">デバイス</param>
        /// <param name="radius">半径</param>
        /// <param name="theta">θ</param>
        /// <param name="phi">φ</param>
        internal Camera(Device device, float radius, float theta, float phi)
        {
            this.Radius = radius;
            this.Theta = theta;
            this.Phi = phi;
            device.Transform.Projection = Matrix.PerspectiveFovLH(Geometry.DegreeToRadian(60.0f),
                (float)device.Viewport.Width / (float)device.Viewport.Height, 1.0f, 100.0f);
        }

        /// <summary>
        /// マウスムーブ入力
        /// </summary>
        /// <param name="mouseLocation">マウスポイント</param>
        /// <param name="button">マウスボタン</param>
        internal void InputMouseMove(Point mouseLocation, MouseButtons button)
        {
            if (button == MouseButtons.Left)
            {
                this.Theta -= mouseLocation.X - this.oldMouseLocation.X;
                this.Phi += mouseLocation.Y - this.oldMouseLocation.Y;

                if (90.0f <= this.Phi)
                {
                    this.Phi = 89.9999f;
                }
                else if (this.Phi <= -90.0f)
                {
                    this.Phi = -89.9999f;
                }
            }
            this.oldMouseLocation = mouseLocation;
        }

        /// <summary>
        /// マウスホイール入力
        /// </summary>
        /// <param name="delta">Δ</param>
        internal void InputMouseWheel(int delta)
        {
            this.Radius -= delta / 480.0f;
            if (this.Radius < 4.0f)
            {
                this.Radius = 4.0f;
            }
        }

        /// <summary>
        /// カメラ設定を更新
        /// </summary>
        /// <param name="device">デバイス</param>
        internal void Update(Device device)
        {
            // レンズの位置を三次元極座標で変換
            float theta = Geometry.DegreeToRadian(this.Theta);
            float phi = Geometry.DegreeToRadian(this.Phi);
            var lensLocation = new Vector3((float)(this.Radius * Math.Cos(theta) * Math.Cos(phi)),
                (float)(this.Radius * Math.Sin(phi)), (float)(this.Radius * Math.Sin(theta) * Math.Cos(phi)));

            // ビュー変換行列を左手座標系ビュー行列で設定する
            device.Transform.View = Matrix.LookAtLH(lensLocation, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
        }
    }
}
