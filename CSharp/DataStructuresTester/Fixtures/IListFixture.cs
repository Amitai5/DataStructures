using System;
using System.Collections.Generic;

namespace DataStructuresTester.Fixtures
{
    public interface IListFixture<T> : IDisposable
    {
        public IList<T> TestList { get; }
    }
}
