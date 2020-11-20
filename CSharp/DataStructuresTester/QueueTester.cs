using DataStructuresTester.Disposables;
using System;
using System.Collections.Generic;
using Xunit;

namespace DataStructuresTester
{
    public class QueueTester : DisposableQueue<int?>
    {
        private const int sampleSize = 100;
        private readonly Random random = new Random();

        [Fact]
        public void TestCircularity()
        {
            int nextTargetCapacity = TestQueue.Capacity;
            for (int i = 0; i < sampleSize; i++)
            {
                TestQueue.Enqueue(random.Next());
                Assert.Equal(i + 1, TestQueue.Count);

                TestQueue.Dequeue();
                Assert.Equal(i, TestQueue.Count);

                TestQueue.Enqueue(random.Next());
                Assert.Equal(i + 1, TestQueue.Count);

                if (i == nextTargetCapacity)
                {
                    nextTargetCapacity *= 2;
                    Assert.Equal(nextTargetCapacity, TestQueue.Capacity);
                }
            }
        }

        [Fact]
        public void TestContains()
        {
            int?[] filledValues = FillQueue();

            bool foundImpossible = TestQueue.Contains(-1);
            Assert.False(foundImpossible);

            for (int i = 0; i < sampleSize; i++)
            {
                int? randomValue = filledValues[random.Next(TestQueue.Count)];
                bool foundValue = TestQueue.Contains(randomValue);
                Assert.True(foundValue);
            }
        }

        [Fact]
        public void TestCopyTo()
        {
            int?[] numbersAdded = FillQueue();
            int?[] copiedList = new int?[TestQueue.Count];

            TestQueue.CopyTo(copiedList, 0);
            AssertQueueEqual(copiedList);
            FillQueue();

            int?[] smallQueue = new int?[TestQueue.Count / 2];
            int?[] largeQueue = new int?[TestQueue.Count * 2];

            Assert.Throws<IndexOutOfRangeException>(() => TestQueue.CopyTo(largeQueue, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => TestQueue.CopyTo(smallQueue, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => TestQueue.CopyTo(largeQueue, largeQueue.Length - 2));
        }

        [Fact]
        public void TestEnqueue()
        {
            TestQueue.Enqueue(null);
            Assert.Single(TestQueue);

            int initialCount = TestQueue.Count + 1;
            for (int i = 0; i < sampleSize; i++)
            {
                TestQueue.Enqueue(random.Next());
                Assert.Equal(i + initialCount, TestQueue.Count);
            }
        }

        [Fact]
        public void TestDequeue()
        {
            for (int i = 0; i < sampleSize; i++)
            {
                int randomVal = random.Next();
                TestQueue.Enqueue(randomVal);
                Assert.Single(TestQueue);

                int? retValue = TestQueue.Dequeue();
                Assert.Equal(randomVal, retValue);
                Assert.Empty(TestQueue);
            }
        }

        [Fact]
        public void TestPeek()
        {
            for (int i = 0; i < sampleSize; i++)
            {
                int randomVal = random.Next();
                TestQueue.Enqueue(randomVal);
                Assert.Single(TestQueue);

                int? peekVal = TestQueue.Peek();
                Assert.Equal(randomVal, peekVal);

                Assert.NotEmpty(TestQueue);
                TestQueue.Clear();
            }
        }

        [Fact]
        public void TestToArray()
        {
            int?[] numbersAdded = FillQueue();
            int?[] copiedArray = TestQueue.ToArray();

            Assert.NotSame(TestQueue.ToArray(), copiedArray);
            Assert.Equal(numbersAdded, copiedArray);
        }

        [Fact]
        public void TestTrimExcess()
        {
            int?[] numbersAdded = FillQueue();
            Assert.NotEqual(numbersAdded.Length, TestQueue.Capacity);

            TestQueue.TrimExcess();
            Assert.Equal(numbersAdded.Length, TestQueue.Capacity);
            AssertQueueEqual(numbersAdded);
        }

        [Fact]
        public void TestTryDequeue()
        {
            bool shouldNotWork = TestQueue.TryDequeue(out int? result);
            Assert.False(shouldNotWork);
            Assert.Null(result);

            TestQueue.Enqueue(random.Next());

            bool shouldWork = TestQueue.TryDequeue(out int? result2);
            Assert.True(shouldWork);
            Assert.Empty(TestQueue);
            Assert.NotNull(result2);
        }

        [Fact]
        public void TestTryPeek()
        {
            bool shouldNotWork = TestQueue.TryPeek(out int? result);
            Assert.False(shouldNotWork);
            Assert.Null(result);

            TestQueue.Enqueue(random.Next());

            bool shouldWork = TestQueue.TryPeek(out int? result2);
            Assert.Single(TestQueue);
            Assert.True(shouldWork);
            Assert.NotNull(result2);
        }

        #region Helper Methods
        private void AssertQueueEqual(IList<int?> other)
        {
            for (int i = 0; i < TestQueue.Count; i++)
            {
                Assert.Equal(TestQueue.Dequeue(), other[i]);
            }
        }

        private int?[] FillQueue()
        {
            int?[] numbersAdded = new int?[sampleSize];
            for (int i = 0; i < sampleSize; i++)
            {
                int newNumber = random.Next();
                numbersAdded[i] = newNumber;
                TestQueue.Enqueue(newNumber);
            }

            Assert.NotEmpty(TestQueue);
            return numbersAdded;
        }
        #endregion Helper Methods
    }
}