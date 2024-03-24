#pragma warning disable CS8618
namespace Order.Host.Configurations;

public class OrderConfig
{
    public string Host { get; set; }
    public string ImgUrl { get; set; }
    public string ConnectionString { get; set; }
}