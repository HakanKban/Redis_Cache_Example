using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IMemoryCache _memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        //AbsolueTime => cache'lenen datanın kesin olarak ömrünü belirtilmesidir.
        //SlidingTime => Belirtilen süre zarfında veriye erişim yapıldığında ömrü bir o kadar uzar. İki zamanlama modeli beraber kullanılabilir.

        [HttpGet("set/{name}")]
        public void SetName(string name)
        {
            _memoryCache.Set("Name", name);
        }

        [HttpGet]
        public string? GetName()
        {
           return _memoryCache.Get<string>("Name");
        }


        [HttpGet]
        public void SetDate() 
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
        }

        [HttpGet("getDate")]
        public DateTime GetDate()
        {
            return _memoryCache.Get<DateTime>("date");
        }


    }
}
