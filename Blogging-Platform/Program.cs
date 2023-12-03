using Blogging_Platform.DB.DB;
using Blogging_Platform.DB.Repositories;
using Blogging_Platform.Domain.AutoMapper;
using Blogging_Platform.Domain.Entity;
using Blogging_Platform.Domain.Options;
using Blogging_Platform.Domain.Repo;
using Blogging_Platform.Services.AuthService;
using Blogging_Platform.Services.PostService;
using Blogging_Platform.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
builder.Logging.ClearProviders();

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Logging.AddSerilog(logger);


builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.Configure<JwtOption>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>();

builder.Services.AddDbContext<ApplicationDBContext>(option =>
        option.UseSqlServer(builder.Configuration.GetConnectionString("BloggingDatabase")
            , x => x.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)));

builder.Services.AddAuthentication(option =>
  {
      option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  }).AddJwtBearer(o =>
   {
       o.RequireHttpsMetadata = false;
       o.SaveToken = false;
       o.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuerSigningKey = true,
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidIssuer = builder.Configuration["Jwt:Issuer"],
           ValidAudience = builder.Configuration["Jwt:Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
       };
   });
builder.Services.AddControllers();

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(PostProfile)));

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
