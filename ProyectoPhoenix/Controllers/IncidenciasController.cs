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
using Web.Security;

namespace ProyectoPhoenix.Controllers
{
    public class IncidenciasController : Controller
    {
        // GET: Incidencias
        [CustomAuthorize((int)TipoUsuarioAsiganado.Administrador)]
        public ActionResult Index()
        {
            try
            {
                IEnumerable<IncidenciaUsuarioViewModel> listaIncidencias = null;
                IServicesIncidencias _ServicesIncidencias = new ServicesIncidencias();
                listaIncidencias = _ServicesIncidencias.GetIncidencias();
                return View(listaIncidencias);
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

        // GET: Incidencias/Details/5
        public ActionResult Detalle(int? id)
        {
            IServicesIncidencias _ServicesIncidencias = new ServicesIncidencias();
            Incidencias oIncidencias = null;

            try
            {
                oIncidencias = _ServicesIncidencias.GetIncidenciasById(Convert.ToInt32(id));
                return View(oIncidencias);
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

        // GET: Incidencias/Create
        public ActionResult Agregar()
        {
            //ESTADO DE SOLICITUD
            List<SelectListItem> listaEstadoSolicitud = new List<SelectListItem>();
            listaEstadoSolicitud.Add(new SelectListItem() { Text = "En proceso", Value = "Proceso" });
            //listaEstado.Add(new SelectListItem() { Text = "Finalizado", Value = "Finalizado" });
            ViewBag.listaEstadoSolicitud = listaEstadoSolicitud;
            //ESTADO
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            //listaEstado.Add(new SelectListItem() { Text = "Inactivo", Value = "Inactivo" });
            ViewBag.listaEstado = listaEstado;
            //USUARIO
            ViewBag.Usuario = GetUsuarios().Select(x => new SelectListItem
            {
                Text = x.Nombre.ToString() + " " + x.Apellidos.ToString(),
                Value = x.ID_Usuario.ToString()
            }).ToList();
            return View();
        }

        // POST: Incidencias/Create
        [HttpPost]
        public ActionResult Agregar(Incidencias pIncidencias)
        {
            try
            {
                IServicesIncidencias _ServicesIncidencias = new ServicesIncidencias();
                _ServicesIncidencias.Agregar(pIncidencias);
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

        // GET: Incidencias/Edit/5
        public ActionResult Editar(int? id)
        {
            //ESTADO DE SOLICITUD
            List<SelectListItem> listaEstadoSolicitud = new List<SelectListItem>();
            listaEstadoSolicitud.Add(new SelectListItem() { Text = "En proceso", Value = "Proceso" });
            listaEstadoSolicitud.Add(new SelectListItem() { Text = "Finalizado", Value = "Finalizado" });
            ViewBag.listaEstadoSolicitud = listaEstadoSolicitud;
            //ESTADO
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            listaEstado.Add(new SelectListItem() { Text = "Inactivo", Value = "Inactivo" });
            ViewBag.listaEstado = listaEstado;
            //USUARIO
            ViewBag.Usuario = GetUsuarios().Select(x => new SelectListItem
            {
                Text = x.Nombre.ToString() + " " + x.Apellidos.ToString(),
                Value = x.ID_Usuario.ToString()
            }).ToList();

            ServicesIncidencias _ServicesIncidencias = new ServicesIncidencias();
            Incidencias oIncidencias = null;
            oIncidencias = _ServicesIncidencias.GetIncidenciasById(Convert.ToInt32(id));

            return View(oIncidencias);
        }

        // POST: Incidencias/Edit/5
        [HttpPost]
        public ActionResult Editar(Incidencias pIncidencias)
        {
            try
            {
                IServicesIncidencias _ServicesIncidencias = new ServicesIncidencias();
                Incidencias oIncidencias = _ServicesIncidencias.Editar(pIncidencias);
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

        // POST: Incidencias/Delete/5
        public ActionResult CambiarEstado(int id)
        {
            try
            {
                RepositoryIncidencias repositoryIncidencias = new RepositoryIncidencias();
                repositoryIncidencias.CambiarEstado(id);
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

        public List<Usuario> GetUsuarios()
        {
            List<Usuario> listaUsuario = null;
            try
            {
                IServicesIncidencias _ServicesIncidencias = new ServicesIncidencias();
                listaUsuario = _ServicesIncidencias.GetUsuario();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
            }
            return listaUsuario;
        }
    }
}
