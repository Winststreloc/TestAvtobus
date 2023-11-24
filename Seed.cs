using System.Transactions;
using NHibernate;
using TestAvtobus.Interfaces;
using TestAvtobus.Models;
using TestAvtobus.Models.NHibernate;
using ISession = NHibernate.ISession;

namespace TestAvtobus;

public class Seed
{
    public async Task SeedDataContext()
    {
        var recordUrl = new RecordURL()
        {
            CountJump = 827,
            CreatedAt = DateTime.Now,
            OriginalUrl = "https://github.com/Winststreloc",
            ShortUrl = "https://short.url.ru"
        };

        using (var session = NHibernateHelper.OpenSession())
        {
            if (!session.Query<RecordURL>().Any())
            {
                using (var transaction = session.BeginTransaction())
                {
                    await session.SaveOrUpdateAsync(recordUrl);
                    await transaction.CommitAsync();
                }
            }
        }
    }
}