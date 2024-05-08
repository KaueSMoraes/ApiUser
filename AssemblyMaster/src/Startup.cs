using AssemblyMaster.Services;
using AssemblyMaster.Security;

namespace AssemblyMaster
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
            // Adicione serviços específicos aqui, como o serviço UserController.
            services.AddControllers();
            services.AddScoped<UserService>();

              services.AddSwaggerGen(c =>
            {
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "create",
                pattern: "api/create",
                defaults: new { controller = "UserController", action = "create" });

                endpoints.MapControllerRoute(
                name: "getusers",
                pattern: "api/getusers",
                defaults: new { controller = "UserController", action = "getusers" });

                endpoints.MapControllerRoute(
                name: "delete",
                pattern: "api/delete",
                defaults: new { controller = "UserController", action = "delete" });

                endpoints.MapControllerRoute(
                name: "updatepassword",
                pattern: "api/updatepassword",
                defaults: new { controller = "UserController", action = "updatepassword" });

                endpoints.MapControllerRoute(
                name: "getguid",
                pattern: "api/getguid",
                defaults: new { controller = "UserController", action = "getguid" });
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}