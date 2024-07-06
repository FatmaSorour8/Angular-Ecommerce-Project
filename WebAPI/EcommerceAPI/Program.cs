using Azure.Core;
using EcommerceAPI.Models;
using EcommerceAPI.Unit_Of_Work;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(
    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<unitOfWork>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Ecommerce API",
        Version = "v1",
        Description = "This Awesome Web Api generated to help you to use all the services provided.",
        Contact = new OpenApiContact
        {
        },
    });
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "file.xml"));
    options.EnableAnnotations();
    #region add security definition
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization Example: 'Bearer'",
        Name = "Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement(){
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme },
                In = ParameterLocation.Header,
                Scheme = "outh2",
                Name = "Bearer",
            },
            new List<string>()

        }
    });
    #endregion
});

builder.Services.AddDbContext<EcommerceContext>(options => {
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("Ecom")
        );
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
);

string Policies = "";
builder.Services.AddCors(options => {
    options.AddPolicy(Policies,
    builder => {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<EcommerceContext>()
    .AddDefaultTokenProviders();

#region jwt create schema
//builder.Services.AddAuthentication(options =>
//    options.DefaultAuthenticateScheme = "jwtScheme"
//).AddJwtBearer("jwtScheme", options => {
//    ////var jwtSettings = builder.Configuration.GetSection("JWTSetting").Get<JwtSettings>();
//    ////var key = jwtSettings.SecurityKey;
//    var _key = "M!F@E#A$N%T^3&M*M(A)A_S+N=O!W@R#A$O%R^U&A&R*B(M)D_O+E;H:L{A}M{M}O[M]N[E]`E`D;I;_M_";
//    var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key));
//});
#endregion

#region jwt use default schema
var JwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(options =>
{
    //This property sets the default scheme that will be used for authenticating the user's identity.
    //When a request is received, ASP.NET Core checks for authentication credentials using the scheme specified here.
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

    //This property sets the default scheme that will be used to challenge unauthorized requests.
    //When a request is received without authentication credentials or with invalid credentials, ASP.NET Core needs to send a challenge back to the client, indicating that authentication is required.
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    // options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
       .AddJwtBearer(o => {
           o.SaveToken = true;
           o.RequireHttpsMetadata = false;
           o.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidAudience = JwtSettings["ValidAudience"],
               ValidIssuer = JwtSettings["ValidIssuer"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.GetSection("securityKey").Value!))
           };
       });
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    //app.MapSwagger().RequireAuthorization();

    app.UseSwaggerUI();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(Policies);

app.MapControllers();

app.Run();