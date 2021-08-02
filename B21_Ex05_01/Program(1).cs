using System;
using System.Windows.Forms;
using System.Drawing;
using System.Text;

// $G$ DSN-999 (-5) you should have separated the logic and the ui into different folders or projects


// $G$ SFN-008 (+10) Bonus: Events in the Logic layer are handled by the UI.

namespace B21_Ex05_01
{
    public class Program
    {
        public static void Main()
        {
            FormGameWindow tictactoeGame = new FormGameWindow();
            tictactoeGame.Run();
        }
    }
}
