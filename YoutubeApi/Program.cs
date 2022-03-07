
namespace YoutubeApi
{
    class Program
    {
        public static IConfigurationRoot configuration = new ConfigurationBuilder()
                                                               .SetBasePath(Directory.GetCurrentDirectory())
                                                               .AddJsonFile("appsettings.json", false, true)
                                                               .AddEnvironmentVariables()
                                                               .Build();
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

