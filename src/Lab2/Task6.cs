using System.Collections;

public class MyList<T> : IEnumerable<T>
{
    // Узел, хранящий значение и ссылки на следующий и предыдущий узлы
    private record Node
    {
        public required T value;
        public Node? prevNode;
        public Node? nextNode;
    }

    // Первый элемент списка
    Node headNode;
    // Последний элемент списка
    Node tailNode;

    public int Lenght { get; private set; } = 0;

    /// <summary>
    /// Вставляет значение в начало списка
    /// </summary>
    /// <param name="value"></param>
    public void PushFirst(T value) => headNode = InsertNode(null, headNode, value);

    /// <summary>
    /// Вставляет значение в конец списка
    /// </summary>
    /// <param name="value"></param>
    public void PushLast(T value) => tailNode = InsertNode(tailNode, null, value);

    /// <summary>
    /// Вставляет значение в список по указанному индексу
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="value"></param>
    public void Insert(int idx, T value)
    {
        var nodeAfter = GetNodeAt(idx);
        InsertNode(nodeAfter.prevNode, nodeAfter, value);
    }

    public T this[int idx]
    {
        get => GetNodeAt(idx).value;
        set => GetNodeAt(idx).value = value;
    }

    public MyList() { }

    public MyList(IEnumerable<T> collection)
    {
        foreach (var item in collection)
            PushLast(item);
    }

    //Cоздает узел и вставляет после prevNode и перед nextNode
    private Node InsertNode(Node? prevNode, Node? nextNode, T value)
    {
        Node newNode = new Node() { value = value, nextNode = nextNode, prevNode = prevNode };

        //Если это первый элемент, то необходима инициализация this.headNode и  this.tailNode
        if (IsFirstElement())
            AddFirstNode(newNode);
        else
            UpdateRefs(prevNode, nextNode, newNode);
        Lenght++;
        return newNode;
    }

    // Проверка, первый ли элемент добавляется
    private bool IsFirstElement() => headNode is null;

    // Добавляет первый узел, он одновременно является начальным и конечным
    private void AddFirstNode(Node node)
    {
        headNode = node;
        tailNode = node;
    }

    // Обновляет ссылку на следующий элемент у nodeBefor и ссылку на предыдущий элемент у nodeAfter
    private void UpdateRefs(Node? nodeBefor, Node? nodeAfter, Node insertNode)
    {
        if (nodeBefor is not null)
            nodeBefor.nextNode = insertNode;
        if (nodeAfter is not null)
            nodeAfter.prevNode = insertNode;
    }

    // Возвращает узел по указанному индексу.
    private Node GetNodeAt(int idx)
    {
        CheckValidIndex(idx);

        // Если idx в первой части списка, то для эффективности используем прямой проход,
        // иначе обратный 
        return idx < Lenght / 2 ? GetNodeUseDirectPassage(idx) : GetNodeUseReversePassage(idx);
    }

    // Проверка индекса на валидность
    private void CheckValidIndex(int idx)
    {
        if (idx >= Lenght || idx < 0)
            throw new IndexOutOfRangeException();
    }

    // Возвращает узел списка, проходя от первого узла к последнему 
    private Node GetNodeUseDirectPassage(int idx)
    {
        Node currentNode = headNode;
        for (int i = 0; i < idx; i++)
            currentNode = currentNode.nextNode;
        return currentNode;
    }

    // Возвращает узел списка, проходя от последнего узла к первому 
    private Node GetNodeUseReversePassage(int idx)
    {
        Node currentNode = tailNode;
        for (int i = Lenght - 1; i > idx; i--)
            currentNode = currentNode.prevNode;
        return currentNode;
    }

    public override string ToString() => string.Join(", ", this);

    public IEnumerator<T> GetEnumerator()
    {
        var currentNode = headNode;
        for (int i = 0; i < Lenght; i++)
        {
            yield return currentNode.value;
            currentNode = currentNode.nextNode;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
