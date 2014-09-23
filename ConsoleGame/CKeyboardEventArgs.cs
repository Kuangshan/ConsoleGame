using System;


namespace ConsoleGame
{
    public class CKeyboardEventArgs:EventArgs
    {
        private CKeys keys;
        public CKeyboardEventArgs(CKeys keys)
        {
            this.keys = keys;
        }

        public CKeys GetKey()
        {
            return keys;
        }
             
    }
}
