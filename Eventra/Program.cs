using Application;
using Application.Common.Behaviors;
using Application.Common.Events;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Broadcasters;
using Application.Common.Interfaces.Contexts;
using Application.Common.Interfaces.Dispatchers;
using Application.Common.Interfaces.QueryRepositories;
using Application.Common.Interfaces.Validations;
using Application.Projects.Commands.CreateProject;
//using Application.Users.Validators;
using Domain.Interfaces;
using Eventra.Broadcasting;
using Eventra.Hubs;
using Eventra.Middlewares;
using FluentValidation;
using Infrastructure.Auth;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
namespace Eventra
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Context and Validation Behaviour

            builder.Services.AddDbContext<EventraDBContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Application.ApplicationAssemblyMarker).Assembly);
            });
            builder.Services.AddValidatorsFromAssembly(typeof(ValidationBehavior<,>).Assembly);
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Application.Common.Behaviors.ValidationBehavior<,>));
            #endregion
            #region Realtime
            builder.Services.AddSignalR();
            builder.Services.AddScoped<INoticeBroadcaster, SignalRNoticeBroadcaster>();
            #endregion
            #region Registering Services Lifetime
            builder.Services.AddScoped<ProjectRepository>();

            builder.Services.AddScoped<IProjectRepository>(
                sp => sp.GetRequiredService<ProjectRepository>());

            builder.Services.AddScoped<IProjectQueryRepository>(
                sp => sp.GetRequiredService<ProjectRepository>());


            // -------------------- Sprint --------------------
            builder.Services.AddScoped<SprintRepository>();

            builder.Services.AddScoped<ISprintRepository>(
                sp => sp.GetRequiredService<SprintRepository>());

            builder.Services.AddScoped<ISprintQueryRepository>(
                sp => sp.GetRequiredService<SprintRepository>());


            // -------------------- User --------------------
            builder.Services.AddScoped<UserRepository>();

            builder.Services.AddScoped<IUserRepository>(
                sp => sp.GetRequiredService<UserRepository>());

            builder.Services.AddScoped<IUserQueryRepository>(
                sp => sp.GetRequiredService<UserRepository>());


            // -------------------- Issue --------------------
            builder.Services.AddScoped<IssueRepository>();

            builder.Services.AddScoped<IIssueRepository>(
                sp => sp.GetRequiredService<IssueRepository>());

            builder.Services.AddScoped<IIssueQueryRepository>(
                sp => sp.GetRequiredService<IssueRepository>());

            // ------------------ Notice -----------------------
            builder.Services.AddScoped<INoticeRepository, NoticeRepository>();
            // ------------------- Misc -----------------------
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            builder.Services.AddScoped<ISprintValidationService,  SprintValidationService>();
            builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            #endregion
            #region Misc Registrations
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IUserContext, UserContext>();
            #endregion
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // This makes enums serialize/deserialize as their string names
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            #region Configuring JWT 
            // JWT Authentication
            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
                        )
                    };
                });

            // Authorization
            builder.Services.AddAuthorization();
            #endregion
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.MapHub<EventraHub>("/hubs/eventra");

            app.Run();
        }
    }
}
