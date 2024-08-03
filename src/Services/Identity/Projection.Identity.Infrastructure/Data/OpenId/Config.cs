using OpenIddict.Abstractions;
using Projection.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection.Identity.Infrastructure.Data.OpenId;

public static class Config
{
    public static List<OpenIddictApplicationDescriptor> ApplicationDescriptors =>
        [
            new OpenIddictApplicationDescriptor()
            {
                ClientId = "projection-ui--dev",
                ClientSecret = "projection@2023".Sha256(),
                DisplayName = "Projection Frontend UI OpenId Client",
                //ClientType = OpenIddictConstants.ClientTypes.Public,
                ApplicationType = OpenIddictConstants.ApplicationTypes.Web,
                ConsentType = OpenIddictConstants.ConsentTypes.Implicit,
                Permissions =
                {
                        OpenIddictConstants.Permissions.Endpoints.Introspection,
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Logout,
                        OpenIddictConstants.Permissions.Endpoints.Device,
                        OpenIddictConstants.Permissions.GrantTypes.Implicit,
                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                        OpenIddictConstants.Permissions.ResponseTypes.Code,
                        OpenIddictConstants.Permissions.ResponseTypes.IdToken,
                        OpenIddictConstants.Permissions.ResponseTypes.Token,
                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles,
                        OpenIddictConstants.Permissions.Prefixes.Scope + "projection-tenants-api"
                },
                DisplayNames =
                {
                    [CultureInfo.GetCultureInfo("en-US")] = "Projection Frontend UI OpenId Client",
                    [CultureInfo.GetCultureInfo("fr-FR")] = "Client OpenId de l'interface utilisateur du frontend de projection",
                    [CultureInfo.GetCultureInfo("de-DE")] = "OpenId-Client für die Projektions-Frontend-Benutzeroberfläche",
                },
                Requirements =
                {
                    OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
                },
                RedirectUris =
                {
                    new Uri("http://localhost:6002/signin-oidc")
                },
                PostLogoutRedirectUris =
                {
                    new Uri("http://localhost:6002/signout-callback-oidc")
                }
            },

        new OpenIddictApplicationDescriptor()
            {
                ClientId = "projection-tenants-api--dev",
                ClientSecret = "projection@2023".Sha256(),
                DisplayName = "Projection Tenants API OpenId Client",
                ClientType = OpenIddictConstants.ClientTypes.Confidential,
                ApplicationType = OpenIddictConstants.ApplicationTypes.Web,
                ConsentType = OpenIddictConstants.ConsentTypes.Implicit,
                Permissions =
                {
                        OpenIddictConstants.Permissions.Endpoints.Introspection,
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Logout,
                        OpenIddictConstants.Permissions.Endpoints.Device,
                        OpenIddictConstants.Permissions.GrantTypes.Implicit,
                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                        OpenIddictConstants.Permissions.ResponseTypes.Code,
                        OpenIddictConstants.Permissions.ResponseTypes.IdToken,
                        OpenIddictConstants.Permissions.ResponseTypes.Token,
                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles,
                        OpenIddictConstants.Permissions.Prefixes.Scope + "projection-tenants-api"
                },
                DisplayNames =
                {
                    [CultureInfo.GetCultureInfo("en-US")] = "Projection Tenants API OpenId Client",
                    [CultureInfo.GetCultureInfo("fr-FR")] = "Client OpenId de l'API des locataires de projection",
                    [CultureInfo.GetCultureInfo("de-DE")] = "OpenId-Client für die Projektionsmieter-API",
                },
                Requirements =
                {
                    OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
                },
                RedirectUris =
                {
                    new Uri("https://localhost:7044/swagger/oauth2-redirect.html")
                },
                PostLogoutRedirectUris =
                {
                    new Uri("https://localhost:7044/swagger/")
                }
        }

        ];


    public static List<OpenIddictScopeDescriptor> ScopeDescriptors =>
        [
            new OpenIddictScopeDescriptor()
            {
                Name = "projection-tenants-api",
                Resources =
                {
                    "projection-tenants-api"
                },
                DisplayNames =
                {
                    [CultureInfo.GetCultureInfo("en-US")] = "Access to the Projection Tenants API",
                    [CultureInfo.GetCultureInfo("fr-FR")] = "Accès à l'API des locataires de projection",
                    [CultureInfo.GetCultureInfo("de-DE")] = "Zugriff auf die Projektionsmieter-API",
                }
            }
        ];
}
