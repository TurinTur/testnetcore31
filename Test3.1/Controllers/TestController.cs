using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test3._1.Controllers
{

    public class TestController : ControllerBase
    {

        [Route("api/helloworld")]
        public string HelloWorld()
        {
            return "Hello World. Time is: " + DateTime.Now.ToString();
        }

        [Route("api/helloworldjson")]
        public object HelloWorldJson()
        {
            return new
            {
                Message = "Hello World. Time is: " + DateTime.Now.ToString(),
                Time = DateTime.Now
            };
        }

        [HttpPost]
        [Route("api/helloworldpost")]
        public object HelloWorldPost(string name)
        {
            return $"Hello {name}. Time is: " + DateTime.Now.ToString();
        }

    }
}