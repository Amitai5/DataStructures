using DataStructuresTester.Fixtures;
using System.Collections.Generic;
using System;
using Xunit;

namespace DataStructuresTester
{
    public class QueueTester : QueueFixture<int?>
    {
        public const int SampleSize = 20;
        private readonly Random random = new Random();

        [Fact]
        public void TestArrayResize()
        {
            const int initCapacity = DataStructures.Queue<int?>.InitCapacity;
            Assert.Equal(initCapacity, TestQueue.Capacity);

            TestQueue.Enqueue(1);
            TestQueue.Enqueue(2);
            TestQueue.Enqueue(3);
            TestQueue.Enqueue(4);
            Assert.Equal(initCapacity, TestQueue.Capacity);

            TestQueue.Enqueue(5);
            Assert.Equal(initCapacity * 2, TestQueue.Capacity);
        }

        [Fact]
        public void TestClear()
        {
            PopulateTestQueue();
            Assert.Equal(SampleSize, TestQueue.Count);

            TestQueue.Clear();
            Assert.Empty(TestQueue);
        }

        [Fact]
        public void TestContains()
        {
            PopulateTestQueue();
            for (int i = 0; i < TestQueue.Count; i++)
            {
                Assert.True(TestQueue.Contains(i + 2));
            }

            Assert.False(TestQueue.Contains(-25));
            Assert.False(TestQueue.Contains(5000));
        }

        [Fact]
        public void TestCopyTo()
        {
            PopulateTestQueue();
            int?[] copyArray = new int?[SampleSize + 1];
            TestQueue.CopyTo(copyArray, 1);

            for (int i = 1; i < copyArray.Length; i++)
            {
                Assert.Equal(TestQueue.Dequeue(), copyArray[i]);
            }

            PopulateTestQueue();
            int?[] smallList = new int?[TestQueue.Count / 2];
            int?[] largeList = new int?[TestQueue.Count * 2];

            Assert.Throws<IndexOutOfRangeException>(() => TestQueue.CopyTo(largeList, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => TestQueue.CopyTo(smallList, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => TestQueue.CopyTo(largeList, largeList.Length - 2));
        }

        [Fact]
        public void TestDequeue()
        {
            PopulateTestQueue();
            Assert.Equal(SampleSize, TestQueue.Count);

            int currentValue = 2;
            int queueCount = SampleSize;

            do
            {
                Assert.Equal(currentValue, TestQueue.Dequeue());
                currentValue++;

                queueCount--;
                Assert.Equal(queueCount, TestQueue.Count);

            } while (TestQueue.Count > 0);
            Assert.Empty(TestQueue);
        }

        [Fact]
        public void TestEnqueue()
        {
            PopulateTestQueue();
            Assert.Equal(SampleSize, TestQueue.Count);

            int currentValue = 2;
            do
            {
                Assert.Equal(currentValue, TestQueue.Dequeue());
                currentValue++;

            } while (TestQueue.Count > 0);

            TestQueue.Enqueue(null);
            Assert.Null(TestQueue.Dequeue());
        }

        [Fact]
        public void TestEnumerator()
        {
            PopulateTestQueue();
            IEnumerator<int?> enumerator = TestQueue.GetEnumerator();

            int currentIndex = 2;
            while (enumerator.MoveNext())
            {
                Assert.Equal(currentIndex, enumerator.Current);
                currentIndex++;
            }
        }

        [Fact]
        public void TestPeek()
        {
            PopulateTestQueue();

            int currentValue = 2;
            int queueCount = SampleSize;

            do
            {
                Assert.Equal(currentValue, TestQueue.Peek());
                Assert.Equal(queueCount, TestQueue.Count);

                Assert.Equal(TestQueue.Peek(), TestQueue.Dequeue());
                queueCount--;

                Assert.Equal(queueCount, TestQueue.Count);
                currentValue++;

            } while (TestQueue.Count > 0);
        }

        [Fact]
        public void TestToArray()
        {
            PopulateTestQueue();
            int?[] copyArray = TestQueue.ToArray();

            for (int i = 0; i < copyArray.Length; i++)
            {
                Assert.Equal(TestQueue.Dequeue(), copyArray[i]);
            }
        }

        [Fact]
        public void TestTrimExcess()
        {
            PopulateTestQueue();
            Assert.Equal(32, TestQueue.Capacity);
            Assert.Equal(SampleSize, TestQueue.Count);

            TestQueue.TrimExcess();
            Assert.Equal(SampleSize, TestQueue.Capacity);
        }

        [Fact]
        public void TestTryDequeue()
        {
            Assert.False(TestQueue.TryDequeue(out int? badResult));
            Assert.Null(badResult);

            TestQueue.Enqueue(100);
            Assert.True(TestQueue.TryDequeue(out int? goodResult));
            Assert.Equal(100, goodResult);
            Assert.Empty(TestQueue);
        }

        [Fact]
        public void TestTryPeek()
        {
            Assert.False(TestQueue.TryPeek(out int? badResult));
            Assert.Null(badResult);

            TestQueue.Enqueue(100);
            Assert.True(TestQueue.TryPeek(out int? goodResult));
            Assert.Equal(100, goodResult);
            Assert.Single(TestQueue);
        }

        private void PopulateTestQueue()
        {
            for (int i = 0; i < SampleSize; i++)
            {
                TestQueue.Enqueue(i + 2);
            }
        }
    }
}