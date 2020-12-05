using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Collections;
using System;

namespace DataStructures
{
    /// <summary>
    /// Represents a first-in, last-out collection of T, objects.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the DataStructures.Stack.</typeparam>
    public class Stack<T> : ICollection, IReadOnlyCollection<T>
    {
        /// <summary>
        /// Gets a value indicating whether access to the DataStructures.Stack is synchronized (thread safe).
        /// True if access to the DataStructures.Stack is synchronized (thread safe); otherwise, false. The default is false.
        /// </summary>
        public bool IsSynchronized => false;

        /// <summary>
        /// Gets an object that can be used to synchronize access to the DataStructures.Stack.
        /// </summary>
        public object SyncRoot => throw new NotImplementedException();

        /// <summary>
        /// Gets the number of elements that the DataStructures.Stack can contain at the current size.
        /// </summary>
        public int Capacity => backingArray.Length;

        /// <summary>
        /// Gets the number of elements contained in the DataStructures.Stack.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the DataStructures.Stack has any elements within it.
        /// True if the DataStructures.Stack is empty; otherwise, false.
        /// </summary>
        public bool IsEmpty => Count == 0;

        /// <summary>
        /// The initial capacity of the DataStructures.Stack if none is given in the constructor of the class.
        /// </summary>
        public const int InitCapacity = 4;

        private int queueTail = -1;
        private int queueHead = 0;
        private T[] backingArray;

        /// <summary>
        /// Initializes a new instance of the DataStructures.Stack class that is empty and has the default initial capacity.
        /// </summary>
        public Stack()
        {
            backingArray = new T[InitCapacity];
            queueTail = -1;
        }

        /// <summary>
        /// Initializes a new instance of the DataStructures.Stack class that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the DataStructures.Stack can contain.</param>
        public Stack(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentNullException("The capacity of the DataStructures.Stack must be initialized as a positive non-zero number.");
            }
            backingArray = new T[capacity];
        }

        /// <summary>
        /// Removes all objects from the DataStructures.Stack.
        /// </summary>
        public void Clear()
        {
            queueTail = -1;
            queueHead = 0;
            Count = 0;
        }

        /// <summary>
        /// Determines whether an element is in the DataStructures.Stack.
        /// </summary>
        /// <param name="item">The object to locate in the DataStructures.Stack. The value can be null.</param>
        /// <returns>True if item is found in the Queue; otherwise, false.</returns>
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

        /// <summary>
        /// Copies the DataStructures.Stack elements to an existing one-dimensional System.Array, starting at the specified array index.
        /// </summary>
        /// <param name="array">The one-dimensional System.Array that is the destination of the elements copied from DataStructures.Stack. The System.Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(Array array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("The array to copy to cannot be null.");
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentOutOfRangeException($"The array of length, {array.Length}, does not have enough space to copy the contents of the DataStructures.Stack starting at index {queueTail}.");
            }

            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new IndexOutOfRangeException($"The start index, {queueTail} is an invalid starting point for the given array.");
            }

            backingArray[queueHead..Count].CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///  Removes and returns the object at the beginning of the DataStructures.Stack.
        /// </summary>
        /// <returns>The object that is removed from the beginning of the DataStructures.Stack.</returns>
        public T Dequeue()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The DataStructures.Stack is empty.");
            }

            Count--;
            queueHead++;

            int previous = queueHead - 1;
            return backingArray[previous % backingArray.Length];
        }

        /// <summary>
        /// Adds an object to the end of the DataStructures.Stack.
        /// </summary>
        /// <param name="item">The object to add to the DataStructures.Stack. The value can be null.</param>
        public void Enqueue(T item)
        {
            Count++;
            Resize();
            backingArray[queueTail % backingArray.Length] = item;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the DataStructures.Stack.
        /// </summary>
        /// <returns>An Enumerator for the DataStructures.Stack.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            T[] validData = getConsecutiveBackend();
            return ((IEnumerable<T>)validData).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns the object at the beginning of the DataStructures.Stack without removing it.
        /// </summary>
        /// <returns>The object at the beginning of the DataStructures.Stack.</returns>
        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The DataStructures.Stack is empty.");
            }
            return backingArray[queueHead % backingArray.Length];
        }

        /// <summary>
        /// Copies the DataStructures.Stack elements to a new array.
        /// </summary>
        /// <returns>A new array containing elements copied from the DataStructures.Stack.</returns>
        public T[] ToArray()
        {
            T[] returnArray = new T[Count];
            CopyTo(returnArray, 0);
            return returnArray;
        }

        /// <summary>
        /// Sets the capacity to the actual number of elements in the DataStructures.Stack.
        /// </summary>
        public void TrimExcess()
        {
            backingArray = ToArray();
            queueTail = Count - 1;
            queueHead = 0;
        }

        /// <summary>
        /// Removes the object at the beginning of the Queue, and copies it to the result parameter.
        /// </summary>
        /// <param name="result">The removed object.</param>
        /// <returns>True if the object is successfully removed; false if the DataStructures.Stack is empty.</returns>
        public bool TryDequeue([MaybeNullWhen(false)] out T? result)
        {
            result = default;
            if (IsEmpty) return false;

            result = Dequeue();
            return true;
        }

        /// <summary>
        /// Returns a value that indicates whether there is an object at the beginning of the Queue, and if one is present, 
        /// copies it to the result parameter. The object is not removed from the DataStructures.Stack.
        /// </summary>
        /// <param name="result">If present, the object at the beginning of the Queue; otherwise, the default value of T.</param>
        /// <returns>True if there is an object at the beginning of the Queue; false if the DataStructures.Stack is empty.</returns>
        public bool TryPeek([MaybeNullWhen(false)] out T? result)
        {
            result = default;
            if (IsEmpty) return false;

            result = Peek();
            return true;
        }

        #region Helper Methods
        private T[] getConsecutiveBackend()
        {
            if (Count == 0)
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
