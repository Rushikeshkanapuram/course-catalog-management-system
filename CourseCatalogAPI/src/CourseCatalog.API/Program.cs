    using CourseCatalog.Application.Interfaces.Services;
    using CourseCatalog.Application.Mapping;
    using CourseCatalog.Application.Services;
    using CourseCatalog.Application.Settings;
    using CourseCatalog.Infrastructure.DependencyInjection;
    using CourseCatalog.Infrastructure.Persistence;
    using CourseCatalog.Infrastructure.Seed;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;
using CourseCatalog.API.Middleware;

var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();
    builder.Services.Configure<JwtSettings>(
        builder.Configuration.GetSection("Jwt"));

    var jwtSettings = builder.Configuration
        .GetSection("Jwt")
        .Get<JwtSettings>()!;
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,

                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Key))
            };
        });
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddScoped<ICourseService, CourseService>();
    builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddAutoMapper(
        cfg => { },
        typeof(CourseProfile).Assembly);
    //// Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


var app = builder.Build();
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();

        await DatabaseSeeder.SeedAsync(context);
    }
// Configure pipeline
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

app.UseCors("AngularPolicy");

app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();