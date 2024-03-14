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
    public class RepositoryRubroCobro : IRepositoryRubroCobro
    {
        public void Agregar(RubroCobro pRubroCobro)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    ctx.RubroCobro.Add(pRubroCobro);
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

        public RubroCobro Editar(RubroCobro pRubroCobro)
        {
            int retorno = 0;
            RubroCobro oRubroCobro = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oRubroCobro = GetRubroCobroById(pRubroCobro.ID_RubroCobro);

                    ctx.RubroCobro.Add(pRubroCobro);
                    ctx.Entry(pRubroCobro).State = System.Data.Entity.EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }
                if(retorno >= 0)
                {
                    oRubroCobro = GetRubroCobroById(pRubroCobro.ID_RubroCobro);
                }

                return oRubroCobro;
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
                    RubroCobro oRubroCobro = (from r in ctx.RubroCobro
                                              where r.ID_RubroCobro == id
                                              select r).FirstOrDefault();

                    if (oRubroCobro.EstadoActual == "Activo")
                    {
                        oRubroCobro.EstadoActual = "Inactivo";
                    }
                    else
                    {
                        oRubroCobro.EstadoActual = "Activo";
                    }
                    ctx.Entry(oRubroCobro).State = System.Data.Entity.EntityState.Modified;
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

        public IEnumerable<RubroCobro> GetRubroCobro()
        {
            try
            {
                IEnumerable<RubroCobro> listaRubroCobro = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    
                    listaRubroCobro = ctx.RubroCobro.ToList();
                }
                return listaRubroCobro;
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

        public RubroCobro GetRubroCobroById(int id)
        {
            try
            {
                RubroCobro oRubroCobro = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    
                    oRubroCobro = ctx.RubroCobro.Find(id);
                }
                return oRubroCobro;
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

        public IEnumerable<RubroCobro> GetRubroCobrosActivo()
        {
            try
            {
                IEnumerable<RubroCobro> listaRubroCobro = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    listaRubroCobro = ctx.RubroCobro.Where(
                        r => r.EstadoActual == "Activo").ToList();
                }
                return listaRubroCobro;
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
