using Microsoft.AspNetCore.Mvc;


namespace MediumPostApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppController : ControllerBase
    {
        [HttpGet(Name = "TestFn")]
        public string Get()
        {
            return "Hello World";
        }
    }
}
