namespace Footwear
{
    using Footwear.Data;
    using Footwear.Data.Models;
    using Footwear.Middlewares;
    using Footwear.Services.CartService;
    using Footwear.Services.OrderService;
    using Footwear.Services.ProductService;
    using Footwear.Services.TokenService;
    using Footwear.Services.UserService;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SpaServices.AngularCli;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Text;
    using Footwear.Services.MailService;
    using Footwear.Settings;

    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            StaticConfig = configuration;
            CurrentEnvironment = env;
        }

        private IWebHostEnvironment CurrentEnvironment { get; }

        public IConfiguration Configuration { get; }

        // A static configuration for static members
        public static IConfiguration StaticConfig { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            services.AddControllersWithViews();
            services.AddAutoMapper(typeof(Startup));

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../ClientApp/dist";
            });

            // CORS configuration
            services.AddCors(opt => opt.AddPolicy("FootwearCorsPolicy", builder =>
            {
                string stripeUrl = Configuration.GetSection("AllowedOrigins:StripeUrl").Value;
                string clientUrl = Configuration.GetSection("AllowedOrigins:ClientUrl").Value;

                builder.WithOrigins(stripeUrl, clientUrl)
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));


            // Database confirguration and identity
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            });

            // Auth token configuration
            var signingKey = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false; //There is no need to save token in server
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Scoped services are better option when you want to maintain state within a request.
            // Transient services are created every time they will use more memory & resources and can
            // have the negative impact on performance
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IMailService, MailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // You can change enviroment in Properties/launchSettings.json
            if (CurrentEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else if (CurrentEnvironment.IsStaging())
            {
                // Used for testing the development and mirror an actual production environment
            }
            else if (CurrentEnvironment.IsProduction())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            }


            app.UseStaticFiles();
            if (!CurrentEnvironment.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            // A middleware used for decrypting the cookie send from client, the cookie value is encrypted token generated
            // in UserController/Login
            app.UseDecryptCookieMiddleware();

            app.UseCors("FootwearCorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

            });

            // Will execute the client start script to run the front-end framework. For localhost usage only, do not
            // use when hosting the API, use only in development enviroment
            if (CurrentEnvironment.IsDevelopment())
            {
                app.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "../ClientApp";
                    spa.UseAngularCliServer(npmScript: "start");
                });
            }

        }
    }
}
