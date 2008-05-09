using System;

namespace MemoryCards
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (MemoryCardGame game = new MemoryCardGame())
            {
                game.Run();
            }
        }
    }
}

