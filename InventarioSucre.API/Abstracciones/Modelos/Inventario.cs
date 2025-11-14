using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class InventarioBase
    {
        [StringLength(50, ErrorMessage = "La identificación debe tener máximo 50 caracteres")]
        public string? NumeroIdentificacion { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "La descripción debe tener entre 5 y 200 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "La marca debe tener máximo 100 caracteres")]
        public string? Marca { get; set; }

        [StringLength(100, ErrorMessage = "El modelo debe tener máximo 100 caracteres")]
        public string? Modelo { get; set; }

        [StringLength(100, ErrorMessage = "La serie debe tener máximo 100 caracteres")]
        public string? Serie { get; set; }

        [Required(ErrorMessage = "La ubicación es requerida")]
        [StringLength(100, ErrorMessage = "La ubicación debe tener máximo 100 caracteres")]
        public string Ubicacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El modo de adquisición es requerido")]
        [StringLength(100, ErrorMessage = "El modo de adquisición debe tener máximo 100 caracteres")]
        public string ModoAdquisicion { get; set; } = string.Empty;

        public string? Observaciones { get; set; }
        public string? ObservacionesInstitucionales { get; set; }
    }

    public class InventarioRequest : InventarioBase
    {
        [Required(ErrorMessage = "El estado es requerido")]
        public Guid IdEstado { get; set; }
    }

    public class InventarioResponse : InventarioBase
    {
        public Guid Id { get; set; }
        public string Estado { get; set; } = string.Empty; 
    }
}