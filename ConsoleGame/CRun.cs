using System;
using ConsoleGame;
using Game;

namespace CRun
{
    class CRun
    {
        static void Main(string[] args)
        {
            CGameEngine.Run(new TestGame());
        }
    }
}
