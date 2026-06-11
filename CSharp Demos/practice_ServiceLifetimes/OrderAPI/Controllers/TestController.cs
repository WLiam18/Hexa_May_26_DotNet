using Microsoft.AspNetCore.Mvc;
using OrderAPI.Services;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITransientGuid _transient1;
        private readonly ITransientGuid _transient2;
        private readonly IScopedGuid _scoped1;
        private readonly IScopedGuid _scoped2;
        private readonly ISingletonGuid _singleton1;
        private readonly ISingletonGuid _singleton2;

        public TestController(
            ITransientGuid t1, ITransientGuid t2,
            IScopedGuid s1, IScopedGuid s2,
            ISingletonGuid sg1, ISingletonGuid sg2)
        {
            _transient1 = t1;
            _transient2 = t2;
            _scoped1 = s1;
            _scoped2 = s2;
            _singleton1 = sg1;
            _singleton2 = sg2;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = new
            {
                Transient = new
                {
                    Instance1 = _transient1.GetGuid(),
                    Instance2 = _transient2.GetGuid(),
                    Same = _transient1.GetGuid() == _transient2.GetGuid()
                },
                Scoped = new
                {
                    Instance1 = _scoped1.GetGuid(),
                    Instance2 = _scoped2.GetGuid(),
                    Same = _scoped1.GetGuid() == _scoped2.GetGuid()
                },
                Singleton = new
                {
                    Instance1 = _singleton1.GetGuid(),
                    Instance2 = _singleton2.GetGuid(),
                    Same = _singleton1.GetGuid() == _singleton2.GetGuid()
                }
            };

            return Ok(result);
        }
    }
}