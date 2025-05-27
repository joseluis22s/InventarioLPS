using System.ComponentModel.DataAnnotations;

namespace InventarioLPS.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "*Campo requerido.")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "*Campo requerido.")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }
    }
}
