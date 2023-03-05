using InventoryServiceApp.Consumers;
using MassTransit;
using SharedLibraryApp.Configurations;
using SharedLibraryApp.Constraints;

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
                        config.AddConsumer<OrderConsumer>();
                        config.UsingRabbitMq((context, conf) =>
                        {
                            conf.Host(Config.RABBIT_MQ_URI);
                            conf.ReceiveEndpoint(QueueConstratint.ORDER_QUEUE, c => {
                                c.ConfigureConsumer<OrderConsumer>(context);
                            });
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


app.Run();
