using DataStructures.Lists;
using DataStructuresTester.Fixtures;
using Xunit;

namespace DataStructuresTester
{
    public class LinkedListTester : ListTester, IClassFixture<LinkedListFixture<int?>>
    {
        private LinkedList<int?> TestLinkedList => (LinkedList<int?>)Fixture.TestList;

        public LinkedListTester(LinkedListFixture<int?> listFixture)
            : base(listFixture)
        {

        }

        [Fact]
        public void TestAddFirst()
        {
            PopulateTestList();
            TestLinkedList.AddFirst(-1);
            Assert.Equal(-1, TestLinkedList[0]);

            TestLinkedList.AddFirst(500);
            Assert.Equal(500, TestLinkedList.First);
        }

        [Fact]
        public void TestAddLast()
        {
            PopulateTestList();
            TestLinkedList.AddLast(-1);
            Assert.Equal(-1, TestLinkedList[^1]);

            TestLinkedList.AddLast(500);
            Assert.Equal(500, TestLinkedList.Last);
        }

        [Fact]
        public void TestRemoveFirst()
        {
            PopulateTestList();
            int? firstValue = TestLinkedList[0];
            TestLinkedList.RemoveFirst();
            Assert.DoesNotContain(firstValue, TestLinkedList);

            firstValue = TestLinkedList.First;
            TestLinkedList.RemoveFirst();
            Assert.DoesNotContain(firstValue, TestLinkedList);
        }

        [Fact]
        public void TestRemoveLast()
        {
            PopulateTestList();
            int? lastValue = TestLinkedList[^1];
            TestLinkedList.RemoveLast();
            Assert.DoesNotContain(lastValue, TestLinkedList);

            lastValue = TestLinkedList.Last;
            TestLinkedList.RemoveLast();
            Assert.DoesNotContain(lastValue, TestLinkedList);
        }
    }
}
