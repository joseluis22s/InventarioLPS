using Microsoft.AspNetCore.Authorization;

namespace InventarioLPS.Services.Authorization
{
    public class PermisoHandler : AuthorizationHandler<PermisoRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermisoRequirement requirement)
        {
            var permisos = context.User.FindAll("Permiso").Select(c => c.Value);
            if (permisos.Contains(requirement.NombrePermiso))
            {
                context.Succeed(requirement);
            }
        }
    }
}
