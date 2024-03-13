namespace PaperUniverse.Core;

public static class Configuration
{
    public static DatabaseConfiguration Database { get; set; } = new DatabaseConfiguration();
    public static SmtpConfiguration Smtp { get; set; } = new SmtpConfiguration();
}

public class DatabaseConfiguration 
{
    public string ConnectionString { get; set; } = string.Empty;
}

public class SmtpConfiguration 
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Server { get; set; } = string.Empty;
    public int Port { get; set; }
}