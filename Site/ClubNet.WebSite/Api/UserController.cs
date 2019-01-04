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
    }
}
