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
    public class RubroCobroController : Controller
    {
        // GET: RubroCobro
        [CustomAuthorize((int)TipoUsuarioAsiganado.Administrador)]
        public ActionResult Index()
        {
            IEnumerable<RubroCobro> listaRubroCobro = null;
            try
            {
                IServicesRubroCobro _ServicesRubroCobro = new ServicesRubroCobro();
                listaRubroCobro = _ServicesRubroCobro.GetRubroCobro();
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
            return View(listaRubroCobro);
        }

        // GET: RubroCobro/Details/5
        public ActionResult Detalle(int id)
        {
            return View();
        }

        // GET: RubroCobro/Create
        public ActionResult Agregar()
        {
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            ViewBag.listaEstados = listaEstado;
            return View();
        }

        // POST: RubroCobro/Create
        [HttpPost]
        public ActionResult Agregar(RubroCobro pRubroCobro)
        {
            try
            {
                IServicesRubroCobro _ServicesRubroCobro = new ServicesRubroCobro();
                _ServicesRubroCobro.Agregar(pRubroCobro);
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

        // GET: RubroCobro/Edit/5
        public ActionResult Editar(int? id)
        {
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            listaEstado.Add(new SelectListItem() { Text = "Inactivo", Value = "Inactivo" });
            ViewBag.listaEstados = listaEstado;

            try
            {
                IServicesRubroCobro _ServicesRubroCobro = new ServicesRubroCobro();
                RubroCobro oRubroCobro = null;

                oRubroCobro = _ServicesRubroCobro.GetRubroCobroById(Convert.ToInt32(id));

                return View(oRubroCobro);
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

        // POST: RubroCobro/Edit/5
        [HttpPost]
        public ActionResult Editar(RubroCobro pRubroCobro)
        {
            try
            {
                IServicesRubroCobro _ServicesRubroCobro = new ServicesRubroCobro();

                RubroCobro oRubroCobro = _ServicesRubroCobro.Editar(pRubroCobro);
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

        // POST: RubroCobro/Delete/5
        
        public ActionResult CambiarEstado(int id)
        {
            try
            {
                RepositoryRubroCobro repositoryRubroCobro = new RepositoryRubroCobro();
                repositoryRubroCobro.CambiarEstado(id);
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
    }
}
