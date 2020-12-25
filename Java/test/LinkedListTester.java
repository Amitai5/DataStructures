import Lists.LinkedList;
import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class LinkedListTester extends ListTester {
    public LinkedListTester() {
        super(new LinkedList<>(Integer.class));
    }

    @Test
    public void testGetFirst() {
        assertEquals(null, getLinkedList().getFirst());

        PopulateTestList();
        assertEquals(getLinkedList().get(0), getLinkedList().getFirst());
    }

    @Test
    public void testGetLast() {
        assertEquals(null, getLinkedList().getLast());

        PopulateTestList();
        assertEquals(getLinkedList().get(getLinkedList().size() - 1), getLinkedList().getLast());
    }

    @Test
    public void testAddFirst() {
        PopulateTestList();
        getLinkedList().addFirst(-1);
        assertEquals(-1, getLinkedList().get(0).intValue());

        getLinkedList().addFirst(500);
        assertEquals(500, getLinkedList().getFirst().intValue());
    }

    @Test
    public void testAddLast() {
        PopulateTestList();
        getLinkedList().addLast(-1);
        assertEquals(-1, getLinkedList().get(getLinkedList().size() - 1).intValue());

        getLinkedList().addLast(500);
        assertEquals(500, getLinkedList().getLast().intValue());
    }

    private LinkedList<Integer> getLinkedList() {
        return (LinkedList<Integer>) testList;
    }
}
