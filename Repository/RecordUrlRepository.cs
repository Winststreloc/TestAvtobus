using NHibernate;
using NHibernate.Linq;
using TestAvtobus.Dto;
using TestAvtobus.Interfaces;
using TestAvtobus.Models;
using TestAvtobus.Models.NHibernate;
using ISession = NHibernate.ISession;

namespace TestAvtobus.Repository;

public class RecordUrlRepository : IRecordUrlRepository
{
    private readonly ISession _session = NHibernateHelper.OpenSession();

    public async Task<RecordURL> GetRecord(long id)
    {
        return await _session.Query<RecordURL>().SingleOrDefaultAsync(r => r.Id == id);
    }

    public async Task<List<RecordURL>> GetAllRecords()
    {
        return await _session.Query<RecordURL>().ToListAsync();
    }

    public async Task InsertRecord(RecordUrlDto recordUrlDto)
    {
        var newRecord = new RecordURL()
        {
            OriginalUrl = recordUrlDto.OriginalUrl,
            ShortUrl = recordUrlDto.ShortUrl,
            CountJump = 0,
            CreatedAt = DateTime.Now
        };
        await _session.SaveOrUpdateAsync(newRecord);
        ITransaction transaction = _session.BeginTransaction();
        transaction.Commit();
    }

    public async Task UpdateRecord(RecordURL recordUrl)
    {
        await _session.UpdateAsync(recordUrl);
        ITransaction transaction = _session.BeginTransaction();
        transaction.Commit();
    }

    public async Task DeleteRecord(long id)
    {
        var recordUrl = await _session.Query<RecordURL>().SingleOrDefaultAsync(r => r.Id == id);
        await _session.DeleteAsync(recordUrl);
        ITransaction transaction = _session.BeginTransaction();
        transaction.Commit();
    }
    
}