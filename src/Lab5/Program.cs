namespace Lab5;

class Program
{
    static void Main(string[] args)
    {
        var workPlace = new Work() { OrganizationName = "BobLabs", PhoneNumber = "555-35-35" };
        var person = new Person() { Age = 5, WorkPlace = workPlace, Name = "Bob" };

        var resJson = MyJsonSerializer.Serialize(person);
        Console.WriteLine(resJson);
        var perss = MyJsonDeserializer.DeserializeFromJson<Person>(resJson);
    }
}
