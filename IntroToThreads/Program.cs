using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToThreads
{
    class Program
    {
        
        static void Main(string[] args)
        {
            double dartsOnBoard = 0;
            Console.Write("How many darts for each thread to throw: ");
            int numDarts = Convert.ToInt32(Console.ReadLine());
            Console.Write("How many threads to run: ");
            int numThreads = Convert.ToInt32(Console.ReadLine());

            List<FindPiThread> findPiThreads = new List<FindPiThread>();
            List<Thread> threads = new List<Thread>();

            for(int i = 0; i < numThreads; i++)
            {
                FindPiThread findPi = new FindPiThread(numDarts);
                findPiThreads.Add(findPi);
                Thread thread = new Thread(new ThreadStart(findPi.ThrowDarts));
                threads.Add(thread);
                thread.Start();
                Thread.Sleep(16);
            }

            for(int i = 0; i < numThreads; i++)
            {
                threads[i].Join();
            }

            for (int i = 0; i < numThreads; i++)
            {
                dartsOnBoard += findPiThreads[i].numOnBoard;
            }

            double pi = (4 * (dartsOnBoard / (numThreads * numDarts)));

            Console.WriteLine(pi);

            Console.ReadKey();

        }

        public class FindPiThread
        {
            int numDarts;
            public int numOnBoard { get; set; }
            Random rand;

            public FindPiThread(int Darts)
            {
                numOnBoard = 0;
                numDarts = Darts;
                rand = new Random();
            }

            public void ThrowDarts()
            {
                for(int i = 0; i < numDarts; i++)
                {
                    double x = rand.NextDouble();
                    double y = rand.NextDouble();

                    double XSquared = x * x;
                    double YSquared = y * y;

                    if(XSquared + YSquared <= 1)
                    {
                        numOnBoard += 1;
                    }
                }
            }
        }

        
    }
}
