using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Collections;
using System;

namespace DataStructures
{
    public class Queue<T> : ICollection, IReadOnlyCollection<T>
    {
        public bool IsSynchronized => false;
        public object SyncRoot => throw new NotImplementedException();

        public int Capacity => backingArray.Length;

        public int Count { get; private set; }

        public bool IsEmpty => Count == 0;

        public const int InitCapacity = 4;

        private int queueTail = -1;
        private int queueHead = 0;
        private T[] backingArray;

        public Queue()
        {
            backingArray = new T[InitCapacity];
            queueTail = -1;
        }

        public Queue(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentNullException("The capacity of the Queue must be initialized as a positive non-zero number.");
            }
            backingArray = new T[capacity];
        }

        public void Clear()
        {
            queueTail = -1;
            queueHead = 0;
            Count = 0;
        }

        public bool Contains(T item)
        {
            T[] consecBackend = getConsecutiveBackend();
            for (int i = 0; i < consecBackend.Length; i++)
            {
                if (consecBackend[i] == null && item == null)
                {
                    return true;
                }

                else if (consecBackend[i] != null && consecBackend[i]!.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(Array array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("The array to copy to cannot be null.");
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentOutOfRangeException($"The array of length, {array.Length}, does not have enough space to copy the contents of the Queue starting at index {queueTail}.");
            }

            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new IndexOutOfRangeException($"The start index, {queueTail} is an invalid starting point for the given array.");
            }

            backingArray[queueHead..Count].CopyTo(array, arrayIndex);
        }


        public T Dequeue()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The Queue is empty.");
            }

            Count--;
            queueHead++;

            int previous = queueHead - 1;
            return backingArray[previous % backingArray.Length];
        }

        public void Enqueue(T item)
        {
            Count++;
            Resize();
            backingArray[queueTail % backingArray.Length] = item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            T[] validData = getConsecutiveBackend();
            return ((IEnumerable<T>)validData).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The Queue is empty.");
            }
            return backingArray[queueHead % backingArray.Length];
        }

        public T[] ToArray()
        {
            T[] returnArray = new T[Count];
            CopyTo(returnArray, 0);
            return returnArray;
        }

        public void TrimExcess()
        {
            backingArray = ToArray();
            queueTail = Count - 1;
            queueHead = 0;
        }

        public bool TryDequeue([MaybeNullWhen(false)] out T? result)
        {
            result = default;
            if (Count == 0) return false;

            result = Dequeue();
            return true;
        }

        public bool TryPeek([MaybeNullWhen(false)] out T? result)
        {
            result = default;
            if (Count == 0) return false;

            result = Peek();
            return true;
        }

        #region Helper Methods
        private T[] getConsecutiveBackend()
        {
            if(Count == 0)
            {
                return Array.Empty<T>();
            }

            int adjustedTail = queueTail % backingArray.Length;
            if (adjustedTail >= queueHead)
            {
                return backingArray[queueHead..(adjustedTail + 1)];
            }

            Span<T> firstPart = backingArray.AsSpan(queueHead..^1);
            Span<T> secondPart = backingArray.AsSpan(queueTail, queueTail + Count - firstPart.Length);

            T[] consecArray = new T[Count];
            firstPart.CopyTo(consecArray.AsSpan());
            secondPart.CopyTo(consecArray.AsSpan(firstPart.Length));
            return consecArray;
        }

        private void Resize()
        {
            queueTail++;
            if (Count < backingArray.Length)
            {
                if (queueHead == backingArray.Length)
                {
                    queueHead = 0;
                }
                return;
            }


            T[] newBackingArray = new T[backingArray.Length * 2];
            backingArray.CopyTo(newBackingArray, 0);
            backingArray = newBackingArray;

            queueTail = Count - 1;
            queueHead = 0;
        }
        #endregion Helper Methods
    }
}
