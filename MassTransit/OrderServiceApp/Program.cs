using MassTransit;
using SharedLibraryApp.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IServiceCollection services = builder.Services;
                    services.AddCors(c =>
                    {
                        c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod()
                          .AllowAnyHeader());
                    });

                    services.AddControllers();
                    services.AddMassTransit(config =>
                    {
                        config.UsingRabbitMq((context, conf) =>
                        {
                            conf.Host(Config.RABBIT_MQ_URI);
                        });
                    });

                    services.AddMassTransitHostedService();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.Run();

