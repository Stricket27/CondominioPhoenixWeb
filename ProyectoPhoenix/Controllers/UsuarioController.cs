using ApplicationCore.Services;
using Infraestructure.Utils;
using Infraestruture.Models;
using Infraestruture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ProyectoPhoenix.Utils;
using System.Web.Mvc;
using Web.Security;
using ProyectoPhoenix.Util;

namespace ProyectoPhoenix.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [CustomAuthorize((int)TipoUsuarioAsiganado.Administrador)]
        public ActionResult Index()
        {
            IEnumerable<Usuario> listaUsuario = null;
            try
            {
                IServicesUsuario _ServicesUsuario = new ServicesUsuario();
                listaUsuario = _ServicesUsuario.GetUsuario();
                if (TempData.ContainsKey("NotificationMessage"))
                {
                    ViewBag.NotificationMessage = TempData["NotificationMessage"];
                }
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
            return View(listaUsuario);
        }

        // GET: Usuario/Details/5
        public ActionResult Detalle(int? id)
        {
            ServicesUsuario _ServicesUsuario = new ServicesUsuario();
            Usuario oUsuario = null;
            try
            {
                oUsuario = _ServicesUsuario.GetUsuarioById(Convert.ToInt32(id));
                return View(oUsuario);
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

        // GET: Usuario/Create
        public ActionResult Agregar()
        {
            //ESTADO
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            //listaEstado.Add(new SelectListItem() { Text = "Inactivo", Value = "Inactivo" });
            ViewBag.listaEstado = listaEstado;
            //TIPO DE USUARIO
            ViewBag.TipoUsuario = TipoUsuario().Select(x => new SelectListItem
            {
                Text = x.TipoUsuario1.ToString(),
                Value = x.ID_TipoUsuario.ToString()
            }).ToList();
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Agregar(Usuario pUsuario)
        {
            try
            {
                IServicesUsuario _ServicesUsuario = new ServicesUsuario();

                if(_ServicesUsuario.GetUsuarioByCorreoElectronico(pUsuario.CorreoElectronico.ToLower()) != null)
                {
                    TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("ERROR", "No se puede agregar otro usuario con el mismo correo electrónico:" + " " + pUsuario.CorreoElectronico,
                        SweetAlertMessageType.error);
                }
                else
                {
                    _ServicesUsuario.Agregar(pUsuario);
                }
                
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

        // GET: Usuario/Edit/5
        public ActionResult Editar(int? id)
        {
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            listaEstado.Add(new SelectListItem() { Text = "Inactivo", Value = "Inactivo" });
            ViewBag.listaEstado = listaEstado;

            ViewBag.TipoUsuario = TipoUsuario().Select(x => new SelectListItem
            {
                Text = x.TipoUsuario1.ToString(),
                Value = x.ID_TipoUsuario.ToString()
            }).ToList();

            ServicesUsuario _ServicesUsuario = new ServicesUsuario();
            Usuario oUsuario = null;
            try
            {
                oUsuario = _ServicesUsuario.GetUsuarioById(Convert.ToInt32(id));
                return View(oUsuario);
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

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Editar(Usuario pUsuario)
        {
            IServicesUsuario _ServicesUsuario = new ServicesUsuario();
            try
            {
                Usuario oUsuario = _ServicesUsuario.Editar(pUsuario);
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

        // POST: Usuario/Delete/5
        
        public ActionResult CambiarEstado(int id)
        {
            try
            {
                RepositoryUsuario repositoryUsuario = new RepositoryUsuario();
                repositoryUsuario.CambiarEstado(id);
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

        public List<TipoUsuario> TipoUsuario()
        {
            List<TipoUsuario> listaTipoUsuario = null;
            try
            {
                IServicesUsuario _ServicesUsuario = new ServicesUsuario();
                listaTipoUsuario = _ServicesUsuario.GetTipoUsuarios();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
            }
            return listaTipoUsuario;
        }

        // POST: Usuario/LogIn/5
        public ActionResult IndexIniciarSesion()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IniciarSesion(Usuario pUsuario)
        {
            IServicesUsuario _ServicesUsuario = new ServicesUsuario();
            Usuario oUsuario = null;
            try
            {
                ModelState.Remove("Nombre");
                ModelState.Remove("Apellidos");
                ModelState.Remove("ID_TipoUsuario");

                if (ModelState.IsValid)
                {
                    oUsuario = _ServicesUsuario.IniciarSesion(pUsuario.CorreoElectronico, pUsuario.Contrasenna);
                    if (oUsuario != null)
                    {
                        Session["Usuario"] = oUsuario;
                        Session["Rol"] = oUsuario.ID_TipoUsuario;
                        Log.Info($"Inicio sesion: {pUsuario.CorreoElectronico}");
                        TempData["mensaje"] = Util.SweetAlertHelper.Mensaje("Bienvenido",
                            oUsuario.Nombre + " " + oUsuario.Apellidos, Util.SweetAlertMessageType.success
                            );
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Log.Warn($"Intento de inicio: {pUsuario.CorreoElectronico}");
                        ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("¿Quién eres?",
                            "Esta cuenta es inválida, intente de nuevo", Util.SweetAlertMessageType.error
                            );
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
            return View("IndexIniciarSesion");
        }
        public ActionResult SinAutorizacion()
        {
            ViewBag.Message = "No autorizado";
            if (Session["Usuario"] != null)
            {
                Usuario oUsuario = Session["Usuario"] as Usuario;
                Log.Warn($"No autorizado {oUsuario.CorreoElectronico}");
            }
            return View();
        }
        public ActionResult CerrarSesion()
        {
            try
            {
                Session["Usuario"] = null;
                Session.Remove("Usuario");

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

    }
}
