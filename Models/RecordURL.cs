namespace TestAvtobus.Models;

public class RecordURL : BaseEntity
{
    public virtual string OriginalUrl { get; set; }
    public virtual string ShortUrl { get; set; }
    public virtual int CountJump { get; set; }
}