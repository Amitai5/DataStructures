using DataStructures;
using System;

namespace DataStructuresTester.Fixtures
{
    public class QueueFixture<T> : IDisposable
    {
        public Queue<T> TestQueue { get; private set; }

        public QueueFixture()
        {
            TestQueue = new Queue<T>();
        }

        public void Dispose()
        {
            TestQueue = new Queue<T>();
        }
    }
}
