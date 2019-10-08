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
        public void Pop_ListHasStrings_ReturnsTheSameAsRegularList()
        {

            PriorityQueue<string> pqList = CreateListOfAThousandStrings();

            // The Count() changes with every Pop() so I can't compare Count() with i directly.
            for (int i = 0, count = pqList.Count(); i < count; i++)
            {
                if (pqList.Pop() != regularList[i])
                {
                    Assert.Fail();
                }
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
        public void Peek_ListIsEmpty_ThrowsException()
        {
            PriorityQueue<string> priorityQueueList = new PriorityQueue<string>();
            Assert.Throws<InvalidOperationException>(() => priorityQueueList.Peek());
        }

        [Test]
        public void Pop_ListIsEmpty_ThrowsException()
        {
            PriorityQueue<string> priorityQueueList = new PriorityQueue<string>();
            Assert.Throws<InvalidOperationException>(() => priorityQueueList.Pop());
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

        public PriorityQueue<string> CreateListOfAThousandStrings()
        {
            regularList.Clear();
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
                regularList.Add(builder.ToString());

            }

            regularList.Sort();
            return list;
        }

        List<string> regularList = new List<string>();
    }
}