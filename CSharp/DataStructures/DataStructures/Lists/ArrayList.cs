using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Lists
{
    /// <summary>
    /// Implements the IList interface using an array whose size is dynamically increased as required.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the LinkedList.</typeparam>
    public class ArrayList<T> : IList<T>, ICloneable
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
                    throw new IndexOutOfRangeException($"The index, {index}, is out of the bounds of the ArrayList.");
                }
                return backingArray[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException($"The index, {index}, is out of the bounds of the ArrayList.");
                }
                backingArray[index] = value;
            }
        }

        /// <summary>
        /// Gets the number of nodes actually contained in the LinkedList.
        /// </summary>
        public int Count => arrayTail + 1;

        /// <summary>
        /// Gets a value indicating whether the LinkedList is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// The initial capacity of the ArrayList if none is given in the constructor of the class.
        /// </summary>
        public const int InitCapacity = 4;

        private int arrayTail = -1;
        private T[] backingArray;

        /// <summary>
        /// Initializes a new instance of a ArrayList that contains elements copied from the specified collection.
        /// </summary>
        public ArrayList()
        {
            backingArray = new T[InitCapacity];
        }

        /// <summary>
        /// Initializes a new instance of the ArrayList class that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The specific capacity to initialize the ArrayList with.</param>
        public ArrayList(int capacity)
        {
            if(capacity < 0)
            {
                throw new ArgumentNullException("The capacity of the ArrayList must be initialized as a positive non-zero number.");
            }

            backingArray = new T[capacity];
        }

        /// <summary>
        /// Initializes a new instance of a ArrayList that contains elements copied from the specified collection.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new ArrayList.</param>
        public ArrayList(ICollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("The ICollection object was null. If you wish to instantiate an ArrayList object without any initial values, use the constructor overload 'new ArrayList()'.");
            }

            backingArray = new T[collection.Count + InitCapacity];
            collection.CopyTo(backingArray, 0);
        }

        private ArrayList(T[] backing, int tail)
        {
            backingArray = backing;
            arrayTail = tail;
        }

        /// <summary>
        /// Adds an object to the end of the ArrayList.
        /// </summary>
        /// <param name="item">The T, item, to be added to the end of the ArrayList. The value can be null.</param>
        public void Add(T item)
        {
            Resize();
            backingArray[arrayTail] = item;
        }

        /// <summary>
        /// Removes all elements from the ArrayList.
        /// </summary>
        public void Clear()
        {
            arrayTail = -1;
        }

        /// <summary>
        /// Creates a shallow copy of the ArrayList.
        /// </summary>
        /// <returns>A shallow copy of the ArrayList.</returns>
        public object Clone()
        {
            return new ArrayList<T>((T[])backingArray.Clone(), arrayTail);
        }

        /// <summary>
        /// Determines whether an element is in the ArrayList.
        /// </summary>
        /// <param name="item">The T, item, to locate in the ArrayList. The value can be null.</param>
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
        /// Copies the entire ArrayList to a compatible one-dimensional Array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ArrayList. The Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("The array to copy to cannot be null.");
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentOutOfRangeException($"The array of length, {array.Length}, does not have enough space to copy the contents of the LinkedList starting at index {arrayIndex}.");
            }

            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new IndexOutOfRangeException($"The start index, {arrayIndex} is an invalid starting point for the given array.");
            }

            backingArray[0..Count].CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Searches for the specified T and returns the zero-based index of the first occurrence within the entire ArrayList.
        /// </summary>
        /// <param name="item">The T, item, to locate in the ArrayList. The value can be null.</param>
        /// <param name="startIndex">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
        /// <returns>The zero-based index of the first occurrence of value within the entire ArrayList, if found; Otherwise, -1.</returns>
        public int IndexOf(T item, int startIndex)
        {
            for (int i = startIndex; i < Count; i++)
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
        /// Searches for the specified T and returns the zero-based index of the first occurrence within the entire ArrayList.
        /// </summary>
        /// <param name="item">The T, item, to locate in the ArrayList. The value can be null.</param>
        /// <returns>The zero-based index of the first occurrence of value within the entire ArrayList, if found; Otherwise, -1.</returns>
        public int IndexOf(T item)
        {
            return IndexOf(item, 0);
        }

        /// <summary>
        /// Inserts an element into the ArrayList at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which value should be inserted.</param>
        /// <param name="item">The T, item, to insert. The value can be null.</param>
        public void Insert(int index, T item)
        {
            if (index < 0 || index > Count)
            {
                throw new IndexOutOfRangeException($"The index, {index}, is out of the bounds of the ArrayList.");
            }

            Resize();
            backingArray[index..arrayTail].CopyTo(backingArray, index + 1);
            backingArray[index] = item;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the ArrayList.
        /// </summary>
        /// <param name="item">The T, item, to remove from the ArrayList. The value can be null.</param>
        /// <returns>A boolean, true if the item was successfully removed; otherwise, false.</returns>
        public bool Remove(T item)
        {
            int indexToRemove = IndexOf(item);
            if (indexToRemove == -1) return false;

            RemoveAt(indexToRemove);
            return true;
        }

        /// <summary>
        /// Removes the element at the specified index of the ArrayList.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        public void RemoveAt(int index)
        {
            backingArray[(index + 1)..Count].CopyTo(backingArray.AsSpan(index..Count));
            arrayTail--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            T[] validData = backingArray[0..Count];
            return ((IEnumerable<T>)validData).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
