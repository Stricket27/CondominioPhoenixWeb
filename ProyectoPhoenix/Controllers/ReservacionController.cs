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
    public class ReservacionController : Controller
    {
        // GET: Reservacion / ADMINISTRADOR
        [CustomAuthorize((int)TipoUsuarioAsiganado.Administrador)]
        public ActionResult Index()
        {
            try
            {
                IEnumerable<ReservacionUsuarioAreaViewModel> listaReservacion = null;
                IServicesReservacion _ServicesReservacion = new ServicesReservacion();
                listaReservacion = _ServicesReservacion.GetReservacion();
                return View(listaReservacion);
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

        public ActionResult VerEstadoReservacion()
        {
            List<ReservacionUsuarioAreaViewModel> listaReservacion = null;
            try
            {
                int idUsuario = 0;
                IServicesReservacion _ServicesReservacion = new ServicesReservacion();
                if (Session["Usuario"] != null)
                {
                    Infraestruture.Models.Usuario usuario = new Infraestruture.Models.Usuario();
                    usuario = (Infraestruture.Models.Usuario)Session["Usuario"];
                    idUsuario = usuario.ID_Usuario;
                }
                listaReservacion = _ServicesReservacion.GetReservacionesById(idUsuario);
                ViewBag.Reservaciones = listaReservacion;
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
            return View();

        }

        // GET: Reservacion/Details/5/ ADMINISTRADOR & RESIDENTE

        public ActionResult Detalle(int? id)
        {
            try
            {
                IServicesReservacion _ServicesReservacion = new ServicesReservacion();
                Reservacion oReservacion = null;
                //oReservacion = _ServicesReservacion.GetReservacionesById(id);
                return View(/*oReservacion*/);
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

        // GET: Reservacion/Create / RESIDENTE
        public ActionResult Agregar(Usuario pUsuario)
        {
            //ESTADO
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            ViewBag.listaEstado = listaEstado;


            //CONFIRMACION
            List<SelectListItem> listaConfirmacion = new List<SelectListItem>();
            listaConfirmacion.Add(new SelectListItem() { Text = "Pendiente", Value = "Pendiente" });
            ViewBag.listaConfirmacion = listaConfirmacion;


            //USUARIO YA INICIADO
            Usuario oUsuario = null;
            IServicesReservacion _ServicesReservacion = new ServicesReservacion();
            //IServicesUsuario _ServicesUsuario = new ServicesUsuario();
            try
            {
                oUsuario = _ServicesReservacion.GetUsuarioById(pUsuario.ID_Usuario);
                if (oUsuario != null)
                {
                    Session["Usuario"] = oUsuario;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }


            //AREA COMUN
            ViewBag.AreaComun = GetAreaComun().Select(x => new SelectListItem
            {
                Text = x.Nombre.ToString(),
                Value = x.ID_AreaComun.ToString()
            }).ToList();
            return View();
        }

        // POST: Reservacion/Create / RESIDENTE
        [HttpPost]
        public ActionResult Agregar(Reservacion pReservacion)
        {
            try
            {
                IServicesReservacion _ServicesReservacion = new ServicesReservacion();
                _ServicesReservacion.Agregar(pReservacion);
                _ServicesReservacion.EnviarCorreo();
                return RedirectToAction("Index", "Home");
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

        // GET: Reservacion/Edit/5 / NO SE TOCA
        public ActionResult Editar(int id)
        {
            return View();
        }

        // POST: Reservacion/Edit/5 / NO SE TOCA
        [HttpPost]
        public ActionResult Editar(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Reservacion/Cambiar estado/5 / ADMINISTRADOR
        //public ActionResult CambiarEstado(int id)
        //{
        //    try
        //    {
        //        RepositoryReservacion repositoryReservacion = new RepositoryReservacion();
        //        repositoryReservacion.CambiarEstado(id);
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Salvar el error en un archivo 
        //        Log.Error(ex, MethodBase.GetCurrentMethod());
        //        TempData["Message"] = "Error al procesar los datos! " + ex.Message;
        //        TempData["Redirect"] = "Libro";
        //        TempData["Redirect-Action"] = "IndexAdmin";
        //        // Redireccion a la captura del Error
        //        return RedirectToAction("Default", "Error");
        //    }
        //}
        // POST: Reservacion/Cambiar confirmacion/5 / ADMINISTRADOR
        public ActionResult CambiarConfirmacion(int id)
        {
            try
            {
                RepositoryReservacion repositoryReservacion = new RepositoryReservacion();
                repositoryReservacion.CambiarConfirmacion(id);
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
        public List<AreaComun> GetAreaComun()
        {
            List<AreaComun> listaAreaComun = null;
            try
            {
                IServicesReservacion _ServicesReservacion = new ServicesReservacion();
                listaAreaComun = _ServicesReservacion.GetAreaComun();

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
            }
            return listaAreaComun;
        }

    }
}
