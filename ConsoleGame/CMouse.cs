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
    internal sealed class CMouse : CInput
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

        public CMouse(IntPtr hwnd)
        {
            this.hwnd = hwnd;
            this.oldPoint = new CPoint(0, 0);
            this.leave = false;

            this.MAX_X = (Console.WindowWidth << 3) - 1;
            this.MAX_Y = Console.WindowHeight << 4;
        }
        #region 鼠标函数
        /// <summary>
        /// 是否按下鼠标
        /// </summary>
        /// <param name="vKey"></param>
        /// <returns></returns>
        private bool IsMouseDown(CMouseButtons vKey)
        {
            return 0 != (GetAsyncKeyState((int)vKey) & KEY_STATE);
        }
        private CMouseButtons GetCurMouseDownKeys()
        {
            CMouseButtons vKey = CMouseButtons.None;
            foreach (int key in Enum.GetValues(typeof(CMouseButtons)))
            {
                if (IsMouseDown((CMouseButtons)key))
                {
                    vKey |= (CMouseButtons)key;//按位取或 允许多个键同时按下
                }
            }
            return vKey;
        }

        /// <summary>  
        /// 获取鼠标坐标  
        /// </summary>  
        /// <returns></returns>  
        private CPoint GetMousePoint()
        {
            CPoint point;
            //获取鼠标在屏幕的位置  
            if (GetCursorPos(out point))
            {
                if (hwnd != IntPtr.Zero)
                {
                    //把屏幕位置转换成控制台工作区位置   
                    ScreenToClient(hwnd, out point);

                    if ((point.X >= 0 && point.Y <= MAX_X)
                        && point.Y >= 0 && point.Y <= MAX_Y)
                    {
                        this.oldPoint = point;
                        this.leave = false;
                    }
                    else
                    {
                        leave = true;
                    }
                }
            }
            return oldPoint;
        }

        private int GetMouseX()
        {
            return GetMousePoint().X;
        }
        private int GetMouseY()
        {
            return GetMousePoint().Y;
        }

        private bool IsLeave()
        {
            return leave;
        }

        #endregion
        #region 鼠标事件
        private void OnMouseMove(CMouseEventArgs e)
        {
            CMouseHandler<CMouseEventArgs> temp = mouseMove;
            if (temp != null)
            {
                temp.Invoke(e);
            }
        }

        private void OnMouseAway(CMouseEventArgs e)
        {
            CMouseHandler<CMouseEventArgs> temp = mouseAway;
            if (temp != null)
            {
                temp.Invoke(e);
            }
        }

        private void OnMouseDown(CMouseEventArgs e)
        {
            CMouseHandler<CMouseEventArgs> temp = mouseDown;
            if (temp != null)
            {
                temp.Invoke(e);
            }
        }

        public void AddMouseMoveEvent(CMouseHandler<CMouseEventArgs> func)
        {
            mouseMove += func;
        }
        public void AddMouseAwayEvent(CMouseHandler<CMouseEventArgs> func)
        {
            mouseAway += func;
        }
        public void AddMouseDownEvent(CMouseHandler<CMouseEventArgs> func)
        {
            mouseDown += func;
        }

        public void mouseEventsHandler()
        {
            CMouseEventArgs e;
            CPoint point = GetMousePoint();
            CMouseButtons vKey = GetCurMouseDownKeys();

            if (!IsLeave())
            {
                if (vKey != CMouseButtons.None)
                {
                    e = new CMouseEventArgs(point.X, point.Y, vKey);
                    this.OnMouseDown(e);
                }
                e = new CMouseEventArgs(point.X, point.Y, false);
                this.OnMouseMove(e);
            }
            else {
                e = new CMouseEventArgs(-1, -1, true);
                this.OnMouseAway(e);
            }
        }

        #endregion
    }
}
