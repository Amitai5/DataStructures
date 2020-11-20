using DataStructuresTester.Disposables;
using System.Data;
using System;
using Xunit;

namespace DataStructuresTester.Lists
{
    public class LinkedListTester : DisposableLinkedList<int?>
    {
        private const int sampleSize = 100;
        private readonly Random random = new Random();

        [Fact]
        public void TestAdd()
        {
            Assert.Throws<NoNullAllowedException>(() => TestList.Add(null));
            Assert.Empty(TestList);

            for (int i = 0; i < sampleSize; i++)
            {
                TestList.Add(random.Next());
                Assert.Equal(i + 1, TestList.Count);
            }
            Assert.NotEmpty(TestList);
        }

        [Fact]
        public void TestAddFirst()
        {
            Assert.Throws<NoNullAllowedException>(() => TestList.AddFirst(null));
            Assert.Empty(TestList);

            for (int i = 0; i < sampleSize; i++)
            {
                int newValue = random.Next();
                TestList.AddFirst(newValue);

                Assert.Equal(newValue, TestList[0]);
                Assert.Equal(i + 1, TestList.Count);
            }
            Assert.NotEmpty(TestList);
        }

        [Fact]
        public void TestAddLast()
        {
            Assert.Throws<NoNullAllowedException>(() => TestList.AddLast(null));
            Assert.Empty(TestList);

            for (int i = 0; i < sampleSize; i++)
            {
                int newValue = random.Next();
                TestList.AddLast(newValue);

                Assert.Equal(newValue, TestList[^1]);
                Assert.Equal(i + 1, TestList.Count);
            }
            Assert.NotEmpty(TestList);
        }

        [Fact]
        public void TestContains()
        {
            int[] numbersAdded = FillList();
            for (int i = 0; i < sampleSize; i++)
            {
                int searchItem = numbersAdded[i];
                bool foundItem = TestList.Contains(searchItem);
                Assert.True(foundItem);
            }
        }

        [Fact]
        public void TestCopyTo()
        {
            int[] numbersAdded = FillList();
            int?[] copiedList = new int?[TestList.Count];
            TestList.CopyTo(copiedList, 0);

            for (int i = 0; i < numbersAdded.Length; i++)
            {
                Assert.Equal(numbersAdded[i], copiedList[i]);
            }

            int?[] smallList = new int?[TestList.Count / 2];
            int?[] largeList = new int?[TestList.Count * 2];

            Assert.Throws<IndexOutOfRangeException>(() => TestList.CopyTo(largeList, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => TestList.CopyTo(smallList, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => TestList.CopyTo(largeList, largeList.Length - 2));
        }

        [Fact]
        public void TestInsert()
        {
            FillList();
            TestList.Insert(10, 0);
            Assert.Equal(10, TestList[0]);

            TestList.Insert(-10, TestList.Count);
            Assert.Equal(-10, TestList[^1]);

            for (int i = 0; i < sampleSize; i++)
            {
                int newNumber = random.Next();
                int randomIndex = random.Next(TestList.Count);

                TestList.Insert(newNumber, randomIndex);
                Assert.Equal(newNumber, TestList[randomIndex]);
            }

            Assert.Throws<IndexOutOfRangeException>(() => TestList.Insert(0, -1));
            Assert.Throws<IndexOutOfRangeException>(() => TestList.Insert(0, TestList.Count + 1));
        }

        [Fact]
        public void TestRemove()
        {
            FillList();
            do
            {
                int randomIndex = random.Next(TestList.Count);
                int? itemToRemove = TestList[randomIndex];
                TestList.Remove(itemToRemove);

                if (randomIndex < TestList.Count - 1)
                {
                    Assert.NotEqual(itemToRemove, TestList[randomIndex]);
                }
            } while (TestList.Count > 0);
        }

        [Fact]
        public void TestRemoveFirst()
        {
            FillList();
            do
            {
                int? headValue = TestList[0];
                TestList.RemoveFirst();

                Assert.NotEqual(headValue, TestList[0]);
            } while (TestList.Count > 1);

            TestList.RemoveFirst();
            Assert.Empty(TestList);
        }

        [Fact]
        public void TestRemoveLast()
        {
            FillList();
            do
            {
                int? tailValue = TestList[^1];
                TestList.RemoveLast();

                Assert.NotEqual(tailValue, TestList[^1]);
            } while (TestList.Count > 1);

            TestList.RemoveLast();
            Assert.Empty(TestList);
        }

        #region Helper Functions
        private int[] FillList()
        {
            int[] numbersAdded = new int[sampleSize];
            for (int i = 0; i < sampleSize; i++)
            {
                int newNumber = random.Next();
                numbersAdded[i] = newNumber;
                TestList.Add(newNumber);
            }

            return numbersAdded;
        }
        #endregion Helper Functions
    }
}