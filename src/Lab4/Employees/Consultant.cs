internal class Consultant : IEmployee
{
    public string Name { get; }

    public Consultant(string name) => Name = name;

    public virtual AccessLevel AccessLevel => AccessLevel.Lower;
}