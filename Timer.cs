using System;
using System.Diagnostics;

namespace NN
{
    internal class Timer
    {
        private readonly Stopwatch s = new Stopwatch();

        public void Start()
        {
            s.Restart();
            WhileRunning();
        }

        public void WhileRunning()
        {
            TimeSpan time = s.Elapsed;
            string elapsed = time.Hours.ToString() + "h " + time.Minutes.ToString() + "m " + time.Seconds.ToString() + "s";

            while (s.IsRunning)
            {
                TimeSpan timeNew = s.Elapsed;
                string elapsedNew = timeNew.Hours.ToString() + "h " + timeNew.Minutes.ToString() + "m " + timeNew.Seconds.ToString() + "s";

                //Sorgt für sekündliche Ausgabe
                if (elapsed != elapsedNew)
                {
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write(new string(' ', Console.BufferWidth));
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(elapsedNew);
                    elapsed = elapsedNew;
                }
            }
        }

        public void Stop()
        {
            s.Stop();
            Console.WriteLine("\nGelaufene Zeit: " + s.Elapsed.TotalSeconds + "s");
        }
    }
}
