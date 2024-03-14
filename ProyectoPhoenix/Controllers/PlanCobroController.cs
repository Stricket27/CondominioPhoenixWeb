using ApplicationCore.Services;
using Infraestructure.Utils;
using Infraestruture.Models;
using Infraestruture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace ProyectoPhoenix.Controllers
{
    public class PlanCobroController : Controller
    {
        // GET: PlanCobro
        [CustomAuthorize((int)TipoUsuarioAsiganado.Administrador)]
        public ActionResult Index()
        {
            IEnumerable<PlanCobro> listaPlanCobro = null;
            try
            {
                IServicesPlanCobro _ServicesPlanCobro = new ServicesPlanCobro();
                listaPlanCobro = _ServicesPlanCobro.GetPlanCobro();
                return View(listaPlanCobro);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
            
        }

        // GET: PlanCobro/Details/5
        public ActionResult Detalle(int? id)
        {
            ServicesPlanCobro _ServicesPlanCobro = new ServicesPlanCobro();
            PlanCobro oPlanCobro = null;
            try
            {
                oPlanCobro = _ServicesPlanCobro.GetPlanCobroById(Convert.ToInt32(id));
                return View(oPlanCobro);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: PlanCobro/Create
        public ActionResult Agregar()
        {
            //ESTADO
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            ViewBag.listaEstado = listaEstado;

            ViewBag.ID_RubroCobro = listaRubro();

            return View();
        }

        // POST: PlanCobro/Create
        [HttpPost]
        public ActionResult Agregar(PlanCobro pPlanCobro, string[] selectRubroCobro)
        {
            try
            {
                IServicesPlanCobro _ServicesPlanCobro = new ServicesPlanCobro();
                PlanCobro oPlanCobro = _ServicesPlanCobro.Agregar(pPlanCobro, selectRubroCobro);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: PlanCobro/Edit/5
        public ActionResult Editar(int? id)
        {
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            listaEstado.Add(new SelectListItem() { Text = "Inactivo", Value = "Inactivo" });
            ViewBag.listaEstado = listaEstado;

            ServicesPlanCobro _ServicesPlanCobro = new ServicesPlanCobro();
            PlanCobro oPlanCobro = null;
            try
            {
                oPlanCobro = _ServicesPlanCobro.GetPlanCobroById(Convert.ToInt32(id));
                ViewBag.ID_RubroCobro = listaRubro(oPlanCobro.RubroCobro);
                return View(oPlanCobro);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // POST: PlanCobro/Edit/5
        [HttpPost]
        public ActionResult Editar(PlanCobro pPlanCobro, string[] selectRubroCobro)
        {
            IServicesPlanCobro _ServicesPlanCobro = new ServicesPlanCobro();
            try
            {
                PlanCobro oPlanCobro = _ServicesPlanCobro.Editar(pPlanCobro, selectRubroCobro);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // POST: PlanCobro/Delete/5
        public ActionResult CambiarEstado(int id)
        {
            try
            {
                RepositoryPlanCobro repositoryPlanCobro = new RepositoryPlanCobro();
                repositoryPlanCobro.CambiarEstado(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public MultiSelectList listaRubro(ICollection<RubroCobro> rubro = null)
        {
            IServicesRubroCobro _ServicesRubroCobro = new ServicesRubroCobro();
            IEnumerable<RubroCobro> listaRubroCobro = _ServicesRubroCobro.GetRubroCobrosActivo();

            int[] listaRubroSelect = null;
            if(rubro != null)
            {
                listaRubroSelect = rubro.Select(r => r.ID_RubroCobro).ToArray();

            }
            return new MultiSelectList(listaRubroCobro, "ID_RubroCobro", "Descripcion", listaRubroSelect);
        }

    }
}
