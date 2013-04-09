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
    /// Vector3の代替クラス
    /// </summary>
    public class Point3D
    {
        /// <summary>
        /// X
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Y
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Z
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Point3D()
            : this(0, 0, 0)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="z">Z</param>
        public Point3D(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vector">Vector3</param>
        public Point3D(Vector3 vector)
            : this(vector.X, vector.Y, vector.Z)
        {
        }

        /// <summary>
        /// Vector3変換
        /// </summary>
        /// <returns>Vector3</returns>
        internal Vector3 ToVector3()
        {
            return new Vector3(this.X, this.Y, this.Z);
        }
    }
}
