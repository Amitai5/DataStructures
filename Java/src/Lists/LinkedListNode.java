package Lists;

class LinkedListNode<T> {
    public LinkedListNode<T> PreviousNode = null;
    public LinkedListNode<T> NextNode = null;
    public T Data = null;

    public LinkedListNode(T data, LinkedListNode<T> previousNode) {
        PreviousNode = previousNode;
        Data = data;
    }
}
