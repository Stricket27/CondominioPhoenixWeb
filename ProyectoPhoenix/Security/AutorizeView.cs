
using Infraestruture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Web.Security
{
    public class AutorizeView
    {
        public static bool IsUserInRole(string[] nombreRoles)
        {
            IEnumerable<TipoUsuarioAsiganado> allowedroles = nombreRoles.
                Select(a => (TipoUsuarioAsiganado)Enum.Parse(typeof(TipoUsuarioAsiganado), a));
            bool authorize = false;
            var oUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            if (oUsuario != null)
            {
                foreach (var rol in allowedroles)
                {
                    if ((int)rol == oUsuario.ID_TipoUsuario)
                        return true;
                }
            }

            return authorize;
        }
    }
}