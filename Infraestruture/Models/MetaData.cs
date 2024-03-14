using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestruture.Models
{
    internal partial class AreaComunData
    {
        [Display(Name = "Nombre del area común")]
        public string Nombre { get; set; }
        public string HoraApertura { get; set; }
        public string HoraCierre { get; set; }
    }

    internal partial class ReservacionData
    {
        public string Dia { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFinal { get; set; }
    }

    internal partial class UsuarioMetaData
    {
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "{0} son datos requeridos")]
        public string Apellidos { get; set; }

        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} no tiene formato válido")]
        public string CorreoElectronico { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Contrasenna { get; set; }
    }

    internal partial class RubroCobroMetaData
    {
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Descripcion { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "{0} solo acepta números")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<double> Precio { get; set; }
    }

    internal partial class PlanCobroMetaData
    {
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Descripcion { get; set; }
    }

    internal partial class EstadoCuentaMetaData
    {
        [Required(ErrorMessage = "{0} No se puede editar este estado de cuenta")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        
        public Nullable<System.DateTime> ProximaFechaPago { get; set; }
    }

    internal partial class ResidenciaMetaData
    {
        [Display(Name = "Debe de poner CP antes del número de residencia")]
        [Required(ErrorMessage = "{0} es un dato requerido, debes de poner antes CP")]
        public string NumeroResidencia { get; set; }

        [Display(Name = "Cantidad de personas")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} solo acepta números")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<int> CantidadPersonas { get; set; }

        [Display(Name = "Año de inicio")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public Nullable<System.DateTime> AnnoInicio { get; set; }

        [Display(Name = "Cantidad de autos")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} solo acepta números")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<int> CantidadAutos { get; set; }
    }
    
    internal partial class IncidenciasMetaData
    {
        [Display(Name = "Título")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "{0} es un dato requerido")]

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Fecha de incidencia")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public Nullable<System.DateTime> FechaIncidencia { get; set; }
    }

    internal partial class InformacionMetaData
    {
        [Display(Name = "Título")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Titulo { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Descripcion { get; set; }

        [Display(Name = "Fecha de publicación")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public Nullable<System.DateTime> FechaPublicacion { get; set; }
    }
}
