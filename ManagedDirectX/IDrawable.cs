using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace ManagedDirectX
{
    /// <summary>
    /// 描画するアイテムはIDrawableを実装する．
    /// ManagedDirectX.Drawメソッドで描画を行う．
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="device">デバイス</param>
        void Draw(Device device);
    }
}
