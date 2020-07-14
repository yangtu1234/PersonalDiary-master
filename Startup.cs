using System;
using System.IO;
using System.Reflection;
using System.Text;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PersonalDiary.Models;
using Swashbuckle.AspNetCore.Filters;

namespace PersonalDiary
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //���ʵ��ӳ�䵽Dto�ķ���
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //��������ݿ����ӷ���
            services.AddDbContext<PersonalDiaryContext>(option =>
            {
                option.UseSqlServer("Server=.;Database=PersonalDiary;integrated security=True;");
            });
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo { Title = "�����ռ���վApi", Version = "v1" });
                //setup.OperationFilter<AddResponseHeadersFilter>();
                //setup.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                setup.OperationFilter<SecurityRequirementsOperationFilter>();
                setup.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    Description = "������accessToken"
                });
            });
            services.AddAuthentication("Bearer").AddJwtBearer(configure =>
            {
                configure.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = "paiguBangBang",
                    ValidateAudience = true,
                    ValidAudience = "paiguSubscribe",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("yangtuweilaikeqi")),
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.FromDays(7),
                    ValidateLifetime = true
                };
            });
            services.AddCors(option =>
            {
                option.AddPolicy("default", configure =>
                {
                    configure.WithOrigins("http://localhost:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                    configure.WithExposedHeaders("X-Pagination");
                });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            //Swagger����ҵ
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PersonalDiary Api");
                c.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            app.UseCors("default");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            string basePath = AppContext.BaseDirectory;
            Assembly servicesAssembly = Assembly.LoadFrom(Path.Combine(basePath, "PersonalDiary.dll"));
            containerBuilder.RegisterAssemblyTypes(servicesAssembly)
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();
        }
    }
}
