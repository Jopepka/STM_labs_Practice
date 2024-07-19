internal record Client
{
    public int id { get; set; }
    public string FirstName { get; set; }
    public string MidleName { get; set; }
    public string LastName { get; set; }
    public int PhoneNumber { get; set; }
    public int PassportNumber { get; set; }
    public int PassportSeries { get; set; }
}