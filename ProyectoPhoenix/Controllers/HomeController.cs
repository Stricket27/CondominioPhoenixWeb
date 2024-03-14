using ApplicationCore.Services;
using Infraestructure.Utils;
using Infraestruture.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ProyectoPhoenix.Controllers
{
    public class HomeController : Controller
    {

        private ProyectoPhoenixEntities modelo { get; set; }

        public string Alertajs { get; set; }

        public void LlenarAlerta()
        {
            modelo = new ProyectoPhoenixEntities();

            List<Reservacion> reservacions;

            reservacions = (from r in modelo.Reservacion
                            where r.Confirmacion == "Pendiente"
                            select r).ToList();

            Alertajs = JsonConvert.SerializeObject(reservacions);

        }

        public ActionResult Index()
        {
            try
            {
                if (TempData.ContainsKey("mensaje"))
                {
                    ViewBag.NotificationMessage = TempData["mensaje"];
                }
                IEnumerable<Informacion> listaInformacion = null;
                IServicesInformacion _ServicesInformacion = new ServicesInformacion();
                listaInformacion = _ServicesInformacion.GetInformacion();
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}