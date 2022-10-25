using Roguelike.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Core
{
    public class InputHandler
    {
        GameManager _gm;
        public ConsoleKey LastPressed { get; private set; }

        public InputHandler(GameManager gm)
        {
            _gm = gm;
        }

        public void WaitForInput()
        {
            LastPressed = Console.ReadKey(true).Key;
            // move player if they're alive
            //pass input to player
            int dx = LastPressed switch
            {
                ConsoleKey.A => -1,
                ConsoleKey.D => 1,
                _ => 0
            };
            int dy = LastPressed switch
            {
                ConsoleKey.W => -1,
                ConsoleKey.S => 1,
                _ => 0
            };
            _gm.Player.Move(dx, dy, _gm.MapManager.Map);
        }
    }
}
