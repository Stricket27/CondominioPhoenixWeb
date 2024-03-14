using ApplicationCore.Services;
using Infraestructure.Utils;
using Infraestruture.Models;
using Infraestruture.Models.ViewModel;
using Infraestruture.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Web.Security;

namespace ProyectoPhoenix.Controllers
{
    public class InformacionController : Controller
    {

        // GET: Informacion
        [CustomAuthorize((int)TipoUsuarioAsiganado.Administrador)]
        public ActionResult Index()
        {
            try
            {
                IEnumerable<Informacion> listaInformacion = null;
                IServicesInformacion _ServicesInformacion = new ServicesInformacion();
                listaInformacion = _ServicesInformacion.GetInformacion();
                if (TempData.ContainsKey("mensaje"))
                {
                    ViewBag.NotificationMessage = TempData["mensaje"];
                }
                return View(listaInformacion);
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

        // GET: Informacion/Details/5
        public ActionResult Detalle(int? id)
        {
            try
            {
                ServicesInformacion _ServicesInformacion = new ServicesInformacion();
                Informacion oInformacion = null;

                oInformacion = _ServicesInformacion.GetInformacionById(Convert.ToInt32(id));

                return View(oInformacion);
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

        // GET: Informacion/Create
        public ActionResult Agregar()
        {
            //ESTADO
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            //listaEstado.Add(new SelectListItem() { Text = "Inactivo", Value = "Inactivo" });
            ViewBag.listaEstado = listaEstado;
            //TIPO INFORMACION
            List<SelectListItem> tipoInformacion = new List<SelectListItem>();
            tipoInformacion.Add(new SelectListItem() { Text = "Noticias", Value = "Noticias" });
            tipoInformacion.Add(new SelectListItem() { Text = "Avisos", Value = "Avisos" });
            tipoInformacion.Add(new SelectListItem() { Text = "Archivo documental: Reglamento", Value = "ArchivoDocumentalReglamento" });
            tipoInformacion.Add(new SelectListItem() { Text = "Archivo documental: Acta", Value = "ArchivoDocumentalActas" });
            tipoInformacion.Add(new SelectListItem() { Text = "Archivo documental: Estado financiero", Value = "ArchivoDocumentalEstadoFinanciero" });
            ViewBag.listaTipoInformacion = tipoInformacion;
            return View();
        }

        // POST: Informacion/Create
        [HttpPost]
        public ActionResult Agregar(HttpPostedFileBase File, Informacion pInformacion)
        {
            MemoryStream target = new MemoryStream();
            IServicesInformacion _ServicesInformacion = new ServicesInformacion();
            try
            {
                //ARCHIVO


                if (pInformacion.Archivo == null)
                {
                    pInformacion.TipoArchivo = "No posee un archivo";
                    if (File != null)
                    {
                        string tipoArchivo = Path.GetExtension(File.FileName);
                        pInformacion.TipoArchivo = tipoArchivo;
                        File.InputStream.CopyTo(target);
                        pInformacion.Archivo = target.ToArray();
                        ModelState.Remove("Archivo");
                    }
                }
                _ServicesInformacion.Agregar(pInformacion);
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

        // GET: Informacion/Edit/5
        public ActionResult Editar(int? id)
        {
            //ESTADO
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            listaEstado.Add(new SelectListItem() { Text = "Inactivo", Value = "Inactivo" });
            ViewBag.listaEstado = listaEstado;
            //TIPO DE INFORMACION
            List<SelectListItem> tipoInformacion = new List<SelectListItem>();
            tipoInformacion.Add(new SelectListItem() { Text = "Noticias", Value = "Noticias" });
            tipoInformacion.Add(new SelectListItem() { Text = "Avisos", Value = "Avisos" });
            tipoInformacion.Add(new SelectListItem() { Text = "Archivo documental: Reglamento", Value = "ArchivoDocumentalReglamento" });
            tipoInformacion.Add(new SelectListItem() { Text = "Archivo documental: Acta", Value = "ArchivoDocumentalActas" });
            tipoInformacion.Add(new SelectListItem() { Text = "Archivo documental: Estado financiero", Value = "ArchivoDocumentalEstadoFinanciero" });
            ViewBag.listaTipoInformacion = tipoInformacion;


            ServicesInformacion _ServicesInformacion = new ServicesInformacion();
            Informacion oInformacion = null;
            try
            {
                oInformacion = _ServicesInformacion.GetInformacionById(Convert.ToInt32(id));
                return View(oInformacion);
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

        // POST: Informacion/Edit/5
        [HttpPost]
        public ActionResult Editar(Informacion pInformacion, HttpPostedFileBase File)
        {
            MemoryStream target = new MemoryStream();
            IServicesInformacion _ServicesInformacion = new ServicesInformacion();
            try
            {
                if (pInformacion.Archivo == null)
                {
                    if (File != null)
                    {
                        File.InputStream.CopyTo(target);
                        pInformacion.Archivo = target.ToArray();
                        ModelState.Remove("Archivo");
                    }
                }

                Informacion oInformacion = _ServicesInformacion.Editar(pInformacion);
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
        // POST: Informacion/Delete/5
        public ActionResult CambiarEstado(int id, FormCollection collection)
        {
            try
            {
                RepositoryInformacion repositoryInformacion = new RepositoryInformacion();
                repositoryInformacion.CambiarEstado(id);
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
