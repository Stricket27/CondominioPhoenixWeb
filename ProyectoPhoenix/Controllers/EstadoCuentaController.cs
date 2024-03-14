using ApplicationCore.Services;
using Infraestructure.Utils;
using Infraestruture.Models;
using Infraestruture.Models.ViewModel;
using Infraestruture.Repository;
using log4net.Config;
using ProyectoPhoenix.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using Web.Security;

namespace ProyectoPhoenix.Controllers
{
    public class EstadoCuentaController : Controller
    {

        // GET: EstadoCuenta
        [CustomAuthorize((int)TipoUsuarioAsiganado.Administrador)]
        public ActionResult Index()
        {
            try
            {
                IEnumerable<EstadoCuentaResidenciaPlanViewModel> listaEstadoCuenta = null;
                IServicesEstadoCuenta _ServicesEstadoCuenta = new ServicesEstadoCuenta();
                listaEstadoCuenta = _ServicesEstadoCuenta.GetEstadoCuenta();

                if (TempData.ContainsKey("NotificationMessage"))
                {
                    ViewBag.NotificationMessage = TempData["NotificationMessage"];
                }

                return View(listaEstadoCuenta);
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

        // GET: EstadoCuenta/Details/5
        public ActionResult Detalle(int? id)
        {
            try
            {
                IServicesEstadoCuenta _ServicesEstadoCuenta = new ServicesEstadoCuenta();
                EstadoCuenta oEstadoCuenta = null;

                oEstadoCuenta = _ServicesEstadoCuenta.GetEstadoCuentaById(Convert.ToInt32(id));
                return View(oEstadoCuenta);
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

        // GET: EstadoCuenta/Create
        public ActionResult Agregar()
        {
            //ESTADO DE PAGO
            List<SelectListItem> listaEstadoPago = new List<SelectListItem>();
            listaEstadoPago.Add(new SelectListItem() { Text = "Pendiente", Value = "Pendiente" });
            //listaEstado.Add(new SelectListItem() { Text = "Finalizado", Value = "Finalizado" });
            ViewBag.listaEstadoPago = listaEstadoPago;
            //ESTADO
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            //listaEstado.Add(new SelectListItem() { Text = "Inactivo", Value = "Inactivo" });
            ViewBag.listaEstado = listaEstado;
            //RESIDENCIA
            ViewBag.Residencia = GetResidencias().Select(x => new SelectListItem
            {
                Text = x.NumeroResidencia.ToString(),
                Value = x.ID_Residencia.ToString()
            }).ToList();
            //PLAN
            ViewBag.Plan = GetPlanCobro().Select(x => new SelectListItem
            {
                Text = x.Descripcion.ToString(),
                Value = x.ID_PlanCobro.ToString()
            }).ToList();
            return View();
        }

        // POST: EstadoCuenta/Create
        [HttpPost]
        public ActionResult Agregar(EstadoCuenta pEstadoCuenta)
        {
            try
            {
                IServicesEstadoCuenta _ServicesCuenta = new ServicesEstadoCuenta();

                _ServicesCuenta.Agregar(pEstadoCuenta);
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

        // GET: EstadoCuenta/Edit/5
        public ActionResult Editar(int? id)
        {
            //ESTADO DE PAGO
            List<SelectListItem> listaEstadoPago = new List<SelectListItem>();
            listaEstadoPago.Add(new SelectListItem() { Text = "Pendiente", Value = "Pendiente" });
            listaEstadoPago.Add(new SelectListItem() { Text = "Finalizado", Value = "Finalizado" });
            ViewBag.listaEstadoPago = listaEstadoPago;
            //ESTADO
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            listaEstado.Add(new SelectListItem() { Text = "Inactivo", Value = "Inactivo" });
            ViewBag.listaEstado = listaEstado;
            //RESIDENCIA
            ViewBag.Residencia = GetResidencias().Select(x => new SelectListItem
            {
                Text = x.NumeroResidencia.ToString(),
                Value = x.ID_Residencia.ToString()
            }).ToList();
            //PLAN
            ViewBag.Plan = GetPlanCobro().Select(x => new SelectListItem
            {
                Text = x.Descripcion.ToString(),
                Value = x.ID_PlanCobro.ToString()
            }).ToList();

            ServicesEstadoCuenta _ServicesEstadoCuenta = new ServicesEstadoCuenta();
            EstadoCuenta oEstadoCuenta = null;
            oEstadoCuenta = _ServicesEstadoCuenta.GetEstadoCuentaById(Convert.ToInt32(id));

            return View(oEstadoCuenta);
        }

        // POST: EstadoCuenta/Edit/5
        [HttpPost]
        public ActionResult Editar(EstadoCuenta pEstadoCuenta)
        {
            try
            {
                IServicesEstadoCuenta _ServicesEstadoCuenta = new ServicesEstadoCuenta();
                if (pEstadoCuenta.ProximaFechaPago > DateTime.Now)
                {
                    TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("No se puede editar", "se puede editar hasta la proxima fecha", 
                        SweetAlertMessageType.error);
                }
                else
                {
                    
                    EstadoCuenta oEstadoCuenta = _ServicesEstadoCuenta.Editar(pEstadoCuenta);
                }

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

        // POST: EstadoCuenta/Delete/5
        public ActionResult CambiarEstado(int id)
        {
            try
            {
                RepositoryEstadoCuenta repositoryEstadoCuenta = new RepositoryEstadoCuenta();
                repositoryEstadoCuenta.CambiarEstado(id);
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

        public List<Residencia> GetResidencias()
        {
            List<Residencia> listaResidencia = null;
            try
            {
                IServicesEstadoCuenta _ServicesEstadoCuenta = new ServicesEstadoCuenta();
                listaResidencia = _ServicesEstadoCuenta.GetResidencia();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
            }
            return listaResidencia;
        }

        public List<PlanCobro> GetPlanCobro()
        {
            List<PlanCobro> listaPlanCobro = null;
            try
            {
                IServicesEstadoCuenta _ServicesEstadoCuenta = new ServicesEstadoCuenta();
                listaPlanCobro = _ServicesEstadoCuenta.GetPlanCobro();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
            }
            return listaPlanCobro;
        }

        public ActionResult buscarEstadoCuentaxFecha(string nombre)
        {
            IEnumerable<EstadoCuentaResidenciaPlanViewModel> lista = null;
            IServicesEstadoCuenta servicesEstadoCuenta = new ServicesEstadoCuenta();

            if (String.IsNullOrEmpty(nombre))
            {
                lista = servicesEstadoCuenta.GetEstadoCuenta();
            }
            else
            {
                lista = servicesEstadoCuenta.GetEstadoCuentaByResidencia(nombre);
            }
            return View("Index", lista);
        }
    }
}
