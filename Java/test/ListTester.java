import org.junit.Rule;
import org.junit.Test;
import org.junit.rules.ExpectedException;

import java.util.Arrays;
import java.util.List;
import java.util.ListIterator;

import static org.junit.Assert.*;

public class ListTester {
    protected List<Integer> testList;
    public final int SampleSize = 10;

    @Rule
    public final ExpectedException exception = ExpectedException.none();

    public ListTester(List<Integer> list) {
        testList = list;
    }

    @Test
    public void testContains() {
        PopulateTestList();
        for (int i = 0; i < testList.size(); i++) {
            assertTrue(testList.contains(i + 2));
        }
    }

    @Test
    public void testAdd() {
        PopulateTestList();
        assertEquals(SampleSize, testList.size());
        for (int i = 0; i < SampleSize; i++) {
            assertEquals(i + 2, testList.get(i).intValue());
        }

        testList.add(null);
        assertNull(testList.get(testList.size() - 1));
        assertEquals(SampleSize + 1, testList.size());
    }

    @Test
    public void testRemoveObject() {
        PopulateTestList();
        int currentSize = testList.size();

        do {
            currentSize--;
            testList.remove(testList.get(0));
            assertEquals(currentSize, testList.size());

        } while (testList.size() > 0);

        assertEquals(0, testList.size());
        assertTrue(testList.isEmpty());
    }

    @Test
    public void testContainsAll() {
        Integer[] tmpArr = new Integer[SampleSize];
        for (int i = 0; i < SampleSize; i++) {
            tmpArr[i] = i + 2;
        }

        PopulateTestList();
        List<Integer> tmpList = Arrays.asList(tmpArr);
        assertTrue(testList.containsAll(tmpList));
    }

    @Test
    public void testAddAll() {
        Integer[] tmpArr = new Integer[SampleSize];
        for (int i = 0; i < SampleSize; i++) {
            tmpArr[i] = i + 2;
        }

        List<Integer> tmpList = Arrays.asList(tmpArr);
        testList.addAll(tmpList);
        assertEquals(SampleSize, testList.size());
    }

    @Test
    public void testRemoveAll() {
        Integer[] tmpArr = new Integer[SampleSize];
        for (int i = 0; i < SampleSize; i++) {
            tmpArr[i] = i + 2;
        }

        PopulateTestList();
        List<Integer> tmpList = Arrays.asList(tmpArr);
        assertEquals(SampleSize, testList.size());

        testList.removeAll(tmpList);
        assertEquals(0, testList.size());
        assertTrue(testList.isEmpty());
    }

    @Test
    public void testRetainAll() {
        Integer[] tmpArr = new Integer[SampleSize / 2];
        for (int i = 0; i < SampleSize / 2; i++) {
            tmpArr[i] = i + 2;
        }

        PopulateTestList();
        List<Integer> tmpList = Arrays.asList(tmpArr);
        assertEquals(SampleSize, testList.size());

        assertTrue(testList.retainAll(tmpList));
        assertEquals(SampleSize / 2, testList.size());
    }

    @Test
    public void testClear() {
        PopulateTestList();
        assertFalse(testList.isEmpty());
        assertEquals(SampleSize, testList.size());

        testList.clear();
        assertTrue(testList.isEmpty());
        assertEquals(0, testList.size());
    }

    @Test
    public void testGetSet() {
        PopulateTestList();
        assertEquals(2, testList.get(0).intValue());
        testList.set(0, 100);
        assertEquals(100, testList.get(0).intValue());

        assertEquals(7, testList.get(5).intValue());
        testList.set(5, 100);
        assertEquals(100, testList.get(5).intValue());

        exception.expect(IndexOutOfBoundsException.class);
        testList.set(testList.size(), 10);
        testList.set(-1, 10);
        testList.get(-1);
    }

    @Test
    public void testAddAtIndex() {
        PopulateTestList();
        testList.add(5, null);
        assertEquals(SampleSize + 1, testList.size());
        assertNull(testList.get(5));

        testList.add(7, null);
        assertEquals(SampleSize + 2, testList.size());
        assertNull(testList.get(7));

        exception.expect(IndexOutOfBoundsException.class);
        testList.add(5000, 10);
        testList.add(-1, 10);
    }

    @Test
    public void testRemoveIndex() {
        PopulateTestList();
        int currentSize = testList.size();

        do {
            currentSize--;
            testList.remove(0);
            assertEquals(currentSize, testList.size());

        } while (testList.size() > 0);

        assertEquals(0, testList.size());
        assertTrue(testList.isEmpty());

        exception.expect(IndexOutOfBoundsException.class);
        testList.remove(testList.size());
    }

    @Test
    public void testIndexOf() {
        PopulateTestList();
        for (int i = 0; i < testList.size(); i++) {
            Integer currentValue = testList.get(i);
            assertEquals(i, testList.indexOf(currentValue));
        }
    }

    @Test
    public void testLastIndexOf() {
        testList.add(1);
        testList.add(2);
        testList.add(1);
        testList.add(3);

        assertEquals(2, testList.lastIndexOf(1));
    }

    @Test
    public void testIterator() {
        PopulateTestList();
        int currentIndex = 0;
        ListIterator<Integer> iterator = testList.listIterator();

        while (iterator.hasNext()) {
            Integer currentInt = iterator.next();
            assertEquals(testList.get(currentIndex), currentInt);
            currentIndex++;
        }
    }

    @Test
    public void testSubList() {
        PopulateTestList();
        List<Integer> subList = testList.subList(2, 6);

        for (int i = 0; i < subList.size(); i++) {
            int offsetIndex = i + 2;
            assertEquals(testList.get(offsetIndex), subList.get(i));
        }

        exception.expect(IndexOutOfBoundsException.class);
        testList.subList(0, 500);
        testList.subList(-1, 5);
    }

    protected void PopulateTestList() {
        for (int i = 0; i < SampleSize; i++) {
            testList.add(i + 2);
        }
    }
}
