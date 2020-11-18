using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Lists
{
    /// <summary>
    /// Represents a Doubly-Linked-List.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the Linked-List.</typeparam>
    public class LinkedList<T> : ICollection<T>
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
        /// Gets a value indicating whether the Linked-List is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Gets a value indicating whether the Linked-List is empty.
        /// </summary>
        public bool IsEmpty => Count == 0;

        /// <summary>
        /// Gets the number of nodes actually contained in the Linked-List.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the value of the last node within the Linked-List.
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
        /// Gets the value of the first node within the Linked-List.
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
        private readonly bool containsReferenceTypes;

        /// <summary>
        /// Initializes a new instance of a LinkedList that contains elements copied from the specified collection.
        /// </summary>
        public LinkedList()
        {
            Count = 0;
            head = null;
            tail = null;
            containsReferenceTypes = !typeof(T).IsValueType;
        }

        /// <summary>
        /// Initializes a new instance of a LinkedList that contains elements copied from the specified collection.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new Linked-List.</param>
        public LinkedList(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("The IEnumerable object was null. If you wish to instantiate an Linked-List object without any initial values, use the constructor overload 'new LinkedList()'.");
            }

            foreach (T item in collection)
            {
                Add(item);
            }
            containsReferenceTypes = !typeof(T).IsValueType;
        }

        /// <summary>
        /// Adds the specified new node at the end of the Linked-List.
        /// </summary>
        /// <param name="item">The new value to add at the end of the Linked-List.</param>
        public void Add(T item)
        {
            AddLast(item);
        }

        /// <summary>
        /// Adds the specified new node at the end of the Linked-List.
        /// </summary>
        /// <param name="item">The new value to add at the end of the Linked-List.</param>
        public void AddFirst(T item)
        {
            if (item == null && !containsReferenceTypes)
            {
                throw new NoNullAllowedException("Null value-types are not allowed in the Linked-List.");
            }

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
        /// Adds the specified new node at the front of the Linked-List.
        /// </summary>
        /// <param name="item">The new value to add at the front of the Linked-List.</param>
        public void AddLast(T item)
        {
            if (item == null && !containsReferenceTypes)
            {
                throw new NoNullAllowedException("Null value-types are not allowed in the Linked-List.");
            }

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
        /// Removes all nodes from the Linked-List.
        /// </summary>
        public void Clear()
        {
            Count = 0;
            head = null;
            tail = null;
        }

        /// <summary>
        /// Determines whether a value is in the Linked-List.
        /// </summary>
        /// <param name="item">The value to locate in the Linked-List. The value can be null for reference types.</param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return GetNode(item) != null;
        }

        /// <summary>
        /// Copies the entire Linked-List to a compatible one-dimensional array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional array that is the destination of the elements copied from Linked-List. The array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if(array == null)
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

            LinkedListNode<T>? cursor = head;
            for (int i = arrayIndex; i < array.Length; i++)
            {
                if(cursor == null)
                {
                    return;
                }
                array[i] = cursor!.NodeData;
                cursor = cursor.NextNode;
            }
        }

        /// <summary>
        /// Inserts an element into the Linked-List at the specified index.
        /// </summary>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        public void Insert(T item, int index)
        {
            if (item == null && !containsReferenceTypes)
            {
                throw new NoNullAllowedException("Null value-types are not allowed in the Linked-List.");
            }

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
            if(oldNode.PreviousNode != null)
            {
                oldNode.PreviousNode.NextNode = newNode;
            }

            oldNode.PreviousNode = newNode;
            newNode.NextNode = oldNode;
            Count++;
        }

        /// <summary>
        /// Removes the first occurrence of the specified value from the Linked-List.
        /// </summary>
        /// <param name="item">The value to remove from the Linked-List.</param>
        /// <returns>true if the element containing value is successfully removed; otherwise, false.</returns>
        public bool Remove(T item)
        {
            if (item == null) return false;

            LinkedListNode<T>? nodeToRemove = GetNode(item);
            if (nodeToRemove == null) return false;

            RemoveNode(nodeToRemove);
            return true;
        }

        /// <summary>
        /// Removes the node at the front of the Linked-List.
        /// </summary>
        /// <returns>true if the element containing value is successfully removed; otherwise, false.</returns>
        public bool RemoveFirst()
        {
            if (head == null) return false;

            RemoveNode(head);
            return true;
        }

        /// <summary>
        /// Removes the node at the end of the Linked-List.
        /// </summary>
        /// <returns>true if the element containing value is successfully removed; otherwise, false.</returns>
        public bool RemoveLast()
        {
            if (tail == null) return false;

            RemoveNode(tail);
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new LinkedListEnumerator<T>(head);
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
                if (tail != null)
                {
                    tail.NextNode = null;
                }
                return;
            }

            node.NextNode.PreviousNode = node.PreviousNode;
            node.PreviousNode.NextNode = node.NextNode;
        }

        private LinkedListNode<T> GetNodeAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException($"The index, {index}, is out of the bounds of the Linked-List.");
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