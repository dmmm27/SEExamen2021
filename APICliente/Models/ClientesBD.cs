using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICliente.Models
{
    public class ClientesBD
    {
        public int cli_codigo_cliente { get; set; }
        public string cli_nombre1 { get; set; }
        public string cli_nombre2 { get; set; }
        public string cli_apellido1 { get; set; }
        public string cli_apellido2 { get; set; }
        public string cli_apellido_casada { get; set; }
        public string cli_direccion { get; set; }
        public int cli_telefono1 { get; set; }
        public int cli_telefono2 { get; set; }

        public string cli_identificacion { get; set; }
        public DateTime cli_fecha_nacimiento { get; set; }

    }
}
