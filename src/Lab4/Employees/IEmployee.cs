using System.Reflection.Metadata.Ecma335;

internal interface IEmployee
{
    string Name { get; }

    AccessLevel GetAccessLevel();

    string UserToString();
}