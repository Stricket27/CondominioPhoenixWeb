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
    public class RepositoryResidencia : IRepositoryResidencia
    {
        public void Agregar(Residencia pResidencia)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    ctx.Residencia.Add(pResidencia);
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
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Residencia oResidencia = (from r in ctx.Residencia
                                              where r.ID_Residencia == id
                                              select r).FirstOrDefault();

                    if (oResidencia.EstadoActual == "Activo")
                    {
                        oResidencia.EstadoActual = "Inactivo";
                    }
                    else
                    {
                        oResidencia.EstadoActual = "Activo";
                    }

                    ctx.Entry(oResidencia).State = System.Data.Entity.EntityState.Modified;
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

        public Residencia Editar(Residencia pResidencia)
        {
            int retorno = 0;
            Residencia oResidencia = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oResidencia = GetResidenciaById(pResidencia.ID_Residencia);

                    ctx.Residencia.Add(pResidencia);
                    ctx.Entry(pResidencia).State = System.Data.Entity.EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }

                if(retorno >= 0)
                {
                    oResidencia = GetResidenciaById(pResidencia.ID_Residencia);
                }
                return oResidencia;
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

        public IEnumerable<ResidenciaUsuarioViewModel> GetResidencia()
        {
            try
            {
                IEnumerable<ResidenciaUsuarioViewModel> listaResidencia = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    listaResidencia = (
                        (from u in ctx.Usuario
                         join r in ctx.Residencia on u.ID_Usuario equals r.ID_Usuario

                         select new ResidenciaUsuarioViewModel
                         {
                             ID_Residencia = r.ID_Residencia.ToString(),
                             NumeroResidencia = r.NumeroResidencia,
                             CantidadPersonas = r.CantidadPersonas.ToString(),
                             AnnoInicio = r.AnnoInicio.ToString(),
                             EstadoResidencia = r.EstadoResidencia.ToString(),
                             EstadoActual = r.EstadoActual.ToString(),
                             CantidadAutos = r.CantidadAutos.ToString(),
                             NombreCompleto = u.Nombre.ToString() + " " + u.Apellidos.ToString()
                         }).ToList());
                }
                return listaResidencia;
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

        public Residencia GetResidenciaById(int id)
        {
            try
            {
                Residencia oResidencia = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oResidencia = ctx.Residencia.Find(id);
                }
                return oResidencia;
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
