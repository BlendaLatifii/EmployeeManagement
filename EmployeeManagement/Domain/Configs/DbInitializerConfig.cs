public class DbInitializerConfig
{
    public UserConfig Admin { get; set; } = new();
    public UserConfig Employee { get; set; } = new();
}