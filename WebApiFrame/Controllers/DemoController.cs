using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiFrame.Controllers
{
    [Route("api/[controller]")]
    public class DemoController : Controller
    {
        #region 构造函数的选择
        private readonly ITestOne _testOne;
        private readonly ITestTwo _testTwo;
        private readonly ITestThree _testThree;

        //public DemoController(ITestOne testOne, ITestTwo testTwo, ITestThree testThree)
        //{
        //    _testOne = testOne;
        //    _testTwo = testTwo;
        //    _testThree = testThree;
        //}
        #endregion

        public ConfigOptions _cfgContent { get;  }

        #region 生命周期管理
        public ITestTransient _testTransient { get; }
        public ITestScoped _testScoped { get; }
        public ITestSingleton _testSingleton { get; }
        public TestService _testService { get; }

        public DemoController(ITestOne testOne, ITestTwo testTwo, ITestThree testThree
            , ITestTransient testTransient, ITestScoped testScoped, ITestSingleton testSingleton, TestService testService
            , IOptions<ConfigOptions> options
            )
        {
            this._cfgContent = options.Value;

            _testOne = testOne;
            _testTwo = testTwo;
            _testThree = testThree;

            _testTransient = testTransient;
            _testScoped = testScoped;
            _testSingleton = testSingleton;
            _testService = testService;
        }
        #endregion

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("Index")]
        public async Task Index() {
            HttpContext.Response.ContentType = "text/html";
            #region 构造函数的选择
            await HttpContext.Response.WriteAsync($"<h1>ITestOne => {this._testOne}</h1>");
            await HttpContext.Response.WriteAsync($"<h1>ITestTwo => {_testTwo}</h1>");
            await HttpContext.Response.WriteAsync($"<h1>ITestThree => {_testThree}</h1>");
            #endregion

            #region 生命周期管理
            await HttpContext.Response.WriteAsync($"<h1>Controller Log</h1>");
            await HttpContext.Response.WriteAsync($"<h6>Transient => {_testTransient.TargetId.ToString()}</h6>");
            await HttpContext.Response.WriteAsync($"<h6>Scoped => {_testScoped.TargetId.ToString()}</h6>");
            await HttpContext.Response.WriteAsync($"<h6>Singleton => {_testSingleton.TargetId.ToString()}</h6>");

            await HttpContext.Response.WriteAsync($"<h1>Service Log</h1>");
            await HttpContext.Response.WriteAsync($"<h6>Transient => {_testService.TestTransient.TargetId.ToString()}</h6>");
            await HttpContext.Response.WriteAsync($"<h6>Scoped => {_testService.TestScoped.TargetId.ToString()}</h6>");
            await HttpContext.Response.WriteAsync($"<h6>Singleton => {_testService.TestSingleton.TargetId.ToString()}</h6>");
            #endregion

            #region 配置管理
            await HttpContext.Response.WriteAsync($"<span>Ele1: {_cfgContent.Ele1}</span><br />");
            await HttpContext.Response.WriteAsync($"<span>Ele2.Sub1: {_cfgContent.Ele2.Sub1}</span><br />");
            await HttpContext.Response.WriteAsync($"<span>Ele2.Sub2: {_cfgContent.Ele2.Sub2}</span><br />");
            #endregion
        }
    }

    public interface ITestOne
    {

    }

    public class TestOne : ITestOne
    {

    }

    public interface ITestTwo
    {

    }

    public class TestTwo : ITestTwo
    {

    }

    public interface ITestThree
    {

    }

    public class TestThree : ITestThree
    {

    }

    // 
    public interface ITest
    {
        Guid TargetId { get; }
    }

    public interface ITestTransient : ITest { }
    public interface ITestScoped : ITest { }
    public interface ITestSingleton : ITest { }

    /// <summary>
    ///   (实现接口)
    /// </summary>
    public class TestInstance : ITestTransient, ITestScoped, ITestSingleton
    {
        public Guid TargetId
        {
            get
            {
                return _targetId;
            }
        }

        private Guid _targetId { get; set; }

        public TestInstance()
        {
            _targetId = Guid.NewGuid();
        }
    }

    /// <summary>
    ///   (拥有接口)
    /// </summary>
    public class TestService
    {
        public ITestTransient TestTransient { get; }
        public ITestScoped TestScoped { get; }
        public ITestSingleton TestSingleton { get; }

        public TestService(ITestTransient testTransient, ITestScoped testScoped, ITestSingleton testSingleton)
        {
            TestTransient = testTransient;
            TestScoped = testScoped;
            TestSingleton = testSingleton;
        }
    }

}
