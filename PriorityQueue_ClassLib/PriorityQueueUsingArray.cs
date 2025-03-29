using PriorityQueue;
using System;

namespace Inl2_PriorityQueue
{
    public class PriorityQueueUsingArray<T> : IPriorityQueue<T> where T : IComparable, IComparable<T>
    {
        private int count = 0;
        private T[] array = new T[16];

        public void Add(T value)
        {
            if (count == array.Length) ResizeArray(array.Length * 2);

            count++;
            array[count - 1] = value;
            HeapifyUp(count - 1);
        }

        public int Count()
        {
            return count;
        }

        public T Peek()
        {
            if (count < 1) throw new InvalidOperationException("Cannot peek an empty list");

            return array[0];
        }

        public T Pop()
        {
            if (count < 1) throw new InvalidOperationException("Cannot pop an empty list");

            T poppedElement = array[0];
            array[0] = default;

            /* Swap first element with last element, subtract count, then heapify the new first element down to it's new position. 
             * Doing it this way keeps the tree balanced, since the nodes will be locked off in the correct order.
             * I tried to let the values "bubble up" based on which was smaller, but that created an unbalanced tree where
             * I couldn't tell which value was an entered one, or a default. 
             */
            (array[0], array[count - 1]) = (array[count - 1], array[0]); // Swap
            count--;
            HeapifyDown(0);

            return poppedElement;
        }

        private void HeapifyUp(int index) 
        {
            /*
             * i >> 1 calculates the index of the parent. The beauty of binary code is that 1100 = 12, 110 = 6, 11 = 3 and 1 = 1.
             * So I can just let the loop itself jump through the array as if it was a binary tree. 
             * index + 1 because it only works if the first index is 1. 
             * i > 1 because we are comparing parent and child, so we don't want to go to the last parent. 
             */
            for (int i = index + 1; i > 1; i = i >> 1)
            {
                int currentIndex = i - 1;
                int parentIndex = (i >> 1) - 1;
                bool parentIsSmallerOrEqual = array[parentIndex].CompareTo(array[currentIndex]) != 1;

                if (parentIsSmallerOrEqual) return;

                (array[currentIndex], array[parentIndex]) = (array[parentIndex], array[currentIndex]); // Swap
            }
        }

        private void HeapifyDown(int index)
        {
            while (true)
            {
                // nr << 1 is the same as nr * 2. I just wanted to use bitwise operators for this exercise. 
                int leftChildIndex = (index << 1) + 1;
                int rightChildIndex = (index << 1) + 2;

                // This will keep track of which of parent, leftChild and rightChild is the smallest. 
                int indexOfSmallest = index;

                // First we compare leftChild with parent
                if (leftChildIndex < count && array[leftChildIndex].CompareTo(array[indexOfSmallest]) == -1)
                {
                    indexOfSmallest = leftChildIndex;
                }

                // Then we compare rightChild with either parent or leftChild depending on previous if-statement.  
                if (rightChildIndex < count && array[rightChildIndex].CompareTo(array[indexOfSmallest]) == -1)
                {
                    indexOfSmallest = rightChildIndex;
                }

                // No child is smaller than parent so we are done. 
                if (indexOfSmallest == index) break;

                (array[index], array[indexOfSmallest]) = (array[indexOfSmallest], array[index]); // Swap
                index = indexOfSmallest;
            }
        }

        private void ResizeArray(int newLength) {
            T[] newArray = new T[newLength];

            for (int i = 0; i < array.Length; i++) 
            {
                newArray[i] = array[i];
            }

            array = newArray;
        }
    }
}
