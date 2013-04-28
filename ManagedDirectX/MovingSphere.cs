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
    /// 球の移動モデル
    /// </summary>
    public class MovingSphere
    {
        /// <summary>
        /// 位置
        /// </summary>
        private Vector3 location;

        /// <summary>
        /// 方向
        /// </summary>
        private Vector3 direction;

        /// <summary>
        /// 速度
        /// </summary>
        private float speed = 0;

        /// <summary>
        /// 乱数生成用
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// 球の半径
        /// </summary>
        internal float Radius { get; private set; }

        /// <summary>
        /// 位置アクセサ
        /// </summary>
        public Point3D Location { get { return new Point3D(this.location); } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="location">位置</param>
        public MovingSphere(Point3D location, float radius)
        {
            this.location = location.ToVector3();
            this.Radius = radius;
            this.direction = new Vector3((float)MovingSphere.random.NextDouble(), (float)MovingSphere.random.NextDouble(), (float)MovingSphere.random.NextDouble());
            this.speed = MovingSphere.random.Next(30, 50) / 50.0f;
        }

        /// <summary>
        /// 移動する
        /// </summary>
        public void Move()
        {
            this.location -= this.direction * this.speed;
        }

        /// <summary>
        /// 反射する
        /// </summary>
        /// <param name="wallNormal"></param>
        internal void Reflect(Vector3 wallNormal)
        {
            if (wallNormal.X != 0)
            {
                this.direction.X *= -1;
            }
            else if (wallNormal.Y != 0)
            {
                this.direction.Y *= -1;
            }
            else if (wallNormal.Z != 0)
            {
                this.direction.Z *= -1;
            }
        }
    }
}
