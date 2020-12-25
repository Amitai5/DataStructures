using System.Collections.Generic;

namespace DataStructuresTester.Fixtures
{
    public class ArrayListFixture<T> : IListFixture<T>
    {
        public IList<T> TestList { get; private set; }

        public ArrayListFixture()
        {
            TestList = new DataStructures.Lists.ArrayList<T>();
        }

        public void Dispose()
        {
            TestList = new DataStructures.Lists.ArrayList<T>();
        }
    }
}
