using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B21_Ex05_01
{
    public class TicTacToeLogic
    {
        private Board m_GameBoard;
        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private readonly TicTacToeEnums.eGameMode r_GameMode;
        private int m_CountTurns;

        public TicTacToeLogic(TicTacToeEnums.eGameMode i_SelectedGameMode, int i_BoardSize, string i_PlayerOneName, string i_PlayerTwoName)
        {
            bool twoPlayers = i_SelectedGameMode == TicTacToeEnums.eGameMode.SinglePlayer;
            m_GameBoard = new Board(i_BoardSize);
            m_PlayerOne = new Player(true, true, TicTacToeEnums.ePlayerSign.X, i_PlayerOneName);
            m_PlayerTwo = new Player(false, !twoPlayers, TicTacToeEnums.ePlayerSign.O, i_PlayerTwoName);
            m_CountTurns = 0;
            r_GameMode = i_SelectedGameMode;
        }

        public ref Board GameBoard
        {
            get
            {

                return ref m_GameBoard;
            }
        }
        public ref Player PlayerOne
        {
            get
            {

                return ref m_PlayerOne;
            }
        }

        public ref Player PlayerTwo
        {
            get
            {

                return ref m_PlayerTwo;
            }
        }

        public TicTacToeEnums.eGameMode IsSinglePlayerMode
        {
            get
            {

                return r_GameMode;
            }
        }

        public int Turns
        {
            get
            {

                return m_CountTurns;
            }
        }

        public void InitGame()
        {
            initBoard();
            m_CountTurns = 0;
            initPlayersTurn();
        }

        private void initBoard()
        {
            for (int i = 0; i < m_GameBoard.Size; i++)
            {
                for (int j = 0; j < m_GameBoard.Size; j++)
                {
                    m_GameBoard[i, j] = TicTacToeEnums.ePlayerSign.Empty;
                }
            }
        }

        public static bool ValidBoardSize(int i_Size)
        {

            return i_Size >= 3 && i_Size <= 9;
        }

        private void initPlayersTurn()
        {
            if (r_GameMode == TicTacToeEnums.eGameMode.SinglePlayer)
            {
                if (m_GameBoard.Size % 2 == 0)
                {
                    m_PlayerOne.Turn = true;
                    m_PlayerTwo.Turn = !m_PlayerOne.Turn;
                }
                else
                {
                    m_PlayerTwo.Turn = true;
                    m_PlayerOne.Turn = !m_PlayerTwo.Turn;
                }
            }
        }

        public void computerMove(ref int i_PrevRow, ref int i_PrevCol)
        {
            int sizeOfBoard = m_GameBoard.Size - 1;

            if (m_CountTurns == 0)
            {
                i_PrevRow = sizeOfBoard / 2;
                i_PrevCol = i_PrevRow;
            }////if the board is empty and the size of board is odd - the computer will choose the middle cell.
            else
            {
                i_PrevRow = sizeOfBoard - i_PrevRow;
                i_PrevCol = sizeOfBoard - i_PrevCol;
            }////the computer will choose the symmetric cell to the prev step of player one.
        }

        public void MakeAMove(ref int io_Row, ref int io_Col)
        {
            if (m_PlayerOne.Turn)
            {
                m_GameBoard[io_Row, io_Col] = m_PlayerOne.PlayerSign;
            }
            else
            {
                m_GameBoard[io_Row, io_Col] = m_PlayerTwo.PlayerSign;
            }

            m_PlayerOne.Turn = !m_PlayerOne.Turn;
            m_PlayerTwo.Turn = !m_PlayerTwo.Turn;
            m_CountTurns++;
        }

        public bool CheckWin()
        {
            int sizeOfBoard = m_GameBoard.Size;
            bool roundWin = checkFullUpDiagonal(sizeOfBoard) || checkFullDownDiagonal(sizeOfBoard);

            for (int i = 0; i < sizeOfBoard && !roundWin; i++)
            {
                roundWin = checkFullRow(i, sizeOfBoard) || checkFullCol(i, sizeOfBoard);
            }

            return !roundWin;
        }

        public bool CheckDraw(int i_CountTurns)
        {

            return (i_CountTurns == m_GameBoard.Size * m_GameBoard.Size);
        }

        private bool checkFullRow(int i_Row, int i_Size)
        {
            bool fullRow = true;
            TicTacToeEnums.ePlayerSign signInRow = m_GameBoard[i_Row, 0];

            for (int i = 0; i < i_Size && fullRow; i++)
            {
                if (m_GameBoard[i_Row, i] == TicTacToeEnums.ePlayerSign.Empty || signInRow != m_GameBoard[i_Row, i])
                {
                    fullRow = !fullRow;
                }
            }

            return fullRow;
        }

        private bool checkFullCol(int i_Col, int i_Size)
        {
            bool fullRow = true;
            TicTacToeEnums.ePlayerSign signInRow = m_GameBoard[0, i_Col];

            for (int i = 0; i < i_Size && fullRow; i++)
            {
                if (m_GameBoard[i, i_Col] == TicTacToeEnums.ePlayerSign.Empty || signInRow != m_GameBoard[i, i_Col])
                {
                    fullRow = !fullRow;
                }
            }

            return fullRow;
        }

        private bool checkFullUpDiagonal(int i_Size)
        {
            bool fullRow = true;
            TicTacToeEnums.ePlayerSign signInRow = m_GameBoard[0, i_Size - 1];

            for (int i = 0, j = i_Size - 1; i < i_Size && fullRow; i++, j--)
            {
                if (m_GameBoard[i, j] == TicTacToeEnums.ePlayerSign.Empty || signInRow != m_GameBoard[i, j])
                {
                    fullRow = !fullRow;
                }
            }

            return fullRow;
        }

        private bool checkFullDownDiagonal(int i_Size)
        {
            bool fullRow = true;
            TicTacToeEnums.ePlayerSign signInRow = m_GameBoard[0, 0];

            for (int i = 0; i < i_Size && fullRow; i++)
            {
                if (m_GameBoard[i, i] == TicTacToeEnums.ePlayerSign.Empty || signInRow != m_GameBoard[i, i])
                {
                    fullRow = !fullRow;
                }
            }

            return fullRow;
        }
    }
}
