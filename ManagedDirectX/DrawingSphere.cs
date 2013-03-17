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
    /// 球の描画アイテム
    /// </summary>
    public class DrawingSphere : IDrawable
    {
        /// <summary>
        /// メッシュ
        /// </summary>
        private Mesh mesh = null;

        /// <summary>
        /// マテリアル
        /// </summary>
        private Material material;

        /// <summary>
        /// 球の座標
        /// </summary>
        public Vector3 Location
        {
            set;
            private get;
        }

        /// <summary>
        /// 球の色
        /// </summary>
        public Color Color
        {
            set
            {
                this.material.Diffuse = value;
                this.material.Ambient = value;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="device">デバイス</param>
        /// <param name="radius">球の半径</param>
        /// <param name="slices">主軸を回転軸としたスライスの数</param>
        /// <param name="stacks">主軸に沿ったスタックの数</param>
        /// <param name="color">球の色</param>
        public DrawingSphere(Device device, float radius, int slices, int stacks, Color color)
        {
            this.mesh = Mesh.Sphere(device, radius, slices, stacks);
            this.Color = color;
        }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="device">デバイス</param>
        public void Draw(Device device)
        {
            device.Material = this.material;
            device.SetTransform(TransformType.World, Matrix.Translation(new Vector3(0.0f, 0.0f, 0.0f)));
            this.mesh.DrawSubset(0);
        }
    }
}
