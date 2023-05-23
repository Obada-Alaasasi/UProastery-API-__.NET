using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UProastery.Data.DB;
using UProastery.Data.User;
using UProastery.Configs;
using UProastery.Helpers;
using UProastery.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


// Add connection string
string ConnectionString = builder.Configuration.GetConnectionString("UProastery");

// Add Database Context
builder.Services.AddDbContext<UP_context>(options => {
    options.UseSqlServer(ConnectionString);
});

// Add Http context
builder.Services.AddHttpContextAccessor();

// add identity and role services
builder.Services.AddIdentityCore<ApiUser>(options =>
    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.Email)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UP_context>();

// add UserIdClaimType for user identification duting ops
//builder.Services.Configure<IdentityOptions>(options =>
 //   options.ClaimsIdentity.UserIdClaimType = ClaimTypes.Email);

// Inject automapper
builder.Services.AddAutoMapper(typeof(MapperConfig));

// add auth and order manager as a scoped service
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<IOrderManager, OrderManager>();

// add authentication service using JWT tokens
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
