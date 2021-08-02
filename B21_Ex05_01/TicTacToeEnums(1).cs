using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B21_Ex05_01
{
    public class TicTacToeEnums
    {
        public enum eGameMode
        {
            SinglePlayer = 1,
            TwoPlayers = 2,
        }

        public enum ePlayerSign
        {
            Empty,
            X,
            O,
        }
    }
}