using System;
using System.Threading;

namespace ConsoleGame
{
    /// <summary>
    /// 游戏的父类
    /// </summary>
    public abstract class CGame:ICGame
    {
        #region 字段
        /// <summary>
        /// 画面更新速度
        /// </summary>
        private int updateRate;
        /// <summary>
        /// 帧率
        /// </summary>
        private int fps;
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
        #endregion

        #region 运行方法
        /// <summary>
        /// 构造方法
        /// </summary>
        public CGame()
        {
            isGameOver = false;
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

        #region 游戏设置方法


        #endregion

        /// <summary>
        /// 实现接口
        /// </summary>
        public void run()
        { 
            int a = 0;
                 
        
        }
    }
}
