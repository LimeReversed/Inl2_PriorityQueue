using System;
using System.Globalization;
using Inl2_PriorityQueue;
using IPriorityQueue;

namespace Console_PQ
{
    class Program
    {
        public static void Main(string[] args)
        {
            PriorityQueueTester.TestPriorityQueue(() => new PriorityQueueUsingArray<int>(), () => new PriorityQueueUsingArray<string>());
            Console.ReadKey();
        }
    }
}
