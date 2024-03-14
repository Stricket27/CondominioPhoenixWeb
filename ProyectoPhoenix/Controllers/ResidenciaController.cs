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
    public class ResidenciaController : Controller
    {
        // GET: Residencia
        [CustomAuthorize((int)TipoUsuarioAsiganado.Administrador)]
        public ActionResult Index()
        {
            try
            {
                IEnumerable<ResidenciaUsuarioViewModel> listaResidencia = null;
                IServicesResidencia _ServicesResidencia = new ServicesResidencia();
                listaResidencia = _ServicesResidencia.GetResidencia();
                return View(listaResidencia);
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

        // GET: Residencia/Details/5
        public ActionResult Detalle(int id)
        {
            try
            {
                IServicesResidencia _ServicesResidencia = new ServicesResidencia();
                Residencia oResidencia = null;

                oResidencia = _ServicesResidencia.GetResidenciaById(id);
                return View(oResidencia);
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

        // GET: Residencia/Create
        public ActionResult Agregar()
        {
            //ESTADO DE RESIDENCIA
            List<SelectListItem> listaEstadoResidencia = new List<SelectListItem>();
            listaEstadoResidencia.Add(new SelectListItem() { Text = "Construcción", Value = "Construcción" });
            listaEstadoResidencia.Add(new SelectListItem() { Text = "Habitada", Value = "Habitada" });
            ViewBag.listaEstadoResidencia = listaEstadoResidencia;
            //ESTADO
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            //listaEstado.Add(new SelectListItem() { Text = "Inactivo", Value = "Inactivo" });
            ViewBag.listaEstado = listaEstado;
            //USUARIO
            ViewBag.Usuario = GetUsuario().Select(x => new SelectListItem
            {
                Text = x.Nombre.ToString() + " " + x.Apellidos.ToString(),
                Value = x.ID_Usuario.ToString()
            }).ToList();
            return View();
        }

        // POST: Residencia/Create
        [HttpPost]
        public ActionResult Agregar(Residencia pResidencia)
        {
            try
            {
                IServicesResidencia _ServicesResidencia = new ServicesResidencia();
                _ServicesResidencia.Agregar(pResidencia);
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

        // GET: Residencia/Edit/5
        public ActionResult Editar(int? id)
        {
            //ESTADO DE RESIDENCIA
            List<SelectListItem> listaEstadoResidencia = new List<SelectListItem>();
            listaEstadoResidencia.Add(new SelectListItem() { Text = "Construcción", Value = "Construcción" });
            listaEstadoResidencia.Add(new SelectListItem() { Text = "Habitada", Value = "Habitada" });
            ViewBag.listaEstadoResidencia = listaEstadoResidencia;
            //ESTADO
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            listaEstado.Add(new SelectListItem() { Text = "Inactivo", Value = "Inactivo" });
            ViewBag.listaEstado = listaEstado;
            //USUARIO
            ViewBag.Usuario = GetUsuario().Select(x => new SelectListItem
            {
                Text = x.Nombre.ToString() + " " + x.Apellidos.ToString(),
                Value = x.ID_Usuario.ToString()
            }).ToList();

            ServicesResidencia _ServicesResidencia = new ServicesResidencia();
            Residencia oResidencia = null;
            oResidencia = _ServicesResidencia.GetResidenciaById(Convert.ToInt32(id));

            return View(oResidencia);
        }

        // POST: Residencia/Edit/5
        [HttpPost]
        public ActionResult Editar(Residencia pResidencia)
        {
            try
            {
                IServicesResidencia _ServicesResidencia = new ServicesResidencia();
                Residencia oResidencia = _ServicesResidencia.Editar(pResidencia);
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
        // POST: Residencia/Delete/5
        public ActionResult CambiarEstado(int id)
        {
            try
            {
                RepositoryResidencia repositoryResidencia= new RepositoryResidencia();
                repositoryResidencia.CambiarEstado(id);
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

        public List<Usuario> GetUsuario()
        {
            List<Usuario> listaUsuario = null;
            try
            {
                
                IServicesResidencia _ServicesResidencia = new ServicesResidencia();
                listaUsuario = _ServicesResidencia.GetUsuario();
                
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
            }
            return listaUsuario;
        }

    }
}
