using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoApi.Models;
using DemoApi.Repository;
using DemoApi.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DemoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class UsuariosController : Controller
    {
        private readonly IOptions<MySettings> appSettings;

        public UsuariosController(IOptions<MySettings> app) {
            appSettings = app;
        }

        [HttpGet]
        public IActionResult getAllUsers() {
            var data = DbClientFactory<UsuarioDao>.Instance.GetAllUsers(appSettings.Value.DBConn);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult saveUser([FromBody] Usuario user)
        {
            var msg = new Message<Usuario>();
            var data = DbClientFactory<UsuarioDao>.Instance.SaveUser(user,appSettings.Value.DBConn);
            if (data == "C200") {
                msg.IsSuccess = true;
                if (user.Id == 0) 
                    msg.ReturnMessage = "Usuario guardado correctamente.";
                else
                    msg.ReturnMessage = "Usuario actualizado correctamente.";
            }
            else if(data == "C201"){
                msg.IsSuccess = false;
                msg.ReturnMessage = "Email ya existe.";
            }
            else if (data == "C202"){
                msg.IsSuccess = false;
                msg.ReturnMessage = "Numero de celular ya existe.";
            }
            return Ok(msg);
        }

        [HttpGet]
        public IActionResult getUser(int id)
        {
            var data = DbClientFactory<UsuarioDao>.Instance.GetAllUsers(appSettings.Value.DBConn);
            var data = DbClientFactory<UsuarioDao>.Instance.Get;
            return Ok(data);
        }
    }
}