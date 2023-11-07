using Microsoft.EntityFrameworkCore;
using SportBet.Data;
using SportBet.DBContexts;
using SportBet.Repository;

namespace SportBet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MatchContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            builder.Services.AddScoped<IMatchRepository, MatchRepository>();
            builder.Services.AddScoped<IMatchOddRepository, MatchOddRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            // Database Initialization
            CreateDbIfNotExists(app);

            // Database Migration
            MigrateDb(app);

            // Generate DB Initialization Script
            GenerateDatabaseInitializationScript(app);

            app.Run();
        }

        private static void CreateDbIfNotExists(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<MatchContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        private static void MigrateDb(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<MatchContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError($"Migration error: {ex.Message}");
            }
        }

        private static void GenerateDatabaseInitializationScript(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<MatchContext>();

                    // Generate SQL script for the initial database creation
                    var initialDatabaseScript = context.Database.GenerateCreateScript();

                    // Get the application's root directory
                    string appRootDirectory = AppContext.BaseDirectory;

                    // Specify the export file path relative to the root directory
                    string exportFilePath = Path.Combine(appRootDirectory, "InitialDatabaseScript.sql");

                    // Save the initial database script to the export file
                    File.WriteAllText(exportFilePath, initialDatabaseScript);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError($"{ex.Message}");
                }
            }
        }
    }
}
