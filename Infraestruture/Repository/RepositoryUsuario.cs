using Infraestructure.Utils;
using Infraestruture.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestruture.Repository
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
        public void Agregar(Usuario pUsuario)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    pUsuario.CorreoElectronico.ToLower();
                    ctx.Usuario.Add(pUsuario);
                    ctx.SaveChanges();
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Usuario Editar(Usuario pUsuario)
        {
            int retorno = 0;
            Usuario oUsuario = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = GetUsuarioById(pUsuario.ID_Usuario);

                    ctx.Usuario.Add(pUsuario);
                    pUsuario.CorreoElectronico.ToLower();
                    ctx.Entry(pUsuario).State = System.Data.Entity.EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }

                if(retorno >= 0)
                {
                    oUsuario = GetUsuarioById(pUsuario.ID_Usuario);
                }
                return oUsuario;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public void CambiarEstado(int id)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    Usuario oUsuario = (from u in ctx.Usuario
                                        where u.ID_Usuario == id
                                        select u).FirstOrDefault();

                    if (oUsuario.EstadoActual == "Activo")
                    {
                        oUsuario.EstadoActual = "Inactivo";
                    }
                    else
                    {
                        oUsuario.EstadoActual = "Activo";
                    }

                    ctx.Entry(oUsuario).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public List<TipoUsuario> GetTipoUsuarios()
        {
            try
            {
                List<TipoUsuario> listaTipoUsuario = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    listaTipoUsuario = ctx.TipoUsuario.ToList();
                }
                return listaTipoUsuario;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Usuario GetUsuarioById(int id)
        {
            try
            {
                Usuario oUsuario = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = ctx.Usuario.Find(id);
                }
                return oUsuario;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Usuario> GetUsuario()
        {
            try
            {
                IEnumerable<Usuario> listaUsuario = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    listaUsuario = ctx.Usuario.ToList();
                }
                return listaUsuario;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Usuario IniciarSesion(string correo, string contrasenna)
        {
            Usuario oUsuario = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = ctx.Usuario.
                     Where(u => u.CorreoElectronico.Equals(correo) && u.Contrasenna == contrasenna).
                    FirstOrDefault<Usuario>();
                }
                if (oUsuario != null)
                    oUsuario = GetUsuarioById(oUsuario.ID_Usuario);
                return oUsuario;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Usuario GetUsuarioByCorreoElectronico(string correo)
        {
            try
            {
                Usuario oUsuario = null;

                using (MyContext ctx = new MyContext())
                {
                    oUsuario = (from u in ctx.Usuario
                                        where u.CorreoElectronico == correo
                                        select u).FirstOrDefault();
                }
                return oUsuario;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
    }
}
