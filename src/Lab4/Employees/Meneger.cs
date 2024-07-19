internal class Meneger : Consultant
{
    public Meneger(string name) : base(name) { }

    public override AccessLevel GetAccessLevel() => AccessLevel.High;
}
