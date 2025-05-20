using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace InventarioLPS.Services.Authorization
{
    public class PermisoPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly DefaultAuthorizationPolicyProvider _fallbackPolicyProvider;

        public PermisoPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            _fallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var policy = new AuthorizationPolicyBuilder();
            policy.AddRequirements(new PermisoRequirement(policyName));
            return Task.FromResult(policy.Build());
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() =>
            _fallbackPolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() =>
            _fallbackPolicyProvider.GetFallbackPolicyAsync();
    }

}
