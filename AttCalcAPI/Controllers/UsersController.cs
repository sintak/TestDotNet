using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AttCalcAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        //// GET: Products
        //[HttpGet]
        //[Route]
        //public async Task<IHttpActionResult> Index()

        //// GET: Products/5
        //[HttpGet]
        //[Route("{id:int}")]
        //public async Task<IHttpActionResult> Details(int? id)

        //// POST: Products/Discount/5
        //[HttpPost]
        //[Route("discount/{id:int}/{discount:decimal}")]
        //public async Task<IHttpActionResult> Discount(int id, decimal discount)

        //// POST: Products/Create
        //[HttpPost]
        //[Route]
        //public async Task<IHttpActionResult> Create(Product product)

        //// POST: Products/Edit/5
        //[HttpPost]
        //[Route("edit")]
        //public async Task<IHttpActionResult> Edit(Product product)

        // GET: api/Users
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Kode", "cnblogs" };
        }

        // GET api/Users/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return $"Kode-{id}";
        //}

        [HttpGet("{id}")]
        public object Get1()
        {
            return new { UserID="001", UserName="Admin" };
        }

        // POST api/Users
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        // POST api/Users
        [HttpPost]
        public IActionResult Post([FromBody]UserInfo userInfo)
        {
            //Model.Add(new User_Info
            //{
            //    Id = value.Id,
            //    Info = value.Info,
            //    Name = value.Name,
            //});
            ////用户登陆相关
            //return new HttpResponseMessage(HttpStatusCode.OK)
            //{
            //    Content = new StringContent("添加数据成功,用户ID：" + value.Id, System.Text.Encoding.UTF8, "text/plain")
            //};
            //return new HttpResponseMessage(HttpStatusCode.OK)
            //{
            //    Content = new StringContent("添加数据成功： phone=" + userInfo.phone)
            //};

            //------------
            //return "POST成功。 phone: " + userInfo.phone;
            return Ok(new { status = "POST成功", phone = userInfo.phone });
            //return new BadRequestObjectResult(new { error = "That is error!" });

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
    }

    public class UserInfo
    {
        public string phone { get; set; }

        public string countryCode { get; set; }

        public int type { get; set; }

        public string from { get; set; }
    }
}
