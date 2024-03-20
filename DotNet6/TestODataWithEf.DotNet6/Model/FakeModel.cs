namespace TestODataWithEf.DotNet6.Model;

public class FakeModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime InsertedAt { get; set; } = DateTime.UtcNow;
}
