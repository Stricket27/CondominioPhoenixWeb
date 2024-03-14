using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.ServicioEmail
{
    public class AdministradorDelSistema : Servicio
    {

        public AdministradorDelSistema()
        {
            enviarEmail = "mquirosca@est.utn.ac.cr";
            contrasenia = "Quiros27";
            host = "smtp.office365.com";
            port = 587;
            ssl = true;
            InicializarSmtpClent();
        }


    }
}
