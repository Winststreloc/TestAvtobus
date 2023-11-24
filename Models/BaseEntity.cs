namespace TestAvtobus.Models;

public class BaseEntity
{
    public virtual long Id { get; set; }
    public virtual DateTime CreatedAt { get; set; }
}