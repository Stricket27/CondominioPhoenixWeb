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
    public class RepositoryEstadoCuenta : IRepositoryEstadoCuenta
    {
        public void Agregar(EstadoCuenta pEstadoCuenta)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    ctx.EstadoCuenta.Add(pEstadoCuenta);
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
                    EstadoCuenta oEstadoCuenta = (from e in ctx.EstadoCuenta
                                                  where e.ID_EstadoCuenta == id
                                                  select e).FirstOrDefault();

                    if (oEstadoCuenta.EstadoActual == "Activo")
                    {
                        oEstadoCuenta.EstadoActual = "Inactivo";
                    }
                    else
                    {
                        oEstadoCuenta.EstadoActual = "Activo";
                    }

                    ctx.Entry(oEstadoCuenta).State = System.Data.Entity.EntityState.Modified;
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

        public EstadoCuenta Editar(EstadoCuenta pEstadoCuenta)
        {
            int retorno = 0;
            EstadoCuenta oEstadoCuenta = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oEstadoCuenta = GetEstadoCuentaById(pEstadoCuenta.ID_EstadoCuenta);

                    ctx.EstadoCuenta.Add(pEstadoCuenta);
                    ctx.Entry(pEstadoCuenta).State = System.Data.Entity.EntityState.Modified;
                    retorno = ctx.SaveChanges();

                    if(retorno >= 0)
                    {
                        oEstadoCuenta = GetEstadoCuentaById(pEstadoCuenta.ID_EstadoCuenta);
                    }
                    return oEstadoCuenta;
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

        public IEnumerable<EstadoCuentaResidenciaPlanViewModel> GetEstadoCuenta()
        {
            try
            {
                IEnumerable<EstadoCuentaResidenciaPlanViewModel> listaEstadoCuentaRP = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    listaEstadoCuentaRP = (
                        (from r in ctx.Residencia
                        join e in ctx.EstadoCuenta on r.ID_Residencia equals e.ID_Residencia
                        join p in ctx.PlanCobro on e.ID_PlanCobro equals p.ID_PlanCobro

                        select new EstadoCuentaResidenciaPlanViewModel
                        {
                            ID_EstadoCuenta = e.ID_EstadoCuenta.ToString(),
                            EstadoPago = e.EstadoPago.ToString(),
                            ProximaFechaPago = e.ProximaFechaPago.ToString(),
                            EstadoActual = e.EstadoActual.ToString(),
                            NumeroResidencia = r.NumeroResidencia.ToString(),
                            DescripcionPlan = p.Descripcion.ToString()
                        }).ToList());
                }
                return listaEstadoCuentaRP;
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

        public IEnumerable<EstadoCuentaResidenciaPlanViewModel> GetEstadoCuentaByResidencia(string nombre)
        {
            IEnumerable<EstadoCuentaResidenciaPlanViewModel> oEstadoCuenta = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oEstadoCuenta = (
                        (from r in ctx.Residencia
                         join e in ctx.EstadoCuenta on r.ID_Residencia equals e.ID_Residencia
                         join p in ctx.PlanCobro on e.ID_PlanCobro equals p.ID_PlanCobro
                         where r.NumeroResidencia == nombre

                         select new EstadoCuentaResidenciaPlanViewModel
                         {
                             ID_EstadoCuenta = e.ID_EstadoCuenta.ToString(),
                             EstadoPago = e.EstadoPago.ToString(),
                             ProximaFechaPago = e.ProximaFechaPago.ToString(),
                             EstadoActual = e.EstadoActual.ToString(),
                             NumeroResidencia = r.NumeroResidencia.ToString(),
                             DescripcionPlan = p.Descripcion.ToString()
                         }).ToList());
            }
            return oEstadoCuenta;
        }

        public EstadoCuenta GetEstadoCuentaById(int id)
        {
            try
            {
                EstadoCuenta oEstadoCuenta = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oEstadoCuenta = ctx.EstadoCuenta.Find(id);
                }
                return oEstadoCuenta;
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


        public List<PlanCobro> GetPlanCobro()
        {
            try
            {
                List<PlanCobro> listaPlanCobro = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    listaPlanCobro = ctx.PlanCobro.ToList();
                }
                return listaPlanCobro;
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

        public List<Residencia> GetResidencia()
        {
            try
            {
                List<Residencia> listaResidencia = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    listaResidencia = ctx.Residencia.ToList();
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
    }
}
