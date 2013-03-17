using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace ManagedDirectX
{
    /// <summary>
    /// XYZ軸の描画アイテム
    /// </summary>
    public class DrawingXYZAxis : IDrawable
    {
        /// <summary>
        /// 頂点バッファ
        /// </summary>
        private VertexBuffer vertex;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="device">デバイス</param>
        /// <param name="length">長さ</param>
        public DrawingXYZAxis(Device device, float length)
        {
            this.vertex = new VertexBuffer(
                typeof(CustomVertex.PositionColored), 6, device, Usage.None, CustomVertex.PositionColored.Format, Pool.Managed);

            var vertices = new[]
            {
                new CustomVertex.PositionColored(0.0f, 0.0f, 0.0f, Color.Red.ToArgb()),
                new CustomVertex.PositionColored(length, 0.0f, 0.0f, Color.Red.ToArgb()),
                new CustomVertex.PositionColored(0.0f, 0.0f, 0.0f, Color.Green.ToArgb()),
                new CustomVertex.PositionColored(0.0f, length, 0.0f, Color.Green.ToArgb()),
                new CustomVertex.PositionColored(0.0f, 0.0f, 0.0f, Color.Blue.ToArgb()),
                new CustomVertex.PositionColored(0.0f, 0.0f, length, Color.Blue.ToArgb())
            };

            using (GraphicsStream data = this.vertex.Lock(0, 0, LockFlags.None))
            {
                data.Write(vertices);
                this.vertex.Unlock();
            }
        }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="device">デバイス</param>
        public void Draw(Device device)
        {
            device.RenderState.Lighting = false;

            device.SetTransform(TransformType.World, Matrix.Identity);
            device.SetStreamSource(0, this.vertex, 0);
            device.VertexFormat = CustomVertex.PositionColored.Format;
            device.DrawPrimitives(PrimitiveType.LineList, 0, 3);

            device.RenderState.Lighting = true;
        }
    }
}
