using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Lists
{
    public class LinkedList<T> : ICollection<T>
    {
        public T this[int index]
        {
            get => GetNodeAt(index).NodeData;
            set => GetNodeAt(index).NodeData = value;
        }
        public bool IsReadOnly => false;
        public bool IsEmpty => Count == 0;
        public int Count { get; private set; }

        public object? SyncRoot => null;
        public bool IsSynchronized => false;

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
        private LinkedListNode<T>? tail;

        public LinkedList()
        {
            Count = 0;
            head = null;
            tail = null;
        }

        public LinkedList(IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                Add(item);
            }
        }

        public void Add(T item)
        {
            AddLast(item);
        }

        public void AddFirst(T item)
        {
            if (item == null)
            {
                throw new NoNullAllowedException("Null values are not allowed in the Linked-List");
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

        public void AddLast(T item)
        {
            if (item == null)
            {
                throw new NoNullAllowedException("Null values are not allowed in the Linked-List");
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

        public void Clear()
        {
            Count = 0;
            head = null;
            tail = null;
        }

        public bool Contains(T item)
        {
            return GetNode(item) != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentOutOfRangeException($"The array of length, {array.Length}, does not have enough space to copy the contents of the Linked-List starting at index {arrayIndex}");
            }

            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new IndexOutOfRangeException($"The start index, {arrayIndex} is an invalid starting point for the given array");
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

        public int IndexOf(T item)
        {
            if (item == null) return -1;

            int nodeIndex = 0;
            LinkedListNode<T>? cursor = head;
            while (cursor != null)
            {
                if (cursor.NodeData!.Equals(item)) return nodeIndex;

                cursor = cursor.NextNode;
                nodeIndex++;
            }
            return -1;
        }

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
            if(oldNode.PreviousNode != null)
            {
                oldNode.PreviousNode.NextNode = newNode;
            }

            oldNode.PreviousNode = newNode;
            newNode.NextNode = oldNode;
            Count++;
        }

        public bool Remove(T item)
        {
            if (item == null) return false;

            LinkedListNode<T>? nodeToRemove = GetNode(item);
            if (nodeToRemove == null) return false;

            RemoveNode(nodeToRemove);
            return true;
        }

        public bool RemoveFirst()
        {
            if (head == null) return false;

            RemoveNode(head);
            return true;
        }

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
                throw new IndexOutOfRangeException($"The index, {index}, is out of the bounds of the Linked-List");
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