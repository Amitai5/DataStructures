import Lists.ArrayList;
import Lists.LinkedList;
import org.junit.Test;

import static junit.framework.TestCase.assertEquals;

public class ArrayListTester extends ListTester {
    public ArrayListTester() {
        super(new ArrayList<Integer>(Integer.class));
    }

    @Test
    public void testArrayResize() {
        final int initCapacity = getArrayList().InitCapacity;
        assertEquals(initCapacity, getArrayList().capacity());

        getArrayList().add(1);
        getArrayList().add(2);
        getArrayList().add(3);
        getArrayList().add(4);
        assertEquals(initCapacity, getArrayList().capacity());

        getArrayList().add(5);
        assertEquals(initCapacity * 2, getArrayList().capacity());
    }

    private ArrayList<Integer> getArrayList() {
        return (ArrayList<Integer>) testList;
    }
}
