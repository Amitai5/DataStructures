using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresTester.Disposables
{
    public class DisposableQueue<T> : IDisposable
    {
        public DataStructures.Queue<T> TestQueue { get; private set; }

        public DisposableQueue()
        {
            TestQueue = new DataStructures.Queue<T>();
        }

        public void Dispose()
        {
            TestQueue = new DataStructures.Queue<T>();
        }
    }
}
