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
                string response = JsonConvert.SerializeObject(loginRespond);
                if (loginRespond.Pass)
                    return Ok(response);
                else
                    return BadRequest(response);
            }
            catch
            {
                return BadRequest(model);
            }
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