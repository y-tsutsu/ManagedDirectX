using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace ManagedDirectX.MovingModel
{
    /// <summary>
    /// 枠の反射モデル
    /// </summary>
    public class ReflectingFrame
    {
        /// <summary>
        /// 壁の法線
        /// </summary>
        private Vector3[] normals = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="width">幅</param>
        /// <param name="height">高さ</param>
        /// <param name="depth">奥行き</param>
        public ReflectingFrame(float width, float height, float depth)
        {
            this.normals = new[]
            {
                new Vector3(width / 2, 0, 0),
                new Vector3(-width / 2, 0, 0),
                new Vector3(0, height / 2, 0),
                new Vector3(0, -height / 2, 0),
                new Vector3(0, 0, depth / 2),
                new Vector3(0, 0, -depth / 2)
            };
        }

        /// <summary>
        /// 接触面の法線取得
        /// </summary>
        /// <param name="sphere">球</param>
        /// <returns>接触面の法線</returns>
        private Vector3[] GetContactNormals(MovingSphere sphere)
        {
            List<Vector3> result = new List<Vector3>();

            if (this.normals[0].X <= sphere.Location.X + sphere.Radius) result.Add(this.normals[0]);
            if (this.normals[1].X >= sphere.Location.X - sphere.Radius) result.Add(this.normals[1]);
            if (this.normals[2].Y <= sphere.Location.Y + sphere.Radius) result.Add(this.normals[2]);
            if (this.normals[3].Y >= sphere.Location.Y - sphere.Radius) result.Add(this.normals[3]);
            if (this.normals[4].Z <= sphere.Location.Z + sphere.Radius) result.Add(this.normals[4]);
            if (this.normals[5].Z >= sphere.Location.Z - sphere.Radius) result.Add(this.normals[5]);

            return result.ToArray(); ;
        }

        /// <summary>
        /// 球を反射する
        /// </summary>
        /// <param name="sphere">球</param>
        public void ReflectBy(MovingSphere sphere)
        {
            var normals = this.GetContactNormals(sphere);
            foreach (var item in normals)
            {
                sphere.Reflect(item);
            }
        }
    }
}
