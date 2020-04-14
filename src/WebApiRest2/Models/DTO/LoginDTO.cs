using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiRest2.Models.DTO
{
    public class LoginDTO
    {
        public decimal Idempresa { get; set; }
        public decimal IdUsuarios { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

    }
}
