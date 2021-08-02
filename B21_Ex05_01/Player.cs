using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B21_Ex05_01
{
    public struct Player
    {
        private readonly string r_Name;
        private int m_Score;
        private bool m_MyTurn;
        private readonly bool r_Human;
        private readonly TicTacToeEnums.ePlayerSign r_PlayerSign;

        public Player(bool i_Turn, bool i_Human, TicTacToeEnums.ePlayerSign i_Sign, string i_Name)
        {
            m_Score = 0;
            m_MyTurn = i_Turn;
            r_Human = i_Human;
            r_PlayerSign = i_Sign;
            r_Name = i_Name;
        }

        public string Name
        {
            get
            {
                return r_Name;
            }
        }

        public int Score
        {
            get
            {
                return m_Score;
            }
        }

        public void AddOnePoint()
        {
            m_Score++;
        }

        public bool Turn
        {
            get
            {

                return m_MyTurn;
            }

            set
            {
                m_MyTurn = value;
            }
        }

        public bool Human
        {
            get
            {

                return r_Human;
            }
        }

        public TicTacToeEnums.ePlayerSign PlayerSign
        {
            get
            {

                return r_PlayerSign;
            }
        }
    }
}
