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
    /// 描画アイテムの工場
    /// </summary>
    public class DrawingFactory
    {
        /// <summary>
        /// デバイス
        /// </summary>
        private Device device = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="device">デバイス</param>
        public DrawingFactory(Device device)
        {
            this.device = device;
        }

        /// <summary>
        /// 球の生成
        /// </summary>
        /// <param name="radius">球の半径</param>
        /// <param name="slices">主軸を回転軸としたスライスの数</param>
        /// <param name="stacks">主軸に沿ったスタックの数</param>
        /// <param name="color">球の色</param>
        /// <returns>生成した球</returns>
        public DrawingSphere CreateSphere(float radius, int slices, int stacks, Color color)
        {
            return new DrawingSphere(this.device, radius, slices, stacks, color);
        }
    }
}
