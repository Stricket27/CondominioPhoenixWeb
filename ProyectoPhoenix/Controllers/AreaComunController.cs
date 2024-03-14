using ApplicationCore.Services;
using Infraestructure.Utils;
using Infraestruture.Models;
using Infraestruture.Models.ViewModel;
using Infraestruture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Web.Security;

namespace ProyectoPhoenix.Controllers
{
    public class AreaComunController : Controller
    {
        // GET: AreaComun
        [CustomAuthorize((int)TipoUsuarioAsiganado.Administrador)]
        public ActionResult Index()
        {
            try
            {
                IEnumerable<AreaComun> listaAreaComun = null;
                IServicesAreaComun _ServicesAreaComun = new ServicesAreaComun();
                listaAreaComun = _ServicesAreaComun.GetAreaComun();
                return View(listaAreaComun);
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

        // GET: AreaComun/Details/5
        public ActionResult Detalle(int? id)
        {
            ServicesAreaComun _ServicesAreaComun = new ServicesAreaComun();
            AreaComun oAreaComun = null;
            try
            {
                oAreaComun = _ServicesAreaComun.GetAreaComunById(Convert.ToInt32(id));
                return View(oAreaComun);
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

        // GET: AreaComun/Create
        public ActionResult Agregar()
        {
            //ESTADO
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            //listaEstado.Add(new SelectListItem() { Text = "Inactivo", Value = "Inactivo" });
            ViewBag.listaEstado = listaEstado;
            return View();
        }

        // POST: AreaComun/Create
        [HttpPost]
        public ActionResult Agregar(AreaComun pAreaComun)
        {
            try
            {
                IServicesAreaComun _ServicesAreaComun = new ServicesAreaComun();
                _ServicesAreaComun.Agregar(pAreaComun);

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
            return RedirectToAction("Index");
        }

        // GET: AreaComun/Edit/5
        public ActionResult Editar(int? id)
        {
            //ESTADO
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            listaEstado.Add(new SelectListItem() { Text = "Inactivo", Value = "Inactivo" });
            ViewBag.listaEstado = listaEstado;
            ServicesAreaComun _ServicesAreaComun = new ServicesAreaComun();
            AreaComun oAreaComun = null;
            try
            {
                oAreaComun = _ServicesAreaComun.GetAreaComunById(Convert.ToInt32(id));
                return View(oAreaComun);
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

        // POST: AreaComun/Edit/5
        [HttpPost]
        public ActionResult Editar(AreaComun pAreaComun)
        {
            IServicesAreaComun _ServicesAreaComun = new ServicesAreaComun();
            try
            {
                AreaComun oAreaComun = _ServicesAreaComun.Editar(pAreaComun);
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

        // POST: AreaComun/Delete/5
        public ActionResult CambiarEstado(int id, FormCollection collection)
        {
            try
            {
                RepositoryAreaComun repositoryAreaComun = new RepositoryAreaComun();
                repositoryAreaComun.CambiarEstado(id);
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
