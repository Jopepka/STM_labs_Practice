internal record FieldChangeInfo
{
    public int Id { get; set; }
    public DateTime ChangeTime { get; set; }
    public int IdEntity { get; set; }
    public string FieldName { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    public string Editor { get; set; }
}
