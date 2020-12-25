using DataStructuresTester.Fixtures;
using System.Collections.Generic;
using System;
using Xunit;

namespace DataStructuresTester
{
    public abstract class ListTester : IDisposable
    {
        public const int SampleSize = 20;
        protected Random random = new Random();
        protected IListFixture<int?> Fixture { get; init; }

        protected ListTester(IListFixture<int?> listFixture)
        {
            Fixture = listFixture;
        }

        public void Dispose()
        {
            Fixture.Dispose();
        }

        [Fact]
        public void TestAdd()
        {
            PopulateTestList();
            Assert.Equal(SampleSize, Fixture.TestList.Count);

            for (int i = 0; i < Fixture.TestList.Count; i++)
            {
                Assert.Equal(i + 2, Fixture.TestList[i]);
            }

            Fixture.TestList.Add(null);
            Assert.Null(Fixture.TestList[^1]);
        }

        [Fact]
        public void TestClear()
        {
            PopulateTestList();
            Assert.Equal(SampleSize, Fixture.TestList.Count);

            Fixture.TestList.Clear();
            Assert.Empty(Fixture.TestList);
        }

        [Fact]
        public void TestContains()
        {
            PopulateTestList();
            for (int i = 0; i < Fixture.TestList.Count; i++)
            {
                Assert.True(Fixture.TestList.Contains(i + 2));
            }

            Assert.False(Fixture.TestList.Contains(-25));
            Assert.False(Fixture.TestList.Contains(5000));
        }

        [Fact]
        public void TestCopyTo()
        {
            PopulateTestList();
            int?[] copyArray = new int?[SampleSize + 1];
            Fixture.TestList.CopyTo(copyArray, 1);

            for (int i = 0; i < Fixture.TestList.Count; i++)
            {
                Assert.Equal(Fixture.TestList[i], copyArray[i + 1]);
            }

            int?[] smallList = new int?[Fixture.TestList.Count / 2];
            int?[] largeList = new int?[Fixture.TestList.Count * 2];

            Assert.Throws<IndexOutOfRangeException>(() => Fixture.TestList.CopyTo(largeList, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => Fixture.TestList.CopyTo(smallList, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => Fixture.TestList.CopyTo(largeList, largeList.Length - 2));
        }

        [Fact]
        public void TestEnumerator()
        {
            PopulateTestList();
            IEnumerator<int?> enumerator = Fixture.TestList.GetEnumerator();

            int currentIndex = 0;
            while (enumerator.MoveNext())
            {
                Assert.Equal(Fixture.TestList[currentIndex], enumerator.Current);
                currentIndex++;
            }
        }

        [Fact]
        public void TestIndexOf()
        {
            PopulateTestList();
            for (int i = 0; i < Fixture.TestList.Count; i++)
            {
                int? numbertoFind = Fixture.TestList[i];
                int foundIndex = Fixture.TestList.IndexOf(numbertoFind);
                Assert.Equal(i, foundIndex);
            }

            int impossibleIndex = Fixture.TestList.IndexOf(-25);
            Assert.Equal(-1, impossibleIndex);
        }

        [Fact]
        public void TestInsert()
        {
            PopulateTestList();
            Fixture.TestList.Insert(0, -10);
            Assert.Equal(-10, Fixture.TestList[0]);

            Fixture.TestList.Insert(Fixture.TestList.Count, -10);
            Assert.Equal(-10, Fixture.TestList[^1]);

            for (int i = 0; i < SampleSize; i++)
            {
                int newNumber = random.Next();
                int randomIndex = random.Next(Fixture.TestList.Count);

                Fixture.TestList.Insert(randomIndex, newNumber);
                Assert.Equal(newNumber, Fixture.TestList[randomIndex]);
            }

            Assert.Throws<IndexOutOfRangeException>(() => Fixture.TestList.Insert(-1, 0));
            Assert.Throws<IndexOutOfRangeException>(() => Fixture.TestList.Insert(Fixture.TestList.Count + 1, 0));
        }

        [Fact]
        public void TestRemove()
        {
            PopulateTestList();
            int lastListCount = Fixture.TestList.Count;

            do
            {
                int randomIndex = random.Next(Fixture.TestList.Count);
                int? itemToRemove = Fixture.TestList[randomIndex];
                Fixture.TestList.Remove(itemToRemove);
                lastListCount--;

                Assert.Equal(lastListCount, Fixture.TestList.Count);
                Assert.DoesNotContain(itemToRemove, Fixture.TestList);

            } while (Fixture.TestList.Count > 0);
        }

        [Fact]
        public void TestRemoveAt()
        {
            PopulateTestList();
            int lastListCount = Fixture.TestList.Count;

            do
            {
                int randomIndex = random.Next(Fixture.TestList.Count);
                int? itemToRemove = Fixture.TestList[randomIndex];
                Fixture.TestList.RemoveAt(randomIndex);
                lastListCount--;

                Assert.Equal(lastListCount, Fixture.TestList.Count);
                Assert.DoesNotContain(itemToRemove, Fixture.TestList);

            } while (Fixture.TestList.Count > 0);
        }

        protected void PopulateTestList()
        {
            for (int i = 0; i < SampleSize; i++)
            {
                Fixture.TestList.Add(i + 2);
            }
        }
    }
}
