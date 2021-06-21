using System;

namespace WebApplication1.Controllers
{
    internal class AuthorizedAttribute : Attribute
    {
        public string Role { get; set; }
    }
}