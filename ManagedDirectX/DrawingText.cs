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
    /// テキストの描画アイテム
    /// </summary>
    public class DrawingText : IDrawable
    {
        /// <summary>
        /// フォント
        /// </summary>
        private Microsoft.DirectX.Direct3D.Font font = null;

        /// <summary>
        /// 表示テキスト
        /// </summary>
        public List<string> Strings { get; set; }

        /// <summary>
        /// 文字の色
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="device">デバイス</param>
        /// <param name="height">文字の高さ</param>
        /// <param name="fontName">フォント名</param>
        /// <param name="color">色</param>
        public DrawingText(Device device, int height, string fontName, Color color)
        {
            var description = new FontDescription();
            description.Height = height;
            description.FaceName = fontName;
            this.font = new Microsoft.DirectX.Direct3D.Font(device, description);

            this.Color = color;

            this.Strings = new List<string>();
        }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="device">デバイス</param>
        public void Draw(Device device)
        {
            int count = 0;
            foreach (var item in this.Strings)
            {
                this.font.DrawText(null, item, 2, this.font.Description.Height * count, this.Color);
                count++;
            }
        }
    }
}
