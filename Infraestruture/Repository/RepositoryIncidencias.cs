using Infraestructure.Utils;
using Infraestruture.Models;
using Infraestruture.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestruture.Repository
{
    public class RepositoryIncidencias : IRepositoryIncidencias
    {
        public void Agregar(Incidencias pIncidencias)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    ctx.Incidencias.Add(pIncidencias);
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

        public void CambiarEstado(int id)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    Incidencias oIncidencias = (from i in ctx.Incidencias
                                                where i.ID_Incidencias == id
                                                select i).FirstOrDefault();

                    if (oIncidencias.EstadoActual == "Activo")
                    {
                        oIncidencias.EstadoActual = "Inactivo";
                    }
                    else
                    {
                        oIncidencias.EstadoActual = "Activo";
                    }

                    ctx.Entry(oIncidencias).State = System.Data.Entity.EntityState.Modified;
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

        public Incidencias Editar(Incidencias pIncidencias)
        {
            int retorno = 0;
            Incidencias oIncidencias = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oIncidencias = GetIncidenciasById(pIncidencias.ID_Incidencias);

                    ctx.Incidencias.Add(pIncidencias);
                    ctx.Entry(pIncidencias).State = System.Data.Entity.EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }

                if (retorno == 0)
                {
                    oIncidencias = GetIncidenciasById(pIncidencias.ID_Incidencias);
                }
                return oIncidencias;
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

        public IEnumerable<IncidenciaUsuarioViewModel> GetIncidencias()
        {
            try
            {
                IEnumerable<IncidenciaUsuarioViewModel> listaIncidencias = null;

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    listaIncidencias = (
                        (from u in ctx.Usuario
                         join i in ctx.Incidencias on u.ID_Usuario equals i.ID_Usuario

                         select new IncidenciaUsuarioViewModel
                         {
                             ID_Incidencias = i.ID_Incidencias.ToString(),
                             Titulo = i.Titulo.ToString(),
                             Descripcion = i.Descripcion.ToString(),
                             FechaIncidencia = i.FechaIncidencia.ToString(),
                             EstadoSolicitud = i.EstadoSolicitud.ToString(),
                             EstadoActual = i.EstadoActual.ToString(),
                             NombreCompleto = u.Nombre.ToString() + " " + u.Apellidos.ToString()
                         }).ToList());
                }
                return listaIncidencias;
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

        public Incidencias GetIncidenciasById(int id)
        {
            try
            {
                Incidencias oIncidencias = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oIncidencias = ctx.Incidencias.Find(id);
                }
                return oIncidencias;
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

        public List<Usuario> GetUsuario()
        {
            try
            {
                List<Usuario> listaUsuario = null;
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
    }
}
