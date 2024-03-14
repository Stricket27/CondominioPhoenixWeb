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
    public class RepositoryInformacion : IRepositoryInformacion
    {
        public void Agregar(Informacion pInformacion)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    ctx.Informacion.Add(pInformacion);
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
                    Informacion oInformacion = (from i in ctx.Informacion
                                                where i.ID_Informacion == id
                                                select i).FirstOrDefault();

                    if(oInformacion.EstadoActual == "Activo")
                    {
                        oInformacion.EstadoActual = "Inactivo";
                    }
                    else
                    {
                        oInformacion.EstadoActual = "Activo";
                    }

                    ctx.Entry(oInformacion).State = System.Data.Entity.EntityState.Modified;
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

        public Informacion Editar(Informacion pInformacion)
        {
            int retorno = 0;
            Informacion oInformacion = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oInformacion = GetInformacionById(pInformacion.ID_Informacion);

                    ctx.Informacion.Add(pInformacion);
                    ctx.Entry(pInformacion).State = System.Data.Entity.EntityState.Modified;

                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                {
                    oInformacion = GetInformacionById(pInformacion.ID_Informacion);
                }
                return oInformacion;
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

        public IEnumerable<Informacion> GetInformacion()
        {
            try
            {
                IEnumerable<Informacion> listaInformacion = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    listaInformacion = ctx.Informacion.ToList();
                }

                return listaInformacion;
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

        public Informacion GetInformacionById(int id)
        {
            try
            {
                Informacion oInformacion = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oInformacion = ctx.Informacion.Find(id);
                }
                return oInformacion;
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
