using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using TestAvtobus.Dto;
using TestAvtobus.Interfaces;
using TestAvtobus.Models;
using TestAvtobus.Models.NHibernate;
using ISession = NHibernate.ISession;

namespace TestAvtobus.Controllers;

public class RecordController : Controller
{
    private readonly ILinkShortenerService _shortenerService;
    private readonly IRecordUrlRepository _recordUrlRepository;
    private readonly ICounterLinkService _counterLinkService;

    public RecordController(ILinkShortenerService shortenerService, IRecordUrlRepository recordUrlRepository, ICounterLinkService counterLinkService)
    {
        _shortenerService = shortenerService;
        _recordUrlRepository = recordUrlRepository;
        _counterLinkService = counterLinkService;
    }

    public IActionResult GetAllRecords()
    {
        using ISession session = NHibernateHelper.OpenSession();

        var recordURL = session.Query<RecordURL>().ToList();

        return View(recordURL);
    }
    public IActionResult CreateRecord()
    {
        return View("CreateRecord");
    }

    [HttpGet("generate-shorter-link")]
    public IActionResult GenerateShorterLink([FromQuery]string? shortUrl)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newShorterUrl = _shortenerService.ShortenUrl(shortUrl);
        return Ok(newShorterUrl);
    }

    [HttpPost("insert-record")]
    public IActionResult InsertRecord([FromBody]RecordUrlDto recordUrlDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _recordUrlRepository.InsertRecord(recordUrlDto);
        
        return StatusCode(201);
    }
    
    
    [HttpDelete("delete-record")]
    public IActionResult DeleteRecord(long id)
    {
        _recordUrlRepository.DeleteRecord(id);
        return NoContent();
    }

    [HttpPatch("counter-increment")]
    public async Task<IActionResult> CounterIncrement([FromQuery]long id)
    {
        var originalUrl = await _counterLinkService.CountJumpLink(id);
        return Ok(Json(new {redirectingUrl = originalUrl}));
    }
    
}