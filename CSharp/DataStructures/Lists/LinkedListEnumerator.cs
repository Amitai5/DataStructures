using System;
using System.Collections.Generic;

namespace DataStructures.Lists
{
    internal class LinkedListEnumerator<T> : IEnumerator<T>
    {
        private LinkedListNode<T>? cursor;
        object System.Collections.IEnumerator.Current => Current!;
        public T Current => cursor!.NodeData;

        public LinkedListEnumerator(LinkedListNode<T>? head)
        {
            cursor = head;
        }

        public bool MoveNext()
        {
            if (cursor == null || cursor.NextNode == null)
            {
                return false;
            }
            cursor = cursor.NextNode;
            return true;
        }

        public void Dispose()
        {

        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
