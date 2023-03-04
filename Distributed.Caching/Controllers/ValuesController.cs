using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Distributed.Caching.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    readonly IDistributedCache _distributedCache;

    public ValuesController(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    [HttpGet("set")]

    public async Task<IActionResult> set(string Name,string surName)
    {
        await _distributedCache.SetStringAsync(Name, surName);
        await _distributedCache.SetAsync("surName", Encoding.UTF8.GetBytes(surName));
        return Ok();
    }
  
    [HttpGet("get")]

    public async Task<IActionResult> get()
    {
        var name = await _distributedCache.GetStringAsync("name");
        var surnameBinary = await _distributedCache.GetAsync("name");
        var surName = Encoding.UTF8.GetString(surnameBinary);
        return Ok(new
        {
            name,
            surName
        });
    }

}