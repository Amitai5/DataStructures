import java.lang.reflect.Array;
import java.security.InvalidParameterException;
import java.util.NoSuchElementException;
import java.util.Iterator;

import static java.lang.String.format;

/**
 * Represents a first-in, first-out collection of T, objects.
 *
 * @param <T> Specifies the element type of the Queue.
 */
public class Queue<T> {
    /**
     * The initial capacity of the Queue if none is given in the constructor of the class.
     */
    public final int InitCapacity = 4;

    private Class<T> nodeClassType;
    private T[] backingArray;

    private int queueTail = -1;
    private int queueHead = 0;
    private int count = 0;

    /**
     * Initializes a new instance of the Queue class that is empty and has the default initial capacity.
     *
     * @param itemType The class-type of the items the Queue will store.
     */
    public Queue(Class<T> itemType) {
        backingArray = (T[]) Array.newInstance(itemType, InitCapacity);
        nodeClassType = itemType;
        queueTail = -1;
    }

    /**
     * Initializes a new instance of the Queue class that is empty and has the specified initial capacity.
     *
     * @param itemType The class-type of the items the Queue will store.
     * @param capacity The initial number of elements that the Queue can contain.
     */
    public Queue(Class<T> itemType, int capacity) {
        if (capacity <= 0) {
            throw new InvalidParameterException("The capacity of the ArrayList must be initialized as a positive non-zero number.");
        }

        backingArray = (T[]) Array.newInstance(itemType, capacity);
        nodeClassType = itemType;
        queueTail = -1;
    }

    /**
     * Removes all objects from the Queue.
     */
    public void clear() {
        queueTail = -1;
        queueHead = 0;
        count = 0;
    }

    /**
     * Determines whether an element is in the Queue.
     *
     * @param item The object to locate in the Queue. The value can be null.
     * @return True if item is found in the Queue; otherwise, false.
     */
    public boolean contains(T item) {
        for (int i = queueHead; i < count; i++) {
            int cyclicalIndex = i % backingArray.length;
            if (backingArray[cyclicalIndex] == null && item == null) {
                return true;
            } else if (backingArray[cyclicalIndex] != null && backingArray[cyclicalIndex].equals(item)) {
                return true;
            }
        }
        return false;
    }

    /**
     * Copies the Queue elements to an existing one-dimensional System.Array, starting at the specified array index.
     *
     * @param array      The one-dimensional System.Array that is the destination of the elements copied from Queue. The System.Array must have zero-based indexing.
     * @param arrayIndex The zero-based index in array at which copying begins.
     */
    public void copyTo(T[] array, int arrayIndex) {
        if (array == null) {
            throw new NullPointerException("The array to copy to cannot be null.");
        }

        if (array.length - arrayIndex < count) {
            throw new IndexOutOfBoundsException(format("The array of length, {0}, does not have enough space to copy the contents " +
                    "of the Queue starting at index {1}.", array.length, queueTail));
        }

        if (arrayIndex < 0 || arrayIndex >= array.length) {
            throw new IndexOutOfBoundsException(format("The start index, {0} is an invalid starting point for the given array.", queueTail));
        }
        System.arraycopy(backingArray, queueHead, array, arrayIndex, count);
    }

    /**
     * Removes and returns the object at the beginning of the Queue.
     *
     * @return The object that is removed from the beginning of the Queue.
     */
    public T dequeue() {
        if (isEmpty()) {
            throw new NoSuchElementException("Queue underflow");
        }

        count--;
        queueHead++;
        int previous = queueHead - 1;
        return backingArray[previous % backingArray.length];
    }

    /**
     * Adds an object to the end of the Queue.
     *
     * @param item The object to add to the Queue. The value can be null.
     */
    public void enqueue(T item) {
        count++;
        resize();
        backingArray[queueTail % backingArray.length] = item;
    }

    /**
     * Returns the object at the beginning of the Queue without removing it.
     *
     * @return The object at the beginning of the Queue.
     */
    public T peek() {
        if (isEmpty()) {
            throw new NoSuchElementException("Queue underflow");
        }
        return backingArray[queueHead % backingArray.length];
    }

    /**
     * Copies the Queue elements to a new array.
     *
     * @return A new array containing elements copied from the Queue.
     */
    public T[] toArray() {
        T[] returnArray = (T[]) Array.newInstance(nodeClassType, count);
        System.arraycopy(backingArray, 0, returnArray, 0, count);
        return returnArray;
    }

    /**
     * Checks if the Queue needs to be resized. If resized, it doubles the count of the backing array.
     */
    private void resize() {
        queueTail++;
        if (count <= backingArray.length) {
            if (queueHead == backingArray.length) {
                queueHead = 0;
            }
            return;
        }

        T[] newBackingArray = (T[]) Array.newInstance(nodeClassType, backingArray.length * 2);
        System.arraycopy(backingArray, 0, newBackingArray, 0, backingArray.length);

        backingArray = newBackingArray;
        queueTail = count - 1;
        queueHead = 0;
    }

    /**
     * Returns an iterator that iterates over the items in the Queue
     *
     * @return an iterator that iterates over the items in the Queue
     */
    public Iterator<T> iterator() {
        return new QueueIterator();
    }

    /**
     * Represents an Iterator designed to traverse an array-backed Queue.
     */
    private class QueueIterator implements Iterator<T> {
        private int currentIndex = 0;

        public boolean hasNext() {
            return currentIndex < count;
        }

        public void remove() {
            throw new UnsupportedOperationException();
        }

        public T next() {
            if (!hasNext()) {
                throw new NoSuchElementException();
            }

            currentIndex++;
            return backingArray[currentIndex - 1];
        }
    }

    /**
     * @return Gets the number of elements that the Queue can contain at the current count.
     */
    public int getCapacity() {
        return backingArray != null ? backingArray.length : 0;
    }

    /**
     * @return Gets a value indicating whether the Queue has any elements within it. True if the Queue is empty; otherwise, false.
     */
    public boolean isEmpty() {
        return count == 0;
    }

    /**
     * @return Gets the number of elements contained in the Queue.
     */
    public int size() {
        return count;
    }
}