using EventSourcing.Domain.Account;
using Marten;

namespace EventSourcing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMarten(options =>
            {
                options.Connection("Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=postgres");
                options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All;
                options.DisableNpgsqlLogging = true;

                options.Projections.LiveStreamAggregation<Account>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}