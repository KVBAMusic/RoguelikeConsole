using System;
using System.Runtime.InteropServices;
using Roguelike.Core;

namespace Roguelike
{
    public class ConsoleAppInit
    {
        // part of the code was based on
        // https://gist.github.com/tomzorz/6142d69852f831fb5393654c90a1f22e

        private const int STD_OUTPUT_HANDLE = -11;
        private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();

        static void Main(string[] args)
        {
            var iStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
            if (!GetConsoleMode(iStdOut, out uint outConsoleMode))
            {
                Console.WriteLine("failed to get output console mode");
                Console.ReadKey();
                return;
            }

            outConsoleMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;
            if (!SetConsoleMode(iStdOut, outConsoleMode))
            {
                Console.WriteLine($"failed to set output console mode, error code: {GetLastError()}");
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.White;
            GameManager gameManager = new();

            Console.WriteLine(@"
Terminal Rougelike by KVBA
written for lulz
------------------------------
controls:
* wasd      move
* spacebar  attack
* i         open inventory
------------------------------");

            Console.WriteLine("press any key to start");
            Console.ReadKey();

            gameManager.Start();
        }
    }
}