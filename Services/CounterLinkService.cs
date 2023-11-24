using TestAvtobus.Interfaces;

namespace TestAvtobus.Services;

public class CounterLinkService : ICounterLinkService
{
    private readonly IRecordUrlRepository _recordUrlRepository;

    public CounterLinkService(IRecordUrlRepository recordUrlRepository)
    {
        _recordUrlRepository = recordUrlRepository;
    }

    public async Task<string> CountJumpLink(long id)
    {
        var recordUrl = await _recordUrlRepository.GetRecord(id);
        recordUrl.CountJump++;
        await _recordUrlRepository.UpdateRecord(recordUrl);
        return recordUrl.OriginalUrl;
    }
}