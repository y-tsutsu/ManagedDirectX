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
    /// 照明
    /// </summary>
    class Lighting
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="device">デバイス</param>
        public Lighting(Device device)
        {
            device.RenderState.Lighting = true;

            device.Lights[0].Type = LightType.Directional;
            device.Lights[0].Diffuse = Color.White;
            device.Lights[0].Ambient = Color.FromArgb(255, 128, 128, 128);
            device.Lights[0].Enabled = true;
            device.Lights[0].Direction = Vector3.Normalize(new Vector3(0.0f, -1.0f, 0.0f));
            device.Lights[0].Update();
        }
    }
}
