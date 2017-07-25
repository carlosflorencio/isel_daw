using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Contracts;
using API.Models;
using API.Services;
using API.TransferModels.InputModels;
using API.TransferModels.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class HelperController : Controller
    {
        [HttpGet("exception")]
        public async Task<IActionResult> Exception()
        {
            throw new Exception("Custom Exception");
        }
    }
}