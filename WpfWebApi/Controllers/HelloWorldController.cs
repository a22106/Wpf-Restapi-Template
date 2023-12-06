using Microsoft.AspNetCore.Mvc;

namespace WpfWebApi.Controllers
{
    [ApiController]
    [Route("/")]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Hello World!";
        }   
    }
}
