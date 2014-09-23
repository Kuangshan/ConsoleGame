using CGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    /// <summary>
    /// 鼠标键值
    /// </summary>
    [Flags]
    public enum CMouseButtons
    {
        Left = 0x01,
        Middle = 0x04,
        None = 0,
        Right = 0x02
    }
    /// <summary>
    /// 鼠标类
    /// </summary>
    internal sealed class CMouse:CInput
    {
        internal delegate void CMouseHandler<TEvnetArgs>(TEvnetArgs e);
        private event CMouseHandler<CMouseEventArgs> mouseMove;
        private event CMouseHandler<CMouseEventArgs> mouseAway;
        private event CMouseHandler<CMouseEventArgs> mouseDown;

        /// <summary>  
        /// 最大X值  
        /// </summary>  
        private readonly int MAX_X = 639;
        /// <summary>  
        /// 最大Y值  
        /// </summary>  
        private readonly int MAX_Y = 400;
        /// <summary>
        /// 控制台句柄
        /// </summary>
        private IntPtr hwnd = IntPtr.Zero;

        private CPoint oldPoint;
        private bool leave;


    }
}
