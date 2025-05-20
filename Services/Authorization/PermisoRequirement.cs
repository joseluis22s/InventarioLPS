using Microsoft.AspNetCore.Authorization;

namespace InventarioLPS.Services.Authorization
{
    public class PermisoRequirement : IAuthorizationRequirement
    {
        public string NombrePermiso { get; }
        public PermisoRequirement(string nombrePermiso) => NombrePermiso = nombrePermiso;
    }
}
