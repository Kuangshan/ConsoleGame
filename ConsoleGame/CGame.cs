using System;
using System.Threading;
using System.Runtime.InteropServices;

namespace ConsoleGame
{
    /// <summary>
    /// 游戏的父类
    /// </summary>
    public abstract class CGame:ICGame
    {
        #region Api函数
        [DllImport("User32.dll")]
        private static extern IntPtr FindWindow(String lpClassName, String lpWindowName);  
        #endregion

        #region 字段
        /// <summary>
        /// 控制台句柄
        /// </summary>
        private IntPtr hwnd = IntPtr.Zero;
        /// <summary>
        /// 鼠标输入设备
        /// </summary>
        private CMouse dcMouse;
        /// <summary>
        /// 键盘输入设备
        /// </summary>
        private CKeyBoard dcKeyboard;

        /// <summary>
        /// 画面更新速度
        /// </summary>
        private int updateRate;

        public int UpdateRate
        {
            get { return updateRate; }
            set { updateRate = value; }
        }
        /// <summary>
        /// 帧率
        /// </summary>
        private int fps;

        public int Fps
        {
            get {
                int ticks = Environment.TickCount;
                tickCount += 1;
                if (ticks - lastTime >= 1000)
                {
                    fps = tickCount;
                    tickCount = 0;
                    lastTime = ticks;
                }
                return fps;
            }
        }
        /// <summary>
        /// 帧数计数
        /// </summary>
        private int tickCount;
        /// <summary>
        /// 上次运行时间
        /// </summary>
        private int lastTime;
        /// <summary>
        /// 游戏结束标志
        /// </summary>
        private bool isGameOver;

        public bool IsGameOver
        {
            get { return isGameOver; }
            set { isGameOver = value; }
        }
        #endregion

        #region 运行方法
        /// <summary>
        /// 构造方法
        /// </summary>
        public CGame()
        {
            isGameOver = false;

            hwnd = FindWindow(null, Console.Title);
            dcMouse = new CMouse(hwnd);
            dcKeyboard = new CKeyBoard();

            dcMouse.AddMouseMoveEvent(GameMouseMove);
            dcMouse.AddMouseAwayEvent(GameMouseAway);
            dcMouse.AddMouseDownEvent(GameMouseDown);

            dcKeyboard.AddKeyDownEvent(GameKeyDown);
            dcKeyboard.AddKeyUpEvent(GameKeyUp);
        }

        
        #region 游戏输入事件
        protected virtual void GameMouseMove(CMouseEventArgs e)
        {
            
        }
        protected virtual void GameMouseAway(CMouseEventArgs e)
        {

        }
        protected virtual void GameMouseDown(CMouseEventArgs e)
        {

        }
        protected virtual void GameKeyDown(CKeyboardEventArgs e)
        {

        }
        protected virtual void GameKeyUp(CKeyboardEventArgs e)
        {

        }
        #endregion
        private void GameInput()
        {
            this.GetMouseDevice().mouseEventsHandler();
            this.GetKeyboardDevice().KeyBoardEventsHandler();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected abstract void GameInit();
        /// <summary>
        /// 游戏主循环
        /// </summary>
        protected abstract void GameLoop();
        /// <summary>
        /// 游戏退出
        /// </summary>
        protected abstract void GameExit();
        #endregion

        #region 游戏输入设备
        internal CMouse GetMouseDevice()
        {
            return dcMouse;
        }
        internal CKeyBoard GetKeyboardDevice()
        {
            return dcKeyboard;
        }
        #endregion

        #region 游戏设置方法
        /// <summary>
        /// 延迟1毫秒
        /// </summary>
        private void Delay()
        {
            this.Delay(1);
        }
        /// <summary>
        /// 延迟指定毫秒
        /// </summary>
        /// <param name="time">延迟毫秒数</param>
        protected void Delay(int time)
        {
            Thread.Sleep(time);
        }
        /// <summary>
        /// 设置是否显示光标
        /// </summary>
        /// <param name="visible"></param>
        protected void SetCursorVisible(bool visible)
        {
            Console.CursorVisible = visible;
        }
        /// <summary>
        /// 关闭游戏并释放资源
        /// </summary>
        private void Close()
        { }
        /// <summary>
        /// 设置控制台标题
        /// </summary>
        /// <param name="title"></param>
        protected void SetTitle(string title)
        {
            Console.Title = title;
        }
        #endregion

        #region 游戏启动接口
        public void Run()
        {
            this.GameInit();
            int startTime = 0;
            while (!isGameOver)
            {
                startTime = Environment.TickCount;
                GameInput();
                GameLoop();
                while (Environment.TickCount - startTime < updateRate)
                {
                    Delay();
                }
            }
            GameExit();
            Close();
        }
        #endregion
    }
}
