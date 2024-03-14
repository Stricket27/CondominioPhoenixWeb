using Infraestruture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServicesUsuario
    {
        //TODOS LOS USUARIOS
        IEnumerable<Usuario> GetUsuario();
        Usuario GetUsuarioById(int id);
        void Agregar(Usuario pUsuario);
        Usuario Editar(Usuario pUsuario);
        void CambiarEstado(int id);
        //TODOS LOS TIPOS DE USUARIOS
        List<TipoUsuario> GetTipoUsuarios();
        Usuario IniciarSesion(string correo, string contrasenna);
        Usuario GetUsuarioByCorreoElectronico(string correo);
    }
}
