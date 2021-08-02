using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace B21_Ex05_01
{
    public delegate void ButtonClicked(int i_Row, int i_Col);

    public class FormGameWindow : Form
    {
        public FormGameSettings m_FormGameSettings;
        public MessageBox m_EndOfGameMessege;
        public ButtonBoardButton[,] m_Board;
        public TicTacToeLogic m_Game;
        public Label m_LablePlayer1;
        public Label m_LablePlayer2;

        public FormGameWindow()
        {       
            initializeComponent();
        }
       
        private void initializeComponent()
        {
            int pad = 5;
            int buttonSize = 50;
            this.Name = "TicTacToe Misere";
            this.Text = "TicTacToeMisere";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.m_FormGameSettings = new FormGameSettings();
            int size = Convert.ToInt32(m_FormGameSettings.BoardSize);
            this.m_Board = new ButtonBoardButton[size, size];
            ButtonBoardButton anchorButton = initButtonMatrix(size, buttonSize, pad);
            int clientSizeBottom = initLabelsLocation(Convert.ToInt32(anchorButton.Bottom), pad);
            this.ClientSize = new Size(anchorButton.Location.X + anchorButton.Width + pad, m_LablePlayer1.Bottom);
        }

        public void Run()
        {
            TicTacToeEnums.eGameMode gameMode = m_FormGameSettings.twoPlayersMode ? TicTacToeEnums.eGameMode.TwoPlayers : TicTacToeEnums.eGameMode.SinglePlayer;
            this.m_Game = new TicTacToeLogic(gameMode, Convert.ToInt32(m_FormGameSettings.BoardSize), m_FormGameSettings.Player1Name, m_FormGameSettings.Player2Name);
            this.m_Game.GameBoard.Changed += changeButtonStatus;
            if (m_FormGameSettings.DialogResult != DialogResult.OK)
            {
                Application.Exit();
            }
            else
            {
                CleanBoard();
                ShowDialog();
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            ButtonBoardButton myButton = sender as ButtonBoardButton;
            int row = myButton.Row, col = myButton.Col;
            m_Game.MakeAMove(ref row, ref col);
            if (!m_Game.CheckWin())
            {
                EndOfGame(winGame(), true);
            }
            else if (m_Game.CheckDraw(m_Game.Turns))
            {
                EndOfGame(string.Format("Tie!{0}", Environment.NewLine), false);
            }
            else
            {
                if (m_Game.IsSinglePlayerMode == TicTacToeEnums.eGameMode.SinglePlayer && m_Game.PlayerTwo.Turn)
                {
                    m_Game.computerMove(ref row, ref col);
                    button_Click(m_Board[row, col], e);
                }
            }
        }

        private ButtonBoardButton initButtonMatrix(int i_BoardSize, int i_ButtonSize, int i_PaddingSize)
        {
            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    m_Board[i, j] = new ButtonBoardButton();
                    m_Board[i, j].Row = i;
                    m_Board[i, j].Col = j;
                    m_Board[i, j].Size = new Size(i_ButtonSize, i_ButtonSize);
                    m_Board[i, j].Margin = new Padding(i_PaddingSize, i_PaddingSize, i_PaddingSize, i_PaddingSize);
                    m_Board[i, j].Location = new Point((i_PaddingSize + i_ButtonSize) * j + i_PaddingSize, (i_PaddingSize + i_ButtonSize) * i + i_PaddingSize);
                    m_Board[i, j].TabStop = false;
                    m_Board[i, j].Click += new EventHandler(button_Click);

                    this.Controls.Add(m_Board[i, j]);
                }
            }

            return m_Board[i_BoardSize - 1, i_BoardSize - 1];
        }

        private int initLabelsLocation(int i_Bottom, int i_Pad)
        {
            m_LablePlayer2 = new Label();
            m_LablePlayer2.Text = string.Format("{0} : 0", m_FormGameSettings.Player2Name);
            m_LablePlayer2.Top = i_Bottom + i_Pad;
            m_LablePlayer2.Left = ((i_Bottom + i_Pad) / 2);
            this.Controls.Add(m_LablePlayer2);

            m_LablePlayer1 = new Label();
            m_LablePlayer1.Text = string.Format("{0} : 0", m_FormGameSettings.Player1Name);
            m_LablePlayer1.Padding = new Padding(0, 0, 0, 0);
            m_LablePlayer1.Left = m_Board[0, 0].Left;       //start where the buttons start
            m_LablePlayer1.Top = m_LablePlayer2.Top;        //same as player 2
            this.Controls.Add(m_LablePlayer1);

            return (int)m_LablePlayer1.Bottom;
        }

        private void changeButtonStatus(int i_Row, int i_Col)
        {
            if (m_Game.PlayerOne.Turn == true)
            {
                m_Board[i_Row, i_Col].Text = m_Game.PlayerOne.PlayerSign.ToString();
            }
            else
            {
                m_Board[i_Row, i_Col].Text = m_Game.PlayerTwo.PlayerSign.ToString();
            }
            m_Board[i_Row, i_Col].Enabled = false;
        }

        private string winGame()
        {
            string playerWinName;
            if (m_Game.PlayerOne.Turn)
            {
                m_Game.PlayerOne.AddOnePoint();
                playerWinName = string.Format("The winner is {0}!{1}", m_Game.PlayerOne.Name, Environment.NewLine);
                m_LablePlayer1.Text = string.Format("{0} : {1}", m_Game.PlayerOne.Name, m_Game.PlayerOne.Score);
            }
            else
            {
                m_Game.PlayerTwo.AddOnePoint();
                playerWinName = string.Format("The winner is {0}!{1}", m_Game.PlayerTwo.Name, Environment.NewLine);
                m_LablePlayer2.Text = string.Format("{0} : {1}", m_Game.PlayerTwo.Name, m_Game.PlayerTwo.Score);
            }
            return playerWinName;
        }

        public void EndOfGame(string i_Msg, bool i_Winner)
        {
            string caption;
            if (i_Winner)
            {
                caption = "A Win!";
            }
            else
            {
                caption = "A Tie!";
            }
            if (MessageBox.Show(string.Format("{0}Would you like to play another game?", i_Msg), caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                CleanBoard();
            }
            else
            {
                this.Close();
                Application.Exit();
            }
        }

        public void CleanBoard()
        {
            m_Game.InitGame();
            for (int i = 0; i < m_Game.GameBoard.Size; i++)
            {
                for (int j = 0; j < m_Game.GameBoard.Size; j++)
                {
                    m_Board[i, j].Text = "";
                    m_Board[i, j].Enabled = true;
                }
            }
            if (m_Game.GameBoard.Size % 2 == 1 && m_Game.IsSinglePlayerMode == TicTacToeEnums.eGameMode.SinglePlayer)
            {
                FillMiddleButton();
            }
        }

        public void FillMiddleButton()
        {
            int middle = m_Game.GameBoard.Size / 2;
            m_Game.MakeAMove(ref middle, ref middle);
            changeButtonStatus(middle, middle);
        }
    }
}