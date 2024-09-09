using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Biblioteca.API.Endpoints;
using Biblioteca.API.Filters;
using Biblioteca.Entities;
using Biblioteca.Persistence;
using Biblioteca.Repositories;
using Biblioteca.Services.Implementation;
using Biblioteca.Services.Interface;
using Biblioteca.Services.Profiles;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Options pattern register
builder.Services.Configure<AppSettings>(builder.Configuration);

//CORS
var corsConfiguration = "BibliotecaCors";
builder.Services.AddCors(setup =>
{
    setup.AddPolicy(corsConfiguration, policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});
 
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(FilterExceptions));
});
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuring Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

//Identity
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:JWTKey"] ??
    throw new InvalidOperationException("JWT Key not configured."));
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

builder.Services.AddIdentity<BibliotecaUserIdentity, IdentityRole>(policies =>
{
    policies.Password.RequireDigit = true;
    policies.Password.RequiredLength = 6;
    policies.User.RequireUniqueEmail = false; 
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();

//Registering services
builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
builder.Services.AddTransient<ILibroRepository, LibroRepository>();
builder.Services.AddTransient<IPedidoDetalleRepository, PedidoDetalleRepository>();
builder.Services.AddTransient<IPedidoCabeceraRepository, PedidoCabeceraRepository>();

builder.Services.AddTransient<IClienteService, ClienteService>();
builder.Services.AddTransient<ILibroService, LibroService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IPedidoCabeceraService, PedidoCabeceraService>(); 
builder.Services.AddTransient<IPedidoDetalleService, PedidoDetalleService>();

//Registering healthchecks
builder.Services.AddHealthChecks()
    .AddCheck("selfcheck", () => HealthCheckResult.Healthy())
    .AddDbContextCheck<ApplicationDbContext>();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<ClienteProfile>();
    config.AddProfile<LibroProfile>();
    config.AddProfile<PedidoCabeceraProfile>();
    config.AddProfile<PedidoDetalleProfile>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(corsConfiguration);

app.MapReports(); 

app.MapControllers();

//Scope
using (var scope = app.Services.CreateScope())
{
    //Auto-migrations
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await db.Database.MigrateAsync();

    //seed data for the admin default user
    await UserDataSeeder.Seed(scope.ServiceProvider);
}

//Configuring health checks
app.MapHealthChecks("/healthcheck", new()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();