using ApplicationCore.Utils;
using Infraestruture.Models;
using Infraestruture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicesUsuario : IServicesUsuario
    {
        public void Agregar(Usuario pUsuario)
        {
            IRepositoryUsuario repositoryUsuario = new RepositoryUsuario();

            string encryptarContrasenna = Cryptography.EncrypthAES(pUsuario.Contrasenna);
            pUsuario.Contrasenna = encryptarContrasenna;

            repositoryUsuario.Agregar(pUsuario);
        }

        public Usuario Editar(Usuario pUsuario)
        {
            IRepositoryUsuario repositoryUsuario = new RepositoryUsuario();
            return repositoryUsuario.Editar(pUsuario);
        }

        public void CambiarEstado(int id)
        {
            IRepositoryUsuario repositoryUsuario= new RepositoryUsuario();
            repositoryUsuario.CambiarEstado(id);
        }

        public List<TipoUsuario> GetTipoUsuarios()
        {
            IRepositoryUsuario repositoryUsuario = new RepositoryUsuario();
            return repositoryUsuario.GetTipoUsuarios();
        }

        public IEnumerable<Usuario> GetUsuario()
        {
            IRepositoryUsuario repositoryUsuario = new RepositoryUsuario();
            return repositoryUsuario.GetUsuario();
        }

        public Usuario GetUsuarioById(int id)
        {
            IRepositoryUsuario repositoryUsuario = new RepositoryUsuario();
            return repositoryUsuario.GetUsuarioById(id);
        }

        public Usuario IniciarSesion(string correo, string contrasenna)
        {
            IRepositoryUsuario repositoryUsuario = new RepositoryUsuario();

            string encryptarContrasenna = Cryptography.EncrypthAES(contrasenna);

            return repositoryUsuario.IniciarSesion(correo, encryptarContrasenna);
        }

        public Usuario GetUsuarioByCorreoElectronico(string correo)
        {
            IRepositoryUsuario repositoryUsuario = new RepositoryUsuario();
            return repositoryUsuario.GetUsuarioByCorreoElectronico(correo);
        }
    }
}
