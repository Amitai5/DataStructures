using System;
using System.Collections.Generic;
using Xunit;

namespace DataStructuresTester.Lists
{
    public class ArrayListTester : DisposableArrayList<int?>
    {
        private const int sampleSize = 100;
        private readonly Random random = new Random();

        [Fact]
        public void TestAdd()
        {
            TestList.Add(null);
            Assert.Single(TestList);

            int initialCount = TestList.Count + 1;
            for (int i = 0; i < sampleSize; i++)
            {
                TestList.Add(random.Next());
                Assert.Equal(i + initialCount, TestList.Count);
            }
            Assert.NotEmpty(TestList);

            TestList.Clear();
            int?[] filledValues = FillList();
            AssertListEqual(filledValues);
        }

        [Fact]
        public void TestClone()
        {
            FillList();

            DataStructures.Lists.ArrayList<int?> copiedList = (DataStructures.Lists.ArrayList<int?>)TestList.Clone();
            Assert.Equal(TestList.Count, copiedList.Count);

            Assert.NotSame(TestList, copiedList);
            AssertListEqual(copiedList);
        }

        [Fact]
        public void TestContains()
        {
            int?[] filledValues = FillList();

            bool foundImpossible = TestList.Contains(-1);
            Assert.False(foundImpossible);

            for(int i = 0; i < sampleSize; i++)
            {
                int? randomValue = filledValues[random.Next(TestList.Count)];
                bool foundValue = TestList.Contains(randomValue);
                Assert.True(foundValue);
            }
        }

        [Fact]
        public void TestCopyTo()
        {
            int?[] numbersAdded = FillList();
            int?[] copiedList = new int?[TestList.Count];

            TestList.CopyTo(copiedList, 0);
            AssertListEqual(copiedList);

            int?[] smallList = new int?[TestList.Count / 2];
            int?[] largeList = new int?[TestList.Count * 2];

            Assert.Throws<IndexOutOfRangeException>(() => TestList.CopyTo(largeList, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => TestList.CopyTo(smallList, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => TestList.CopyTo(largeList, largeList.Length - 2));
        }

        [Fact]
        public void TestIndexOf()
        {
            int?[] numbersAdded = FillList();
            for (int i = 0; i < TestList.Count; i++)
            {
                int randomIndex = random.Next(TestList.Count);
                int? randomValue = numbersAdded[randomIndex];
                int foundIndex = TestList.IndexOf(randomValue);

                Assert.Equal(randomIndex, foundIndex);
            }

            int impossibleIndex = TestList.IndexOf(-25);
            Assert.Equal(-1, impossibleIndex);
        }

        [Fact]
        public void TestInsert()
        {
            FillList();
            TestList.Insert(0, -10);
            Assert.Equal(-10, TestList[0]);

            TestList.Insert(TestList.Count, -10);
            Assert.Equal(-10, TestList[^1]);

            for (int i = 0; i < sampleSize; i++)
            {
                int newNumber = random.Next();
                int randomIndex = random.Next(TestList.Count);

                TestList.Insert(randomIndex, newNumber);
                Assert.Equal(newNumber, TestList[randomIndex]);
            }

            Assert.Throws<IndexOutOfRangeException>(() => TestList.Insert(-1, 0));
            Assert.Throws<IndexOutOfRangeException>(() => TestList.Insert(TestList.Count + 1, 0));
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
        public void TestRemoveAt()
        {
            FillList();
            do
            {
                int randomIndex = random.Next(TestList.Count);
                int? itemToRemove = TestList[randomIndex];
                TestList.RemoveAt(randomIndex);

                if (randomIndex < TestList.Count - 1)
                {
                    Assert.NotEqual(itemToRemove, TestList[randomIndex]);
                }
            } while (TestList.Count > 0);
        }

        #region Helper Functions
        private void AssertListEqual(IList<int?> other)
        {
            for (int i = 0; i < TestList.Count; i++)
            {
                Assert.Equal(TestList[i], other[i]);
            }
        }

        private int?[] FillList()
        {
            int?[] numbersAdded = new int?[sampleSize];
            for (int i = 0; i < sampleSize; i++)
            {
                int newNumber = random.Next();
                numbersAdded[i] = newNumber;
                TestList.Add(newNumber);
            }

            Assert.NotEmpty(TestList);
            return numbersAdded;
        }
        #endregion Helper Functions
    }
}
