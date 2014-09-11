using System;


namespace ConsoleGame
{
    public sealed class CGameEngine
    {
        public static void Run(ICGame game)
        {
            if (game == null)
            {
                Console.Write("引擎未初始化");
                Console.ReadLine();
            }
            else {
                game.Run();
            }
        }
    }
}
