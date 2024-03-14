using ApplicationCore.Services;
using Infraestructure.Utils;
using Infraestruture.Models;
using Infraestruture.Models.ViewModel;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ProyectoPhoenix.Controllers
{
    public class ReportesController : Controller
    {

        //private readonly IServicesPlanCobro _servicesPlan;

        //public ReportesController(IServicesPlanCobro servicesPlan)
        //{
        //    _servicesPlan = servicesPlan;
        //}

        // GET: Reportes
        public ActionResult Index()
        {
            return View("EstadoCuenta");
        }

        public ActionResult EstadoCuentaReporte()
        {
            IEnumerable <EstadoCuentaResidenciaPlanViewModel> lista = null;
            try
            {
                IServicesEstadoCuenta _ServicesEstadoCuenta = new ServicesEstadoCuenta();
                lista = _ServicesEstadoCuenta.GetEstadoCuenta();
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
            return View(lista);
        }

        public ActionResult CrearPDFEstadoCuenta()
        {
            IEnumerable<EstadoCuentaResidenciaPlanViewModel> lista = null;
            try
            {
                IServicesEstadoCuenta _ServicesEstadoCuenta = new ServicesEstadoCuenta();
                lista = _ServicesEstadoCuenta.GetEstadoCuenta();

                MemoryStream memory = new MemoryStream();
                PdfWriter pdfWriter = new PdfWriter(memory);

                PdfDocument pdfDocument = new PdfDocument(pdfWriter);
                Document document = new Document(pdfDocument);

                Paragraph paragraph = new Paragraph("Estado de cuenta con residencia y planes")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.COURIER))
                    .SetFontSize(14)
                    .SetFontColor(ColorConstants.BLUE);

                document.Add(paragraph);

                Table table = new Table(5, true);

                table.AddHeaderCell("Número de residencia");
                table.AddHeaderCell("Nombre del plan");
                table.AddHeaderCell("Proxima fecha a pagar");
                table.AddHeaderCell("Estado de pago");
                table.AddHeaderCell("Estado actual");

                foreach (var item in lista)
                {
                    table.AddCell(new Paragraph(item.NumeroResidencia));
                    table.AddCell(new Paragraph(item.DescripcionPlan));
                    table.AddCell(new Paragraph(item.ProximaFechaPago));
                    table.AddCell(new Paragraph(item.EstadoPago));
                    table.AddCell(new Paragraph(item.EstadoActual));
                }
                document.Add(table);

                int numeroPaginas = pdfDocument.GetNumberOfPages();
                for (int i = 1; i<= numeroPaginas; i ++)
                {
                    document.ShowTextAligned(
                        new Paragraph(
                            String.Format("Página {0} de {1}", 1, numeroPaginas)
                            ),
                        559, 826, i, iText.Layout.Properties.TextAlignment.RIGHT, iText.Layout.Properties.VerticalAlignment.TOP, 0);
                }

                document.Close();
                return File(memory.ToArray(), "application/pdf", "reporte.pdf");
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

        public ActionResult GraficoPlanCobro()
        {
            GraficoViewModel grafico = new GraficoViewModel();
            IServicesPlanCobro servicesPlan = new ServicesPlanCobro();
            servicesPlan.GetPlanCobroOutCobro(out string etiquetas, out string valores);
            grafico.Etiquetas = etiquetas;
            grafico.Valores = valores;
            int cantidadValores = valores.Split(',').Length;
            grafico.Colores = string.Join(",", grafico.GenerateColors(cantidadValores));
            grafico.titulo = "Plan por cobro";
            grafico.tituloEtiquetas = "Planes con sus cobros";
            //Tipos de graficos: bar, bubble, doughnut, pie, line, polarArea
            grafico.tipo = "doughnut";
            ViewBag.grafico = grafico;
            return View();
        }
    }
}
