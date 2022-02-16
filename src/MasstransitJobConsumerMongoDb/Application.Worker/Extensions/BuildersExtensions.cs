namespace Application.Worker.Extensions
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class BuildersExtensions
    {
        public static void ConfigureHost(this IConfigurationBuilder builder)
        {
        }

        public static void ConfigureApp(this HostBuilderContext context, IConfigurationBuilder config)
        {
        }

        public static void ConfigureServices(this HostBuilderContext context, IServiceCollection services)
        {
            services.AddLogger();
            services.AddMessaging(context.Configuration);
        }
    }
}