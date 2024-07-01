using System.Collections;

public class MyList<T> : IEnumerable<T>
{
    private record Node
    {
        public required T value;
        public Node? prevNode;
        public Node? nextNode;
    }

    Node head;
    Node tail;

    public int Lenght { get; private set; } = 0;

    public void PushFirst(T value) => head = InsertNode(null, head, value);

    public void PushLast(T value) => tail = InsertNode(tail, null, value);

    public void Insert(int idx, T value)
    {
        var nodeBefor = GetNodeAt(idx);
        InsertNode(nodeBefor, nodeBefor.nextNode, value);
    }

    private Node InsertNode(Node? prevNode, Node? nextNode, T value)
    {
        Node newNode = new Node() { value = value, nextNode = nextNode, prevNode = prevNode };
        if (IsFirstElement())
            AddFirstNode(newNode);
        else
            UpdateRefs(prevNode, nextNode, newNode);
        Lenght++;
        return newNode;
    }

    private bool IsFirstElement() => head is null;

    private void AddFirstNode(Node node)
    {
        head = node;
        tail = node;
    }

    private void UpdateRefs(Node? nodeBefor, Node? nodeAfter, Node insertNode)
    {
        if (nodeBefor is not null)
            nodeBefor.nextNode = insertNode;
        if (nodeAfter is not null)
            nodeAfter.prevNode = insertNode;
    }

    public T GetFirst() => GetAt(0);

    public T GetLast() => GetAt(Lenght - 1);

    public T GetAt(int idx) => GetNodeAt(idx).value;

    private Node GetNodeAt(int idx)
    {
        CheckIndex(idx);
        return idx < Lenght / 2 ? DirectPassage(idx) : ReversePassage(idx);
    }

    private void CheckIndex(int idx)
    {
        if (idx >= Lenght || idx < 0)
            throw new IndexOutOfRangeException();
    }

    private Node DirectPassage(int idx)
    {
        Node currentNode = head;
        for (int i = 0; i < idx; i++)
            currentNode = currentNode.nextNode;
        return currentNode;
    }

    private Node ReversePassage(int idx)
    {
        Node currentNode = tail;
        for (int i = Lenght - 1; i > idx; i--)
            currentNode = currentNode.prevNode;
        return currentNode;
    }

    public override string ToString() => string.Join(", ", this);

    public IEnumerator<T> GetEnumerator()
    {
        var currentNode = head;
        for (int i = 0; i < Lenght; i++)
        {
            yield return currentNode.value;
            currentNode = currentNode.nextNode;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
