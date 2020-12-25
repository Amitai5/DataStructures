using System.Collections.Generic;

namespace DataStructuresTester.Fixtures
{
    public class LinkedListFixture<T> : IListFixture<T>
    {
        public IList<T> TestList { get; private set; }

        public LinkedListFixture()
        {
            TestList = new DataStructures.Lists.LinkedList<T>();
        }

        public void Dispose()
        {
            TestList = new DataStructures.Lists.LinkedList<T>();
        }
    }
}
