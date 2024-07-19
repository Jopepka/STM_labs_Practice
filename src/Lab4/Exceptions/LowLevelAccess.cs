internal class LowLevelAccess : Exception
{
    public LowLevelAccess() : base("A higher access level is required") { }
}