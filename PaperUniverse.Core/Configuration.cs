namespace PaperUniverse.Core;

public static class Configuration
{
    public static DatabaseConfiguration Database { get; set; } = new DatabaseConfiguration();
}

public class DatabaseConfiguration 
{
    public string ConnectionString { get; set; } = string.Empty;
}