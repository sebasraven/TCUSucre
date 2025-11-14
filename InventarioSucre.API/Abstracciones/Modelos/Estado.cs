using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class EstadoBase
    {
        [Required(ErrorMessage = "El nombre del estado es requerido")]
        [StringLength(50, ErrorMessage = "El nombre del estado debe tener máximo 50 caracteres")]
        public string NombreEstado { get; set; } = string.Empty;
    }

    public class EstadoRequest : EstadoBase
    {
        
    }

    public class EstadoResponse : EstadoBase
    {
        public Guid Id { get; set; }
    }
}