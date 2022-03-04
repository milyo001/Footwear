namespace MusicStore.Test
{
    using Footwear;
    using Microsoft.Extensions.Configuration;

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}