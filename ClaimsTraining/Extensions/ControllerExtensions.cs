using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace ClaimsTraining.Extensions
{
    public static class ControllerExtensions
    {
        public static Int32 GetUserId(this Controller _Controller) =>
        Int32.Parse(_Controller.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value);

        public static String GetCurrentUserEmail(this Controller _Controller) =>
            _Controller.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
    }
}
