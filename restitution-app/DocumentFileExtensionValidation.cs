#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using Gov.Cscp.VictimServices.Public.Models;

namespace Gov.Cscp.VictimServices.Public;

/// <summary>
/// Server-side document filename extension allowlist aligned with ClientApp
/// <c>config.ts</c> <c>accepted_file_extensions</c> (VULN-002).
/// </summary>
public static class DocumentFileExtensionValidation
{
    public static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        "pdf",
        "png",
        "jpeg",
        "jpg",
        "doc",
        "docx",
        "ppt",
    };

    public static bool IsAllowedFilename(string? filename)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            return false;
        }

        var extension = Path.GetExtension(filename).TrimStart('.');
        if (string.IsNullOrEmpty(extension))
        {
            return false;
        }

        return AllowedExtensions.Contains(extension);
    }

    public static bool TryValidateDocuments(IEnumerable<DocumentDto>? documents, out string? error)
    {
        error = null;
        if (documents is null)
        {
            return true;
        }

        foreach (var document in documents)
        {
            if (document is null)
            {
                continue;
            }

            if (!IsAllowedFilename(document.Filename))
            {
                error = $"Unsupported file type: {document.Filename}";
                return false;
            }
        }

        return true;
    }
}
