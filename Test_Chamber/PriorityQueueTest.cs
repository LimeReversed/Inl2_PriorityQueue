using Inl2_PriorityQueue;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test_Chamber
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Pop_ListHasStrings_ReturnsStringsInTheRightOrder()
        {

            PriorityQueue<string> pqList = CreateListOfAThousandStrings();
            string previous = "0";

            // When poping a QueueList the count will change, so only get Count() once. 
            for (int i = 0, count = pqList.Count(); i < count; i++)
            {
                string current = pqList.Pop();

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

            PriorityQueue<string> list = new PriorityQueue<string>();

            list.Add("Test");
            list.Pop();

            Assert.Throws<InvalidOperationException>(() => list.Pop());
        }

        [Test]
        public void Pop_AllElementsAreTheSame_Works()
        {
            PriorityQueue<string> list = new PriorityQueue<string>();
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
        public void Pop_ListAlreadySorted_Works()
        {
            PriorityQueue<string> list = new PriorityQueue<string>();
            list.Add("A");
            list.Add("B");
            list.Add("C");
            list.Add("D");
            list.Add("E");
            list.Add("F");
            list.Add("G");

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
            PriorityQueue<string> priorityQueueList = new PriorityQueue<string>();
            Assert.Throws<InvalidOperationException>(() => priorityQueueList.Pop());
        }

        [Test]
        public void Pop_NotRemovingAll_CountIsCorrect()
        {
            PriorityQueue<string> PQList = CreateListOfAThousandStrings();

            for (int i = 0; i < 500; i++)
            {
                PQList.Pop();
            }

            Assert.AreEqual(500, PQList.Count());
        }

        [Test]
        public void Peek_ListIsEmpty_ThrowsException()
        {
            PriorityQueue<string> priorityQueueList = new PriorityQueue<string>();
            Assert.Throws<InvalidOperationException>(() => priorityQueueList.Peek());
        }

        [Test]
        public void Count_ListIsEmpty()
        {
            PriorityQueue<string> list = new PriorityQueue<string>();
            Assert.AreEqual(0, list.Count());
        }

        [Test]
        public void Count_ListHasAThousandElements_CountIsCorrect()
        {
            PriorityQueue<string> list = CreateListOfAThousandStrings();
            Assert.AreEqual(1000, list.Count());
        }

        /*
         * I debated whether the below test is fair or not. The regular list is faster if 
         * you just add all the elements and then sort only once. So why not just do that?
         * I concluded that you are supposed to be able to get an element at any time. 
         * Not just after you've added them all, meaning that it has to be sorted with 
         * every Add() and in that context the PriorityQueue is much faster. 
         */
        [Test]
        public void Add_TakesLessTimeThanRegular()
        {
            PriorityQueue<int> list = new PriorityQueue<int>();
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
                list.Add(i);
            }
            TimeSpan listTime = DateTime.Now - start;

            Assert.IsTrue(listTime.Milliseconds < regularIntListTime.Milliseconds);
        }

        public PriorityQueue<string> CreateListOfAThousandStrings()
        {
            PriorityQueue<string> list = new PriorityQueue<string>();
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