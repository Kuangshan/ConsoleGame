using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public sealed class CMouseEventArgs:EventArgs
    {
        private int x, y;
        private bool leave;
        private CMouseButtons vKey;

        public CMouseEventArgs(int x, int y, bool leave)
        {
            this.x = x;
            this.y = y;
            this.leave = leave;
        }
        public CMouseEventArgs(int x, int y, CMouseButtons key)
        {
            this.x = x;
            this.y = y;
            this.vKey = key;
        }
        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        public bool isLeave()
        {
            return leave;
        }

        public CMouseButtons getKey()
        {
            return vKey;
        }

        public bool ContainKey(CMouseButtons key)
        {
            return (getKey() & key) == key;
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", x, y);
        }
    }
}
