using DataStructures.Lists;
using DataStructuresTester.Fixtures;
using Xunit;

namespace DataStructuresTester
{
    public class ArrayListTester : ListTester, IClassFixture<ArrayListFixture<int?>>
    {
        private ArrayList<int?> TestArrayList => (ArrayList<int?>)Fixture.TestList;

        public ArrayListTester(ArrayListFixture<int?> listFixture)
            : base(listFixture)
        {

        }

        [Fact]
        public void TestArrayResize()
        {
            const int initCapacity = ArrayList<int?>.InitCapacity;
            Assert.Equal(initCapacity, TestArrayList.Capacity);

            TestArrayList.Add(1);
            TestArrayList.Add(2);
            TestArrayList.Add(3);
            TestArrayList.Add(4);
            Assert.Equal(initCapacity, TestArrayList.Capacity);

            TestArrayList.Add(5);
            Assert.Equal(initCapacity * 2, TestArrayList.Capacity);
        }

        [Fact]
        public void TestToArray()
        {
            PopulateTestList();
            int?[] copyArray = TestArrayList.ToArray();

            Assert.Equal(copyArray.Length, TestArrayList.Count);
            for (int i = 0; i < TestArrayList.Count; i++)
            {
                Assert.Equal(TestArrayList[i], copyArray[i]);
            }
        }
    }
}
