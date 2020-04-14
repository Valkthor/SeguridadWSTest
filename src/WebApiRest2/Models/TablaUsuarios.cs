using System;
using System.Collections.Generic;

namespace WebApiRest2.Models
{
    public partial class TablaUsuarios
    {
        public decimal IdIntegracionAdministracion { get; set; }
        public decimal Idempresa { get; set; }
        public decimal IdUsuarios { get; set; }
        public string Pass { get; set; }
        public decimal? Estado { get; set; }
        public string Usuario { get; set; }
        public string ClaveApp { get; set; }
        public string SistemaIntegracion { get; set; }
        public decimal? Tipo { get; set; }
    }
}
