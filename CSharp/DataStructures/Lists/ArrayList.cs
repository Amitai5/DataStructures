using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Lists
{
    public class ArrayList<T> : IList<T>, ICloneable
    {
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException($"The index, {index}, is out of the bounds of the Array-List.");
                }
                return backingArray[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException($"The index, {index}, is out of the bounds of the Array-List.");
                }
                backingArray[index] = value;
            }
        }
        public int Count => arrayTail + 1;

        public bool IsReadOnly => false;
        public const int InitCapacity = 4;

        private int arrayTail = -1;
        private T[] backingArray;

        public ArrayList()
        {
            backingArray = new T[InitCapacity];
        }

        public ArrayList(int capacity)
        {
            if(capacity < 0)
            {
                throw new ArgumentNullException("The capacity of the ArrayList must be initialized as a positive non-zero number.");
            }

            backingArray = new T[capacity];
        }

        public ArrayList(ICollection c)
        {
            if (c == null)
            {
                throw new ArgumentNullException("The ICollection object was null. If you wish to instantiate an ArrayList object without any initial values, use the constructor overload 'new ArrayList()'.");
            }

            backingArray = new T[c.Count + InitCapacity];
            c.CopyTo(backingArray, 0);
        }

        private ArrayList(T[] backing, int tail)
        {
            backingArray = backing;
            arrayTail = tail;
        }

        public void Add(T item)
        {
            resize();
            backingArray[arrayTail] = item;
        }

        public void Clear()
        {
            arrayTail = -1;
        }

        public object Clone()
        {
            return new ArrayList<T>((T[])backingArray.Clone(), arrayTail);
        }

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

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("The array to copy to cannot be null.");
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentOutOfRangeException($"The array of length, {array.Length}, does not have enough space to copy the contents of the Linked-List starting at index {arrayIndex}.");
            }

            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new IndexOutOfRangeException($"The start index, {arrayIndex} is an invalid starting point for the given array.");
            }

            backingArray[0..Count].CopyTo(array, arrayIndex);
        }

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

        public void Insert(int index, T item)
        {
            if (index < 0 || index > Count)
            {
                throw new IndexOutOfRangeException($"The index, {index}, is out of the bounds of the ArrayList.");
            }

            resize();
            backingArray[index..arrayTail].CopyTo(backingArray, index + 1);
            backingArray[index] = item;
        }

        public bool Remove(T item)
        {
            int indexToRemove = IndexOf(item);
            if (indexToRemove == -1) return false;

            RemoveAt(indexToRemove);
            return true;
        }

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
        private void resize()
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
