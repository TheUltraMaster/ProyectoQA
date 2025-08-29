using System.ComponentModel.DataAnnotations;

namespace Proyecto.DTOS;

public class UsuarioDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El correo electrónico es obligatorio")]
    [StringLength(50, ErrorMessage = "El correo electrónico no puede exceder los 50 caracteres")]
    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido")]
    public string Usuario { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres")]
    public string Password { get; set; } = string.Empty;

    public bool IsAdmin { get; set; } = false;

    public bool Activo { get; set; } = true;
}


