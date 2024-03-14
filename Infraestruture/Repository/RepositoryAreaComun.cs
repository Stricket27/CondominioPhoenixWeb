using Infraestructure.Utils;
using Infraestruture.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infraestruture.Repository
{
    public class RepositoryAreaComun : IRepositoryAreaComun
    {
        public void Agregar(AreaComun pAreaComun)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    ctx.AreaComun.Add(pAreaComun);
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
                    AreaComun oAreaComun = (from a in ctx.AreaComun
                                            where a.ID_AreaComun == id
                                            select a).FirstOrDefault();

                    if (oAreaComun.EstadoActual == "Activo")
                    {
                        oAreaComun.EstadoActual = "Inactivo";
                    }
                    else
                    {
                        oAreaComun.EstadoActual = "Activo";
                    }

                    ctx.Entry(oAreaComun).State = System.Data.Entity.EntityState.Modified;
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

        public AreaComun Editar(AreaComun pAreaComun)
        {
            int retorno = 0;
            AreaComun oAreaComun = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oAreaComun = GetAreaComunById(pAreaComun.ID_AreaComun);

                    ctx.AreaComun.Add(pAreaComun);
                    ctx.Entry(pAreaComun).State = System.Data.Entity.EntityState.Modified;
                    retorno = ctx.SaveChanges();

                    if (retorno >= 0)
                    {
                        oAreaComun = GetAreaComunById(pAreaComun.ID_AreaComun);
                    }
                    return oAreaComun;
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

        public IEnumerable<AreaComun> GetAreaComun()
        {
            try
            {
                List<AreaComun> listaAreaComun = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    listaAreaComun = ctx.AreaComun.ToList();
                }
                return listaAreaComun;
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

        public AreaComun GetAreaComunById(int id)
        {
            try
            {
                AreaComun oAreaComun = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oAreaComun = ctx.AreaComun.Find(id);
                }
                return oAreaComun;
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
