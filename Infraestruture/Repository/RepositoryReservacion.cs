using AccesoADatos.ServicioEmail;
using Infraestructure.Utils;
using Infraestruture.Models;
using Infraestruture.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Infraestruture.Repository
{
    public class RepositoryReservacion : IRepositoryReservacion
    {
        public void Agregar(Reservacion pReservacion)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    ctx.Reservacion.Add(pReservacion);
                    ctx.SaveChanges();
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public void CambiarConfirmacion(int id)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Reservacion oReservacion = (from r in ctx.Reservacion
                                                where r.ID_Reservacion == id
                                                select r).FirstOrDefault();

                    if (oReservacion.Confirmacion == "Pendiente")
                    {
                        oReservacion.Confirmacion = "Aprobado";
                    }
                    else if (oReservacion.Confirmacion == "Aprobado")
                    {
                        oReservacion.Confirmacion = "Rechazado";
                    }
                    else if (oReservacion.Confirmacion == "Rechazado")
                    {
                        oReservacion.Confirmacion = "Aprobado";
                    }

                    ctx.Entry(oReservacion).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public void EnviarCorreo()
        {

            var outlook = new Outlook.Application();

            var mailItem = (Outlook.MailItem)outlook.CreateItem(Outlook.OlItemType.olMailItem);

            mailItem.Subject = "Correo de prueba";
            mailItem.Body = "Prueba";
            mailItem.To = "mquirosca@est.utn.ac.cr";

            mailItem.Send();

            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(mailItem);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(outlook);


            //try
            //{
            //    using (MyContext ctx = new MyContext())
            //    {
            //        ctx.Configuration.LazyLoadingEnabled = false;
            //        var servicioEmail = new AdministradorDelSistema();
            //        servicioEmail.EnviarArchivos(
            //            sujeto: "Reservacion nueva",
            //            emailReciente: new List<string> { "moisescr27@outlook.com"}
            //            );
            //    }
            //}
            //catch (DbUpdateException dbEx)
            //{
            //    string mensaje = "";
            //    Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
            //    throw new Exception(mensaje);
            //}
            //catch (Exception ex)
            //{
            //    string mensaje = "";
            //    Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
            //    throw;
            //}
        }

        //public void CambiarEstado(int id)
        //{
        //    try
        //    {
        //        using (MyContext ctx = new MyContext())
        //        {
        //            ctx.Configuration.LazyLoadingEnabled = false;
        //            Reservacion oReservacion = (from r in ctx.Reservacion
        //                                        where r.ID_Reservacion == id
        //                                        select r).FirstOrDefault();

        //            if (oReservacion.EstadoActual == "Activo")
        //            {
        //                oReservacion.EstadoActual = "Inactivo";
        //            }
        //            else
        //            {
        //                oReservacion.EstadoActual = "Activo";
        //            }

        //            ctx.Entry(oReservacion).State = System.Data.Entity.EntityState.Modified;
        //            ctx.SaveChanges();
        //        }
        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        string mensaje = "";
        //        Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
        //        throw new Exception(mensaje);
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje = "";
        //        Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
        //        throw;
        //    }
        //}

        //public Reservacion Editar(Reservacion pReservacion)
        //{
        //    int retorno = 0;
        //    Reservacion oReservacion = null;
        //    try
        //    {
        //        using (MyContext ctx = new MyContext())
        //        {
        //            ctx.Configuration.LazyLoadingEnabled = false;
        //            oReservacion = GetReservacionById(pReservacion.ID_Reservacion);

        //            ctx.Reservacion.Add(pReservacion);
        //            ctx.Entry(pReservacion).State = System.Data.Entity.EntityState.Modified;
        //            retorno = ctx.SaveChanges();
        //        }

        //        if (retorno >= 0)
        //        {
        //            oReservacion = GetReservacionById(pReservacion.ID_Reservacion);
        //        }
        //        return oReservacion;
        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        string mensaje = "";
        //        Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
        //        throw new Exception(mensaje);
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje = "";
        //        Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
        //        throw;
        //    }
        //}

        public List<AreaComun> GetAreaComun()
        {
            try
            {
                List<AreaComun> listaAreaComun = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    listaAreaComun = ctx.AreaComun.ToList();
                }
                return listaAreaComun;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<ReservacionUsuarioAreaViewModel> GetReservacion()
        {
            try
            {
                IEnumerable<ReservacionUsuarioAreaViewModel> listaReservacion = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    listaReservacion = (
                        (from u in ctx.Usuario
                         join r in ctx.Reservacion on u.ID_Usuario equals r.ID_Usuario
                         join a in ctx.AreaComun on r.ID_AreaComun equals a.ID_AreaComun

                         select new ReservacionUsuarioAreaViewModel
                         {
                             NombreCompleto = u.Nombre.ToString() + u.Apellidos.ToString(),
                             NombreAreaComun = a.Nombre.ToString(),
                             ID_Reservacion = r.ID_Reservacion.ToString(),
                             Confirmacion = r.Confirmacion.ToString(),
                             Dia = r.Dia.ToString(),
                             HoraInicio = r.HoraInicio.ToString(),
                             HoraFinal = r.HoraFinal.ToString()
                         }).ToList());
                }
                return listaReservacion;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }

        }

        public List<ReservacionUsuarioAreaViewModel> GetReservacionesById(int id)
        {
            try
            {
                List<ReservacionUsuarioAreaViewModel> listaReservacion = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    listaReservacion = (
                        (from u in ctx.Usuario
                         join r in ctx.Reservacion on u.ID_Usuario equals r.ID_Usuario
                         join a in ctx.AreaComun on r.ID_AreaComun equals a.ID_AreaComun
                         where r.ID_Usuario == id

                         select new ReservacionUsuarioAreaViewModel
                         {
                             NombreAreaComun = a.Nombre.ToString(),
                             ID_Reservacion = r.ID_Reservacion.ToString(),
                             Confirmacion = r.Confirmacion.ToString(),
                             Dia = r.Dia.ToString(),
                             HoraInicio = r.HoraInicio.ToString(),
                             HoraFinal = r.HoraFinal.ToString()
                         }).ToList());
                }
                return listaReservacion;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }

            //try
            //{
            //    List<Reservacion> listaReservaciones = null;
            //    using (MyContext ctx = new MyContext())
            //    {
            //        ctx.Configuration.LazyLoadingEnabled = false;
            //        listaReservaciones = (from r in ctx.Reservacion
            //                              join a in ctx.AreaComun on r.ID_AreaComun equals a.ID_AreaComun
            //                              where r.ID_Usuario == id
            //                              select r).ToList();
            //    }
            //    return listaReservaciones;
            //}
            //catch (DbUpdateException dbEx)
            //{
            //    string mensaje = "";
            //    Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
            //    throw new Exception(mensaje);
            //}
            //catch (Exception ex)
            //{
            //    string mensaje = "";
            //    Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
            //    throw;
            //}
        }

        public Usuario GetUsuarioById(int id)
        {
            try
            {
                Usuario oUsuario = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = ctx.Usuario.Find(id);
                }
                return oUsuario;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
    }
}
