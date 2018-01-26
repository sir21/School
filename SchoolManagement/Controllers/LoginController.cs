using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolManagement.Interfaces;
using SchoolManagement.Models.StudentModels;
using SchoolManagement.Services;

namespace SchoolManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/login/[action]")]
    public class LoginController : Controller
    {
        private IAccountService _accountService;

        public LoginController(AccountServices accountService)
        {
            _accountService = accountService;
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Register
        public ActionResult Register()
        {
            return Ok();
        }

        // POST: Login/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] StudentRegisterModel studentRegisterModel)
        {
            try
            {
                if (await _accountService.RegistrationService(studentRegisterModel))
                    return Ok();
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: Login/Login
        public ActionResult Login()
        {
            return Ok();
        }

        // POST: Login/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] StudentLoginModel model)
        {
            try
            {
                LoginRespond loginRespond = await _accountService.LoginServiceAsync(model);
                if (loginRespond.Pass)
                    return Ok(loginRespond);
                else
                    return BadRequest(loginRespond);
            }
            catch
            {
                return BadRequest(model);
            }
        }

        // GET: Login/Logout
        public ActionResult Logout()
        {
            return Ok();
        }

        // POST: Login/Logout
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Logout([FromBody] StudentLoginModel model)
        {
            if(_accountService.Authenticate(model.Email, model.Password))
            {
                await _accountService.LogoutService(model.Email);

                return Ok();
            }

            return BadRequest();
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}