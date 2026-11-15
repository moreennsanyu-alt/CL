using System.ComponentModel.DataAnnotations;

namespace ClinicManager.Win.Configuration;

/// <summary>
/// Strongly typed settings, bound from the "AppConfig" section.
/// </summary>
public class ApplicationConfiguration
{
    public const string SectionName = "AppConfig";

    [Required]
    public string ApplicationName { get; set; } = "CmApp";

    public string Environment { get; set; } = "Development";

    [Range(1, 65535)]
    public int Port { get; set; } = 5000;

    public bool EnableLogging { get; set; } = true;

    public string? ConnectionString { get; set; }

    public string? StorageEntropy { get; set; }
    
}
