using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B21_Ex05_01
{
    public class Board
    {
        private readonly int r_Size;
        public event ButtonClicked Changed;
        private readonly TicTacToeEnums.ePlayerSign[,] r_Board;

        public Board(int i_UserSize)
        {
            r_Size = i_UserSize;
            r_Board = new TicTacToeEnums.ePlayerSign[i_UserSize, i_UserSize];
        }

        public TicTacToeEnums.ePlayerSign this[int i_Row, int i_Col]
        {
            get
            {

                return r_Board[i_Row, i_Col];
            }

            set
            {
                r_Board[i_Row, i_Col] = value;
                OnChanged(i_Row, i_Col);
            }
        }

        public int Size
        {
            get
            {

                return r_Size;
            }
        }

        public bool ValidValue(int i_Value)
        {

            return (i_Value >= 0 && i_Value < r_Size);
        }

        protected virtual void OnChanged(int i_Row, int i_Col)
        {
            this.Changed?.Invoke(i_Row, i_Col);
        }
    }
}

