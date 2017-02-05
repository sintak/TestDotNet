using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiFrame.Models;
using Microsoft.Extensions.Logging;
using System.Collections;
using WebApiFrame.Core.Filters;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiFrame.Repository;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiFrame.Controllers
{
    [Route("api/[controller]")]
    //[MyActionFilter]  // 标识在控制器上，则访问这个控制器下的所有方法都将调用这个过滤器
    //[TypeFilter(typeof(MyActionFilterAttribute), Arguments = new object[] { "My-Header", "WebApiFrame-Header" })]  // ### 通过TypeFilter引入
    [MyActionFilter("Class")]
    public class UsersController : Controller
    {
        private ILogger<UsersController> _logger;

        private readonly IUserRepository _userRepository;

        public UsersController(ILogger<UsersController> logger, IUserRepository userRepo)
        {
            this._logger = logger;
            this._userRepository = userRepo;
        }

        //public UsersController(IUserRepository userRepository)
        //{
        //    this._userRepository = userRepository;
        //}

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Controller Executd!");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Controller Executing!");
        }

        [HttpGet("{id}")]
        //[MyActionFilter]
        [MyActionFilter("Method", -1)]
        public IActionResult Get(int id)
        {
            //Tester.main();

            // 演示日志输出
            _logger.LogInformation("This is Information Log!");
            _logger.LogWarning("This is Warning Log!");
            _logger.LogError("This is Error Log!");

            //var user = new User() { Id = id, Name = "Name:" + id, Sex = "Male" };
            var user = this._userRepository.GetById(id);
            return new ObjectResult(user);
        }

        [HttpGet]
        public IActionResult GetAll() {
            //throw new Exception("GetAll function failed");

            var list = this._userRepository.GetAll();
            return new ObjectResult(list);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            // TODO：新增操作
            user.Id = new Random().Next(1, 10);
            return CreatedAtAction("Get", new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            // TODO: 更新操作
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // TODO: 删除操作

        }
    }
}
