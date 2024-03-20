using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using TestODataWithEf.DotNet6.Context;
using TestODataWithEf.DotNet6.Model;

namespace TestODataWithEf.DotNet6.Controllers;

[ApiController]
[Route("[controller]")]
public class FakeController : ControllerBase
{
    private readonly FakeContext _context;
    private readonly ILogger<FakeController> _logger;

    public FakeController(
        ILogger<FakeController> logger,
        FakeContext context)
    {
        _context = context;
        _logger = logger;
    }

    [EnableQuery]
    [HttpGet("enable-query")]
    public ActionResult<IQueryable<FakeModel>> Get()
    {
        return Ok(
            _context.Fakes    
        );
    }
}
