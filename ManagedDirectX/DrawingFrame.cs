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
    /// 枠の描画アイテム
    /// </summary>
    public class DrawingFrame : IDrawable
    {
        /// <summary>
        /// 頂点バッファ
        /// </summary>
        private VertexBuffer vertex;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="device">デバイス</param>
        /// <param name="width">幅</param>
        /// <param name="height">高さ</param>
        /// <param name="depth">奥行き</param>
        /// <param name="color">色</param>
        public DrawingFrame(Device device, float width, float height, float depth, Color color)
        {
            this.vertex = new VertexBuffer(
                typeof(CustomVertex.PositionColored), 24, device, Usage.None, CustomVertex.PositionColored.Format, Pool.Managed);

            var w = width / 2.0f;
            var h = height / 2.0f;
            var d = depth / 2.0f;
            var vertices = new[]
            {
                new CustomVertex.PositionColored(-w, -h, -d, color.ToArgb()),
                new CustomVertex.PositionColored(+w, -h, -d, color.ToArgb()),
                new CustomVertex.PositionColored(-w, -h, -d, color.ToArgb()),
                new CustomVertex.PositionColored(-w, +h, -d, color.ToArgb()),
                new CustomVertex.PositionColored(-w, -h, -d, color.ToArgb()),
                new CustomVertex.PositionColored(-w, -h, +d, color.ToArgb()),
                new CustomVertex.PositionColored(+w, +h, -d, color.ToArgb()),
                new CustomVertex.PositionColored(-w, +h, -d, color.ToArgb()),
                new CustomVertex.PositionColored(+w, +h, -d, color.ToArgb()),
                new CustomVertex.PositionColored(+w, -h, -d, color.ToArgb()),
                new CustomVertex.PositionColored(+w, +h, -d, color.ToArgb()),
                new CustomVertex.PositionColored(+w, +h, +d, color.ToArgb()),
                new CustomVertex.PositionColored(-w, +h, +d, color.ToArgb()),
                new CustomVertex.PositionColored(+w, +h, +d, color.ToArgb()),
                new CustomVertex.PositionColored(-w, +h, +d, color.ToArgb()),
                new CustomVertex.PositionColored(-w, -h, +d, color.ToArgb()),
                new CustomVertex.PositionColored(-w, +h, +d, color.ToArgb()),
                new CustomVertex.PositionColored(-w, +h, -d, color.ToArgb()),
                new CustomVertex.PositionColored(+w, -h, +d, color.ToArgb()),
                new CustomVertex.PositionColored(-w, -h, +d, color.ToArgb()),
                new CustomVertex.PositionColored(+w, -h, +d, color.ToArgb()),
                new CustomVertex.PositionColored(+w, +h, +d, color.ToArgb()),
                new CustomVertex.PositionColored(+w, -h, +d, color.ToArgb()),
                new CustomVertex.PositionColored(+w, -h, -d, color.ToArgb()),
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
            device.DrawPrimitives(PrimitiveType.LineList, 0, 12);

            device.RenderState.Lighting = true;
        }
    }
}
