using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Lists
{
    /// <summary>
    /// Represents a Doubly-LinkedList.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the DataStructures.Lists.LinkedList.</typeparam>
    public class LinkedList<T> : IList<T>
    {
        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        public T this[int index]
        {
            get => GetNodeAt(index).NodeData;
            set => GetNodeAt(index).NodeData = value;
        }

        /// <summary>
        /// Gets the number of nodes actually contained in the DataStructures.Lists.LinkedList.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the DataStructures.Lists.LinkedList is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Gets the value of the first node within the DataStructures.Lists.LinkedList.
        /// </summary>
        public T? First
        {
            get
            {
                if (head != null)
                {
                    return head.NodeData;
                }
                return default;
            }
        }
        private LinkedListNode<T>? head;

        /// <summary>
        /// Gets the value of the last node within the DataStructures.Lists.LinkedList.
        /// </summary>
        public T? Last
        {
            get
            {
                if (tail != null)
                {
                    return tail.NodeData;
                }
                return default;
            }
        }
        private LinkedListNode<T>? tail;

        /// <summary>
        /// Initializes a new instance of a DataStructures.Lists.LinkedList that contains elements copied from the specified collection.
        /// </summary>
        public LinkedList()
        {
            Count = 0;
            head = null;
            tail = null;
        }

        /// <summary>
        /// Initializes a new instance of a DataStructures.Lists.LinkedList that contains elements copied from the specified collection.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new DataStructures.Lists.LinkedList.</param>
        public LinkedList(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("The IEnumerable object was null. If you wish to instantiate an DataStructures.Lists.LinkedList object without any initial values, use the constructor overload 'new LinkedList()'.");
            }

            Count = 0;
            head = null;
            tail = null;
            foreach (T item in collection)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Adds the specified new node at the end of the DataStructures.Lists.LinkedList.
        /// </summary>
        /// <param name="item">The new value to add at the end of the DataStructures.Lists.LinkedList. The value can be null.</param>
        public void Add(T item)
        {
            AddLast(item);
        }

        /// <summary>
        /// Adds the specified new node at the end of the DataStructures.Lists.LinkedList.
        /// </summary>
        /// <param name="item">The new value to add at the end of the DataStructures.Lists.LinkedList. The value can be null.</param>
        public void AddFirst(T item)
        {
            Count++;
            LinkedListNode<T> newNode = new LinkedListNode<T>(item, null);
            if (head != null)
            {
                newNode.NextNode = head;
                head.PreviousNode = newNode;
            }
            head = newNode;
        }

        /// <summary>
        /// Adds the specified new node at the front of the DataStructures.Lists.LinkedList.
        /// </summary>
        /// <param name="item">The new value to add at the front of the DataStructures.Lists.LinkedList. The value can be null.</param>
        public void AddLast(T item)
        {
            if (head == null)
            {
                AddFirst(item);
                tail = head;
                return;
            }

            LinkedListNode<T> newNode = new LinkedListNode<T>(item, tail);
            if (tail != null)
            {
                tail.NextNode = newNode;
            }
            tail = newNode;
            Count++;
        }

        /// <summary>
        /// Removes all nodes from the DataStructures.Lists.LinkedList.
        /// </summary>
        public void Clear()
        {
            Count = 0;
            head = null;
            tail = null;
        }

        /// <summary>
        /// Determines whether a value is in the DataStructures.Lists.LinkedList.
        /// </summary>
        /// <param name="item">The value to locate in the DataStructures.Lists.LinkedList. The value can be null for reference types.</param>
        /// <returns>True if item is found in the LinkedList; Otherwise, false.</returns>
        public bool Contains(T item)
        {
            return GetNode(item) != null;
        }

        /// <summary>
        /// Copies the entire DataStructures.Lists.LinkedList to a compatible one-dimensional System.Array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional System.Array that is the destination of the elements copied from DataStructures.Lists.LinkedList. The System.Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in the array at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("The array to copy to cannot be null.");
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentOutOfRangeException($"The array of length, {array.Length}, does not have enough space to copy the contents of the DataStructures.Lists.LinkedList starting at index {arrayIndex}.");
            }

            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new IndexOutOfRangeException($"The start index, {arrayIndex} is an invalid starting point for the given array.");
            }

            LinkedListNode<T>? cursor = head;
            for (int i = arrayIndex; i < array.Length; i++)
            {
                if (cursor == null)
                {
                    return;
                }
                array[i] = cursor!.NodeData;
                cursor = cursor.NextNode;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the DataStructures.LinkedList starting with the head.
        /// </summary>
        /// <returns>An Enumerator for the DataStructures.LinkedList.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            LinkedListNode<T>? current = head;
            while (current != null)
            {
                yield return current.NodeData;
                current = current.NextNode;
            }
        }

        /// <summary>
        /// Searches for the specified T and returns the zero-based index of the first occurrence within the entire DataStructures.Lists.LinkedList.
        /// </summary>
        /// <param name="item">The T, item, to locate in the DataStructures.Lists.LinkedList. The value can be null.</param>
        /// <returns>The zero-based index of the first occurrence of value within the entire LinkedList, if found; Otherwise, -1.</returns>
        public int IndexOf(T item)
        {
            LinkedListNode<T>? cursor = head;
            for (int index = 0; cursor != null; index++)
            {
                if (cursor.NodeData!.Equals(item))
                {
                    return index;
                }

                cursor = cursor.NextNode;
            }
            return -1;
        }

        /// <summary>
        /// Inserts an element into the DataStructures.Lists.LinkedList at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        public void Insert(int index, T item)
        {
            if (index == 0)
            {
                AddFirst(item);
                return;
            }
            else if (index == Count)
            {
                AddLast(item);
                return;
            }

            LinkedListNode<T> oldNode = GetNodeAt(index);
            LinkedListNode<T> newNode = new LinkedListNode<T>(item, oldNode.PreviousNode);
            if (oldNode.PreviousNode != null)
            {
                oldNode.PreviousNode.NextNode = newNode;
            }

            oldNode.PreviousNode = newNode;
            newNode.NextNode = oldNode;
            Count++;
        }

        /// <summary>
        /// Removes the first occurrence of the specified value from the DataStructures.Lists.LinkedList.
        /// </summary>
        /// <param name="item">The value to remove from the DataStructures.Lists.LinkedList.</param>
        /// <returns>True if the element containing value is successfully removed; otherwise, false.</returns>
        public bool Remove(T item)
        {
            if (item == null) return false;

            LinkedListNode<T>? nodeToRemove = GetNode(item);
            if (nodeToRemove == null) return false;

            RemoveNode(nodeToRemove);
            return true;
        }

        /// <summary>
        /// Removes the value at the specified index within the DataStructures.Lists.LinkedList.
        /// </summary>
        /// <param name="index">The index of the element to remove from the DataStructures.Lists.LinkedList.</param>
        public void RemoveAt(int index)
        {
            LinkedListNode<T>? nodeToRemove = GetNodeAt(index);
            if (nodeToRemove == null)
            {
                throw new IndexOutOfRangeException($"The index, {index}, is not within the bounds of the DataStructures.Lists.LinkedList.");
            }
            RemoveNode(nodeToRemove);
        }

        /// <summary>
        /// Removes the node at the front of the DataStructures.Lists.LinkedList.
        /// </summary>
        /// <returns>True if the element containing value is successfully removed; otherwise, false.</returns>
        public bool RemoveFirst()
        {
            if (head == null) return false;

            RemoveNode(head);
            return true;
        }

        /// <summary>
        /// Removes the node at the end of the DataStructures.Lists.LinkedList.
        /// </summary>
        /// <returns>True if the element containing value is successfully removed; otherwise, false.</returns>
        public bool RemoveLast()
        {
            if (tail == null) return false;

            RemoveNode(tail);
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region Helper Methods
        private void RemoveNode(LinkedListNode<T> node)
        {
            Count--;
            if (node.PreviousNode == null)
            {
                head = node.NextNode;
                if (head != null)
                {
                    head.PreviousNode = null;
                }
                return;
            }

            if (node.NextNode == null)
            {
                tail = node.PreviousNode;
                tail.NextNode = null;
                return;
            }

            node.NextNode.PreviousNode = node.PreviousNode;
            node.PreviousNode.NextNode = node.NextNode;
        }

        private LinkedListNode<T> GetNodeAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException($"The index, {index}, is out of the bounds of the DataStructures.Lists.LinkedList.");
            }

            LinkedListNode<T>? cursor = head;
            for (int i = 0; i < index; i++)
            {
                cursor = cursor!.NextNode;
            }
            return cursor!;
        }

        private LinkedListNode<T>? GetNode(T item)
        {
            LinkedListNode<T>? cursor = head;
            while (cursor != null)
            {
                if (cursor.NodeData!.Equals(item))
                {
                    return cursor;
                }
                cursor = cursor.NextNode;
            }
            return null;
        }
        #endregion Helper Methods
    }
}