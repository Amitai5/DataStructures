namespace DataStructures.Lists
{
    internal class LinkedListNode<T>
    {
        public T NodeData { get; set; }

        public LinkedListNode<T>? NextNode { get; set; }

        public LinkedListNode<T>? PreviousNode { get; set; }

        public LinkedListNode(T data, LinkedListNode<T>? previous)
        {
            this.NodeData = data;
            this.PreviousNode = previous;
        }
    }
}