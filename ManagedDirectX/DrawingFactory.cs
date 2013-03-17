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

        /// <summary>
        /// 枠の生成
        /// </summary>
        /// <param name="device">デバイス</param>
        /// <param name="width">幅</param>
        /// <param name="height">高さ</param>
        /// <param name="depth">奥行き</param>
        /// <param name="color">色</param>
        /// <returns>生成した枠</returns>
        public DrawingFrame CreateFrame(float width, float height, float depth, Color color)
        {
            return new DrawingFrame(this.device, width, height, depth, color);
        }

        /// <summary>
        /// XYZ軸の生成
        /// </summary>
        /// <param name="length">長さ</param>
        /// <returns>生成したXYZ軸</returns>
        public DrawingXYZAxis CreateXYZAxis(float length)
        {
            return new DrawingXYZAxis(this.device, length);
        }

        /// <summary>
        /// テキストの生成
        /// </summary>
        /// <param name="height">文字の高さ</param>
        /// <param name="fontName">フォント名</param>
        /// <param name="color">色</param>
        /// <returns>生成したテキスト</returns>
        public DrawingText CreateText(int height, string fontName, Color color)
        {
            return new DrawingText(this.device, height, fontName, color);
        }
    }
}
