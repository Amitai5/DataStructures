﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Lists
{
    /// <summary>
    /// Implements the IList interface using an array whose size is dynamically increased as required.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the ArrayList.</typeparam>
    public class ArrayList<T> : IList<T>
    {
        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException($"The index, {index}, is out of the bounds of the DataStructures.Lists.ArrayList.");
                }
                return backingArray[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException($"The index, {index}, is out of the bounds of the DataStructures.Lists.ArrayList.");
                }
                backingArray[index] = value;
            }
        }

        /// <summary>
        /// Gets the number of nodes actually contained in the ArrayList.
        /// </summary>
        public int Count => arrayTail + 1;

        /// <summary>
        /// Gets the number of elements that the DataStructures.Lists.ArrayList can contain at the current size.
        /// </summary>
        public int Capacity => backingArray.Length;

        /// <summary>
        /// Gets a value indicating whether the ArrayList is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// The initial capacity of the DataStructures.Lists.ArrayList if none is given in the constructor of the class.
        /// </summary>
        public const int InitCapacity = 4;

        private int arrayTail = -1;
        private T[] backingArray;

        /// <summary>
        /// Initializes a new instance of a DataStructures.Lists.ArrayList that contains elements copied from the specified collection.
        /// </summary>
        public ArrayList()
        {
            backingArray = new T[InitCapacity];
            arrayTail = -1;
        }

        /// <summary>
        /// Initializes a new instance of the DataStructures.Lists.ArrayList class that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The specific capacity to initialize the DataStructures.Lists.ArrayList with.</param>
        public ArrayList(int capacity)
        {
            if(capacity < 0)
            {
                throw new ArgumentNullException("The capacity of the DataStructures.Lists.ArrayList must be initialized as a positive non-zero number.");
            }

            backingArray = new T[capacity];
            arrayTail = -1;
        }

        /// <summary>
        /// Initializes a new instance of a DataStructures.Lists.ArrayList that contains elements copied from the specified collection.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new DataStructures.Lists.ArrayList.</param>
        public ArrayList(ICollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("The ICollection object was null. If you wish to instantiate an DataStructures.Lists.ArrayList object without any initial values, use the constructor overload 'new ArrayList()'.");
            }

            backingArray = new T[collection.Count + InitCapacity];
            collection.CopyTo(backingArray, 0);
        }

        /// <summary>
        /// Adds an object to the end of the DataStructures.Lists.ArrayList.
        /// </summary>
        /// <param name="item">The T, item, to be added to the end of the DataStructures.Lists.ArrayList. The value can be null.</param>
        public void Add(T item)
        {
            Resize();
            backingArray[arrayTail] = item;
        }

        /// <summary>
        /// Removes all elements from the DataStructures.Lists.ArrayList.
        /// </summary>
        public void Clear()
        {
            arrayTail = -1;
        }

        /// <summary>
        /// Determines whether an element is in the DataStructures.Lists.ArrayList.
        /// </summary>
        /// <param name="item">The T, item, to locate in the DataStructures.Lists.ArrayList. The value can be null.</param>
        /// <returns>True if item is found in the ArrayList; Otherwise, false.</returns>
        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (backingArray[i] == null && item == null)
                {
                    return true;
                }

                if (backingArray[i] != null && backingArray[i]!.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Copies the entire DataStructures.Lists.ArrayList to a compatible one-dimensional System.Array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional System.Array that is the destination of the elements copied from DataStructures.Lists.ArrayList. The System.Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("The array to copy to cannot be null.");
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentOutOfRangeException($"The array of length, {array.Length}, does not have enough space to copy the contents of the ArrayList starting at index {arrayIndex}.");
            }

            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new IndexOutOfRangeException($"The start index, {arrayIndex} is an invalid starting point for the given array.");
            }

            backingArray[0..Count].CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator for the entire DataStructures.Lists.ArrayList.
        /// </summary>
        /// <returns>An IEnumerator for the entire DataStructures.Lists.ArrayList.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            T[] validData = backingArray[0..Count];
            return ((IEnumerable<T>)validData).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Searches for the specified T and returns the zero-based index of the first occurrence within the entire DataStructures.Lists.ArrayList.
        /// </summary>
        /// <param name="item">The T, item, to locate in the DataStructures.Lists.ArrayList. The value can be null.</param>
        /// <returns>The zero-based index of the first occurrence of value within the entire ArrayList, if found; Otherwise, -1.</returns>
        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (backingArray[i] == null && item == null)
                {
                    return i;
                }

                if (backingArray[i] != null && backingArray[i]!.Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Inserts an element into the DataStructures.Lists.ArrayList at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which value should be inserted.</param>
        /// <param name="item">The T, item, to insert. The value can be null.</param>
        public void Insert(int index, T item)
        {
            if (index < 0 || index > Count)
            {
                throw new IndexOutOfRangeException($"The index, {index}, is out of the bounds of the DataStructures.Lists.ArrayList.");
            }

            Resize();
            backingArray[index..arrayTail].CopyTo(backingArray, index + 1);
            backingArray[index] = item;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the DataStructures.Lists.ArrayList.
        /// </summary>
        /// <param name="item">The T, item, to remove from the DataStructures.Lists.ArrayList. The value can be null.</param>
        /// <returns>A boolean, true if the item was successfully removed; otherwise, false.</returns>
        public bool Remove(T item)
        {
            int indexToRemove = IndexOf(item);
            if (indexToRemove == -1) return false;

            RemoveAt(indexToRemove);
            return true;
        }

        /// <summary>
        /// Removes the element at the specified index of the DataStructures.Lists.ArrayList.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        public void RemoveAt(int index)
        {
            backingArray[(index + 1)..Count].CopyTo(backingArray.AsSpan(index..Count));
            arrayTail--;
        }

        /// <summary>
        /// Copies the elements of the DataStructures.Lists.ArrayList to a new T, item, array.
        /// </summary>
        /// <returns>A T, item, array containing copies of the elements of the DataStructures.Lists.ArrayList.</returns>
        public T[] ToArray()
        {
            T[] returnArray = new T[Count];
            CopyTo(returnArray, 0);
            return returnArray;
        }

        #region Helper Methods
        private void Resize()
        {
            arrayTail++;
            if (arrayTail < backingArray.Length) return;

            T[] newBackingArray = new T[backingArray.Length * 2];
            backingArray.CopyTo(newBackingArray, 0);
            backingArray = newBackingArray;
        }
        #endregion Helper Methods
    }
}
