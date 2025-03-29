using Inl2_PriorityQueue;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Test_Chamber
{
    public class TestPriorityQueueWithArray
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Pop_ListHasIntegers_ReturnsInTheRightOrder()
        {

            PriorityQueueUsingArray<int> pq = new PriorityQueueUsingArray<int>();
            pq.Add(3);
            pq.Add(10);
            pq.Add(7);
            pq.Add(4);
            pq.Add(1);
            pq.Add(5);
            pq.Add(9);
            pq.Add(6);
            pq.Add(2);
            pq.Add(8);

            int previous = 0;

            // When poping a QueueList the count will change, so only get Count() once. 
            for (int i = 0, count = pq.Count(); i < count; i++)
            {
                int current = pq.Pop();
                Debug.WriteLine(current);

                // Previous should be smaller than or equal to current. If it's not then fail. 
                if (previous > current)
                {
                    Assert.Fail();
                }

                previous = current;
            }
            Assert.Pass();
        }

        [Test]
        public void Pop_ListHasStrings_ReturnsStringsInTheRightOrder()
        {

            PriorityQueueUsingArray<string> pq = CreatePqOfAThousandStrings();
            string previous = "0";

            // When poping a QueueList the count will change, so only get Count() once. 
            for (int i = 0, count = pq.Count(); i < count; i++)
            {
                string current = pq.Pop();

                // Previous should be smaller than or equal to current. If it's not then fail. 
                if (previous.CompareTo(current) == 1)
                {
                    Assert.Fail();
                }

                previous = current;
            }
            Assert.Pass();
        }

        [Test]
        public void Pop_RemovingTooMany_ThrowsException()
        {

            PriorityQueueUsingArray<string> list = new PriorityQueueUsingArray<string>();

            list.Add("Test");
            list.Pop();

            Assert.Throws<InvalidOperationException>(() => list.Pop());
        }

        [Test]
        public void Pop_AllElementsAreTheSame_Works()
        {
            PriorityQueueUsingArray<string> list = new PriorityQueueUsingArray<string>();
            list.Add("Hej");
            list.Add("Hej");
            list.Add("Hej");
            list.Add("Hej");
            list.Add("Hej");
            list.Add("Hej");
            list.Add("Hej");

            list.Pop();
            list.Pop();
            list.Pop();
            list.Pop();
            list.Pop();
            list.Pop();
            list.Pop();

            Assert.Pass();

        }

        [Test]
        public void Pop_ListIsEmpty_ThrowsException()
        {
            PriorityQueueUsingArray<string> priorityQueueList = new PriorityQueueUsingArray<string>();
            Assert.Throws<InvalidOperationException>(() => priorityQueueList.Pop());
        }

        [Test]
        public void Pop_NotRemovingAll_CountIsCorrect()
        {
            PriorityQueueUsingArray<string> PQList = CreatePqOfAThousandStrings();

            for (int i = 0; i < 500; i++)
            {
                PQList.Pop();
            }

            Assert.AreEqual(500, PQList.Count());
        }

        [Test]
        public void Peek_ListIsEmpty_ThrowsException()
        {
            PriorityQueueUsingArray<string> priorityQueueList = new PriorityQueueUsingArray<string>();
            Assert.Throws<InvalidOperationException>(() => priorityQueueList.Peek());
        }

        [Test]
        public void Count_ListIsEmpty()
        {
            PriorityQueueUsingArray<string> list = new PriorityQueueUsingArray<string>();
            Assert.AreEqual(0, list.Count());
        }

        [Test]
        public void Count_ListHasAThousandElements_CountIsCorrect()
        {
            PriorityQueueUsingArray<string> list = CreatePqOfAThousandStrings();
            Assert.AreEqual(1000, list.Count());
        }

        //    /*
        //     * I debated whether the below test is fair or not. The regular list is faster if 
        //     * you just add all the elements and then sort only once. So why not just do that?
        //     * I concluded that you are supposed to be able to get an element at any time. 
        //     * Not just after you've added them all, meaning that it has to be sorted with 
        //     * every Add() and in that context the PriorityQueue is much faster. 
        //     */
        [Test]
        public void Add_TakesLessTimeThanRegular()
        {
            PriorityQueueUsingArray<int> pq = new PriorityQueueUsingArray<int>();
            List<int> regularIntList = new List<int>();

            DateTime start = DateTime.Now;
            for (int i = 0; i < 100000; i++)
            {
                regularIntList.Add(i);
                regularIntList.Sort();
            }

            TimeSpan regularIntListTime = DateTime.Now - start;

            start = DateTime.Now;
            for (int i = 0; i < 100000; i++)
            {
                pq.Add(i);
            }
            TimeSpan pqTime = DateTime.Now - start;

            Debug.WriteLine($"Regular list: {regularIntListTime.TotalSeconds} seconds");
            Debug.WriteLine($"PQ: {pqTime.TotalSeconds} seconds");
            Assert.IsTrue(pqTime.TotalSeconds < regularIntListTime.TotalSeconds);
        }

        [Test]
        public void Add_UsingArrayTakesLessTimeThanObjects()
        {
            PriorityQueueUsingArray<int> pqUsingArray = new PriorityQueueUsingArray<int>();
            PriorityQueue<int> pqUsingObjects = new PriorityQueue<int>();
            int amount = 10000000;

            DateTime start = DateTime.Now;
            for (int i = 0; i < amount; i++)
            {
                pqUsingObjects.Add(i);
            }

            TimeSpan objectPqTime = DateTime.Now - start;
            Debug.WriteLine($"PQ with objects: {objectPqTime.TotalSeconds} seconds");
            
            start = DateTime.Now;
            
            for (int i = 0; i < amount; i++)
            {
                pqUsingArray.Add(i);
            }
            
            TimeSpan arrayPqTime = DateTime.Now - start;


            Debug.WriteLine($"PQ with array: {arrayPqTime.TotalSeconds} seconds");
            Assert.IsTrue(arrayPqTime.TotalSeconds < objectPqTime.TotalSeconds);
        }

        public PriorityQueueUsingArray<string> CreatePqOfAThousandStrings()
        {
            PriorityQueueUsingArray<string> list = new PriorityQueueUsingArray<string>();
            Random r = new Random();
            for (int i = 0; i < 1000; i++)
            {
                StringBuilder builder = new StringBuilder();
                for (int j = 0; j < 5; j++)
                {
                    builder.Append((char)r.Next(97, 121));
                }
                list.Add(builder.ToString());
            }
            return list;
        }
    }
}