using Microsoft.Extensions.Configuration;

namespace MyProjects.Shared.Application.Cache
{
    public static class MemoryTree
    {
        public static double TimeToLive(IConfiguration configuration)
        {
            return configuration.GetValue<double>("Cache:timeToLive");
        }

    }
}
