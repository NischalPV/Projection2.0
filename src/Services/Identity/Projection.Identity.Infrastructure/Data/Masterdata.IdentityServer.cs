using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore.Models;
using Projection.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace Projection.Identity.Infrastructure.Data;

internal static partial class Masterdata
{
    public static void SeedIdentityServerMasterData(ModelBuilder builder)
    {
        SeedDefaultApplications(builder);
        SeedDefaultApplicationScopes(builder);
    }

    /// <summary>
    /// Seed default openiddict applications
    /// </summary>
    /// <param name="builder"></param>
    private static void SeedDefaultApplications(this ModelBuilder builder)
    {
        List<OpenIddictApplicationDescriptor> applicationDescriptors = OpenId.Config.ApplicationDescriptors;


        var applications = BuildApplications(applicationDescriptors);

        applications.ForEach(application => builder.Entity<OpenIddictEntityFrameworkCoreApplication>().HasData(application));

    }

    private static void SeedDefaultApplicationScopes(this ModelBuilder builder)
    {
        List<OpenIddictScopeDescriptor> scopeDescriptors = OpenId.Config.ScopeDescriptors;

        var scopes = BuildApplicationScopes(scopeDescriptors);

        scopes.ForEach(scope => builder.Entity<OpenIddictEntityFrameworkCoreScope>().HasData(scope));
    }

    private static List<OpenIddictEntityFrameworkCoreScope> BuildApplicationScopes(List<OpenIddictScopeDescriptor> scopeDescriptors)
    {
        var scopes = new List<OpenIddictEntityFrameworkCoreScope>();

        foreach (var descriptor in scopeDescriptors)
        {
            var scope = new OpenIddictEntityFrameworkCoreScope
            {
                Name = descriptor.Name,
                DisplayName = descriptor.DisplayName,
                Id = descriptor.Description,
                Description = descriptor.Descriptions[CultureInfo.GetCultureInfo("en-US")],
            };

            scope.SetScopeResources(descriptor.Resources.ToImmutableArray());
            scope.SetScopeDisplayNames(descriptor.DisplayNames);

            scopes.Add(scope);
        }

        return scopes;
    }


    private static void SetScopeResources(this OpenIddictEntityFrameworkCoreScope scope, ImmutableArray<string> resources)
    {
        ArgumentNullException.ThrowIfNull(scope);

        if (resources.IsDefaultOrEmpty)
        {
            scope.Resources = null;
        }

        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Indented = false
        });

        writer.WriteStartArray();

        foreach (var resource in resources)
        {
            writer.WriteStringValue(resource);
        }

        writer.WriteEndArray();
        writer.Flush();

        scope.Resources = Encoding.UTF8.GetString(stream.ToArray());
    }

    private static void SetScopeDisplayNames(this OpenIddictEntityFrameworkCoreScope scope, Dictionary<CultureInfo, string> displayNames)
    {
        ArgumentNullException.ThrowIfNull(scope);

        if (displayNames.Count == 0)
        {
            scope.DisplayNames = null;
        }

        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Indented = false
        });

        writer.WriteStartObject();

        foreach (var displayName in displayNames)
        {
            writer.WriteString(displayName.Key.Name, displayName.Value);
        }

        writer.WriteEndObject();
        writer.Flush();

        scope.DisplayNames = Encoding.UTF8.GetString(stream.ToArray());
    }

    private static List<OpenIddictEntityFrameworkCoreApplication> BuildApplications(List<OpenIddictApplicationDescriptor> applicationDescriptors)
    {
        var applications = new List<OpenIddictEntityFrameworkCoreApplication>();

        foreach (var descriptor in applicationDescriptors)
        {
            var application = new OpenIddictEntityFrameworkCoreApplication
            {
                ClientId = descriptor.ClientId,
                ClientSecret = descriptor.ClientSecret,
                DisplayName = descriptor.DisplayName,
                ApplicationType = descriptor.ApplicationType,
                ConsentType = descriptor.ConsentType,
                ClientType = descriptor.ClientType,
                Id = descriptor.Settings["Id"]

            };

            application.SetPermissionsAsync(descriptor.Permissions.ToImmutableArray());
            application.SetRedirectUrisAsync(descriptor.RedirectUris.Select(uri => uri.OriginalString).ToImmutableArray());
            application.SetPostLogoutRedirectUrisAsync(descriptor.PostLogoutRedirectUris.Select(uri => uri.OriginalString).ToImmutableArray());
            application.SetRequirementsAsync(descriptor.Requirements.ToImmutableArray());

            applications.Add(application);

        }

        return applications;
    }

    private static void SetPermissionsAsync(this OpenIddictEntityFrameworkCoreApplication application, ImmutableArray<string> permissions)
    {
        if (application is null)
        {
            throw new ArgumentNullException(nameof(application));
        }

        if (permissions.IsDefaultOrEmpty)
        {
            application.Permissions = null;
        }

        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Indented = false
        });

        writer.WriteStartArray();

        foreach (var permission in permissions)
        {
            writer.WriteStringValue(permission);
        }

        writer.WriteEndArray();
        writer.Flush();

        application.Permissions = Encoding.UTF8.GetString(stream.ToArray());
    }

    private static void SetPostLogoutRedirectUrisAsync(this OpenIddictEntityFrameworkCoreApplication application, ImmutableArray<string> uris)
    {
        if (application is null)
        {
            throw new ArgumentNullException(nameof(application));
        }

        if (uris.IsDefaultOrEmpty)
        {
            application.PostLogoutRedirectUris = null;
        }

        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Indented = false
        });

        writer.WriteStartArray();

        foreach (var uri in uris)
        {
            writer.WriteStringValue(uri.ToString());
        }

        writer.WriteEndArray();
        writer.Flush();

        application.PostLogoutRedirectUris = Encoding.UTF8.GetString(stream.ToArray());
    }

    private static void SetRedirectUrisAsync(this OpenIddictEntityFrameworkCoreApplication application, ImmutableArray<string> uris)
    {
        if (application is null)
        {
            throw new ArgumentNullException(nameof(application));
        }

        if (uris.IsDefaultOrEmpty)
        {
            application.RedirectUris = null;
        }

        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Indented = false
        });

        writer.WriteStartArray();

        foreach (var uri in uris)
        {
            writer.WriteStringValue(uri.ToString());
        }

        writer.WriteEndArray();
        writer.Flush();

        application.RedirectUris = Encoding.UTF8.GetString(stream.ToArray());
    }

    private static void SetRequirementsAsync(this OpenIddictEntityFrameworkCoreApplication application, ImmutableArray<string> requirements)
    {
        if (application is null)
        {
            throw new ArgumentNullException(nameof(application));
        }

        if (requirements.IsDefaultOrEmpty)
        {
            application.Requirements = null;
        }

        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Indented = false
        });

        writer.WriteStartArray();

        foreach (var requirement in requirements)
        {
            writer.WriteStringValue(requirement);
        }

        writer.WriteEndArray();
        writer.Flush();

        application.Requirements = Encoding.UTF8.GetString(stream.ToArray());
    }


}
