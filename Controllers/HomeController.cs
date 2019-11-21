using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopAuth.Models;
using ShopAuth.Repositories;
using ShopAuth.Services;

namespace ShopAuth.Controllers
{
    [Route("v1/account")]
    public class HomeController : Controller
    {
        private readonly IUserRepository repository;

        public HomeController(IUserRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]User model)
        {
            if (string.IsNullOrEmpty(model.Username))
                return NotFound(new { message = "Username não informado!" });

            if (string.IsNullOrEmpty(model.Password))
                return NotFound(new { message = "Senha não informada!" });

            var user = await this.repository.Get(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";

    }
}