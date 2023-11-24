using TestAvtobus.Dto;
using TestAvtobus.Models;

namespace TestAvtobus.Interfaces;

public interface IRecordUrlRepository
{
    Task<RecordURL> GetRecord(long id);
    Task<List<RecordURL>> GetAllRecords();
    Task InsertRecord(RecordUrlDto recordUrlDto);
    Task UpdateRecord(RecordURL recordUrl);
    Task DeleteRecord(long id);
}