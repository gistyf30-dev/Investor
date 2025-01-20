using InvestorsAPI.Database;
using InvestorsAPI.Services;
using InvestorsAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<InvestorDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("USBankDbConnectionString")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IInvestorFundsRepository, InvestorFundsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

//Seed the database
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        SeedData.Initialize(services);
    }
    catch(Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation(ex, "An error occurred seeding the database.");
    }
}

app.UseSwagger();
app.UseSwaggerUI();
    
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
//    c.RoutePrefix = string.Empty; //To Launch Swagger UI at the root
//});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseAuthorization();

// Set your default controller
//app.MapControllerRoute( name: "default", pattern: "{controller=Investors}/{action=Index}/{id?}");

app.Run();
