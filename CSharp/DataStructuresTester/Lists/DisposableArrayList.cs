using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresTester.Lists
{
    public class DisposableArrayList<T> : IDisposable
    {
        public DataStructures.Lists.ArrayList<T> TestList { get; private set; }

        public DisposableArrayList()
        {
            TestList = new DataStructures.Lists.ArrayList<T>();
        }

        public void Dispose()
        {
            TestList = new DataStructures.Lists.ArrayList<T>();
        }
    }
}
