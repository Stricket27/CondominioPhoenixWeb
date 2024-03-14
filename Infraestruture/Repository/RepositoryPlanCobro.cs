using Infraestructure.Utils;
using Infraestruture.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infraestruture.Repository
{
    public class RepositoryPlanCobro : IRepositoryPlanCobro
    {
        public PlanCobro Agregar(PlanCobro pPlanCobro, string[] seleccionRubroCobro)
        {
            int retorno = 0;
            PlanCobro oPlanCobro = null;
            decimal? totalPrecioRubro = 0;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oPlanCobro = GetPlanCobroById(pPlanCobro.ID_PlanCobro);
                    IRepositoryRubroCobro repositoryRubroCobro = new RepositoryRubroCobro();

                    if(oPlanCobro == null)
                    {
                        if (seleccionRubroCobro != null)
                        {
                            pPlanCobro.RubroCobro = new List<RubroCobro>();
                            foreach (var rubro in seleccionRubroCobro)
                            {
                                var rubroToAdd = repositoryRubroCobro.GetRubroCobroById(int.Parse(rubro));

                                ctx.RubroCobro.Attach(rubroToAdd);

                                pPlanCobro.RubroCobro.Add(rubroToAdd);

                                totalPrecioRubro += rubroToAdd.Precio;
                            }
                            pPlanCobro.TotalPrecioRubro = (decimal?)totalPrecioRubro;
                        }
                    }
                    ctx.PlanCobro.Add(pPlanCobro);
                    retorno = ctx.SaveChanges();
                    if(retorno >= 0)
                    {
                        oPlanCobro = GetPlanCobroById(pPlanCobro.ID_PlanCobro);
                    }
                    return oPlanCobro;
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

        public PlanCobro Editar(PlanCobro pPlanCobro, string[] seleccionRubroCobro)
        {
            int retorno = 0;
            PlanCobro oPlanCobro = null;
            decimal? totalPrecioRubro = 0;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oPlanCobro = GetPlanCobroById(pPlanCobro.ID_PlanCobro);

                    ctx.PlanCobro.Add(pPlanCobro);
                    ctx.Entry(pPlanCobro).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();

                    var selectRubroCobroID = new HashSet<string>(seleccionRubroCobro);

                    if(seleccionRubroCobro != null)
                    {
                        ctx.Entry(pPlanCobro).Collection(r => r.RubroCobro).Load();

                        var newRubroForPlan = ctx.RubroCobro.
                            Where(x => selectRubroCobroID.Contains(x.ID_RubroCobro.ToString())).ToList();

                        foreach(var rubro in newRubroForPlan)
                        {
                            totalPrecioRubro += rubro.Precio;
                        }

                        pPlanCobro.TotalPrecioRubro = (decimal?)totalPrecioRubro;
                        pPlanCobro.RubroCobro = newRubroForPlan;

                        ctx.Entry(pPlanCobro).State = EntityState.Modified;
                        ctx.SaveChanges();
                    }
                }

                if(retorno >= 0)
                {
                    oPlanCobro = GetPlanCobroById(pPlanCobro.ID_PlanCobro);
                }
                return oPlanCobro;
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
                    PlanCobro oPlanCobro = (from p in ctx.PlanCobro
                                            where p.ID_PlanCobro == id
                                            select p).FirstOrDefault();

                    if(oPlanCobro.EstadoActual == "Activo")
                    {
                        oPlanCobro.EstadoActual = "Inactivo";
                    }
                    else
                    {
                        oPlanCobro.EstadoActual = "Activo";
                    }
                    ctx.Entry(oPlanCobro).State = System.Data.Entity.EntityState.Modified;
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

        public IEnumerable<PlanCobro> GetPlanCobro()
        {
            try
            {
                IEnumerable<PlanCobro> listaPlanCobro = null;
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

        public PlanCobro GetPlanCobroById(int id)
        {
            try
            {
                PlanCobro oPlanCobro = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oPlanCobro = ctx.PlanCobro.
                        Where(p => p.ID_PlanCobro == id).
                        Include("RubroCobro").
                        FirstOrDefault();
                }
                return oPlanCobro;
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

        public IEnumerable<PlanCobro> GetPlanCobroByRubroCobro(int IDRubroCobro)
        {
            try
            {
                IEnumerable<PlanCobro> listaPlanCobro = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    listaPlanCobro = ctx.PlanCobro.Include(r => r.RubroCobro).
                        Where(r => r.RubroCobro.Any(o => o.ID_RubroCobro == IDRubroCobro)).
                        ToList();
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

        public void GetPlanCobroOutCobro(out string etiquetas, out string valores)
        {
            String varEtiquetas = "";
            String varValores = "";
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    var lista = ctx.PlanCobro.ToList();

                    //lista = (from p in ctx.PlanCobro

                    //         select new PlanCobro
                    //         {
                    //             Descripcion = p.Descripcion,
                    //             TotalPrecioRubro = p.TotalPrecioRubro
                    //         }).ToList();

                    var resultado = ctx.PlanCobro.GroupBy(x => x.TotalPrecioRubro).
                    Select(
                    o => new
                    {
                        Count = o.Count(),
                        TotalPrecioRubro = o.Key
                    });

                    //Crear etiquetas y valores
                    foreach (var item in lista)
                    {
                        varEtiquetas += String.Format(/*"{0:dd/MM/yyyy}", */item.Descripcion) + ",";
                        varValores += item.TotalPrecioRubro + ",";
                    }
                }
                //Ultima coma
                varEtiquetas = varEtiquetas.Substring(0, varEtiquetas.Length - 1); // ultima coma
                varValores = varValores.Substring(0, varValores.Length - 1);
                //Asignar valores de salida
                etiquetas = varEtiquetas;
                valores = varValores;
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
                throw new Exception(mensaje);
            }
        }
    }
}
