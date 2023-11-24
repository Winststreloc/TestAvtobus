using FluentNHibernate.Mapping;

namespace TestAvtobus.Models.Mapping;

public class RecordURLMap : ClassMap<RecordURL>
{
    public RecordURLMap()
    {
        Id(x => x.Id);
        Map(x => x.CreatedAt);
        Map(x => x.OriginalUrl);
        Map(x => x.ShortUrl);
        Map(x => x.CountJump);
    }
}