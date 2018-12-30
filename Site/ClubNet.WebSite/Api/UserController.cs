namespace ClubNet.WebSite.Api
{
    using ClubNet.Shared.Api.Contracts;
    using ClubNet.Shared.Api.Dto;

    using Microsoft.AspNetCore.Mvc;

    using System;
    using System.Threading.Tasks;

    // For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

    /// <summary>
    /// Define the user api controller 
    /// </summary>
    [Route("{lang}/api/user")]
    [ApiController]
    public class UserController : ControllerBase, IUserApi
    {
        #region Methods

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [Route("create")]
        public async Task<IApiResponse> CreateUserAccountAsync([FromBody]RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }

        #endregion

        // GET: api/<controller>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
