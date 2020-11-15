using System;

namespace DataStructuresTester.Lists
{
    public class DisposableLinkedList<T> : IDisposable
    {
        public DataStructures.Lists.LinkedList<T> TestList { get; private set;  }

        public DisposableLinkedList()
        {
            TestList = new DataStructures.Lists.LinkedList<T>();
        }

        public void Dispose()
        {
            TestList = new DataStructures.Lists.LinkedList<T>();
        }
    }
}