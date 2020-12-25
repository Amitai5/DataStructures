import org.junit.Before;
import org.junit.Test;

import static org.junit.Assert.*;

public class QueueTester {
    private Queue<Integer> testQueue;

    @Test
    public void testClear() {
        testQueue.enqueue(1);
        testQueue.enqueue(2);
        testQueue.enqueue(3);
        testQueue.enqueue(4);
        testQueue.clear();

        Integer[] items = testQueue.toArray();
        assertArrayEquals(items, new Integer[]{ });
    }

    @Test
    public void testContains() {
        for(int i = 0; i < 100; i++) {
            testQueue.enqueue(i);
        }

        assertFalse(testQueue.contains(500));
        assertTrue(testQueue.contains(95));
        assertTrue(testQueue.contains(1));
        assertTrue(testQueue.contains(2));
    }

    @Test
    public void testCopyTo() {
        testQueue.enqueue(1);
        testQueue.enqueue(2);
        testQueue.enqueue(3);
        testQueue.enqueue(4);

        Integer[] items = new Integer[10];
        testQueue.copyTo(items, 1);

        assertEquals(1, items[1].intValue());
        assertEquals(2, items[2].intValue());
        assertEquals(3, items[3].intValue());
        assertEquals(4, items[4].intValue());
    }

    @Test
    public void testEnqueue() {
        for(int i = 0; i < 100; i++) {
            testQueue.enqueue(i);
        }

        Integer[] items = testQueue.toArray();
        assertEquals(1, items[1].intValue());
        assertEquals(2, items[2].intValue());
        assertEquals(3, items[3].intValue());
        assertEquals(98, items[98].intValue());
    }

    @Test
    public void testDequeue() {
        testQueue.enqueue(1);
        testQueue.enqueue(2);
        testQueue.enqueue(3);
        testQueue.enqueue(4);

        assertEquals(1, testQueue.dequeue().intValue());
        assertEquals(2, testQueue.dequeue().intValue());
        assertEquals(3, testQueue.dequeue().intValue());
        assertEquals(4, testQueue.dequeue().intValue());
    }

    @Test
    public void testPeek() {
        testQueue.enqueue(1);
        testQueue.enqueue(2);
        testQueue.enqueue(3);
        testQueue.enqueue(4);

        assertEquals(1, testQueue.peek().intValue());
        testQueue.dequeue();
        assertEquals(2, testQueue.peek().intValue());
        testQueue.dequeue();
        assertEquals(3, testQueue.peek().intValue());
        testQueue.dequeue();
        assertEquals(4, testQueue.peek().intValue());
        testQueue.dequeue();
    }

    @Test
    public void testCount() {
        testQueue.enqueue(1);
        testQueue.enqueue(2);
        testQueue.enqueue(3);
        testQueue.enqueue(4);

        assertEquals(4, testQueue.size());
        testQueue.dequeue();
        assertEquals(3, testQueue.size());
        testQueue.dequeue();
        assertEquals(2, testQueue.size());
        testQueue.dequeue();
        assertEquals(1, testQueue.size());
        testQueue.dequeue();
    }

    @Test
    public void testCapacity() {
        assertEquals(testQueue.InitCapacity, testQueue.getCapacity());
        testQueue.enqueue(1);
        testQueue.enqueue(2);
        testQueue.enqueue(3);
        testQueue.enqueue(4);
        assertEquals(testQueue.InitCapacity, testQueue.getCapacity());

        testQueue.enqueue(5);
        assertEquals(testQueue.InitCapacity * 2, testQueue.getCapacity());
    }

    @Before
    public void initQueue() {
        testQueue = new Queue<>(Integer.class);
    }
}
