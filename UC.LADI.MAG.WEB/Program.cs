using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text;
using UC.LADI.MAG.WEB.Infrastructure.Authentication;
using UC.LADI.MAG.WEB.Infrastructure.Swagger;
using UC.LADI.MAG.WEB.Services;
using UC.Core.Common;
using UC.Core.Interfaces;
using UC.Core.Models.Ums;
using Serilog;
using Serilog.Events;
using UC.LADI.MAG.WEB.Services.DbContext.master;
using UC.LADI.MAG.WEB.Common;
using Npgsql;
using System.Data;
using System.Configuration;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Override("Microsoft", LogEventLevel.Fatal)
	.MinimumLevel.Fatal()
	.WriteTo.File(
		"logs/logfile.log",
		rollingInterval: RollingInterval.Day,
		shared: true, 
		outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Message:lj}{NewLine}{Exception}")
	.CreateLogger();

var config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json").Build();

string AllowOrigins = "TrustedOrigins";
string allowedHosts = "*";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowOrigins,
    builder =>
    {
        builder.WithOrigins(allowedHosts).AllowAnyHeader().AllowAnyMethod();
    });
});



builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddMemoryCache();

string sessionTimeout = config.GetSection("SessionTimeout").Value;
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(string.IsNullOrEmpty(sessionTimeout) ? 30 : int.Parse(sessionTimeout));
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<IDictionary<string, List<string>>>(new Dictionary<string, List<string>>());
builder.Services.AddSingleton<LangResource>(new LangResource());

builder.Services.AddSignalR(options => { options.KeepAliveInterval = TimeSpan.FromSeconds(5); }).AddMessagePackProtocol();

#region Register Localization
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

CultureInfo[] cultureInfos = { new CultureInfo("vi"), new CultureInfo("en") };

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(cultureInfos[0]);
    options.SupportedCultures = cultureInfos;
    options.SupportedUICultures = cultureInfos;
    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider()
    };
});

#endregion

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "UC.LADI.MAG.WEB",
        Version = "v1"
    });
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
    c.CustomSchemaIds(i => i.FullName);
    c.SchemaFilter<SwaggerExcludeFilter>();
});


builder.Services.AddScoped<DbSession>();
builder.Services.AddTransient<UnitOfWork>();




builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserProvider, UserProvider>();
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

builder.Services.AddScoped(typeof(IServiceWrapper), typeof(ServiceWrapper));
builder.Services.AddHttpContextAccessor();

var jwtTokenConfig = config.GetSection("IdentityServerAuthentication").Get<IdentityServerAuthentication>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = jwtTokenConfig.RequireHttpsMetadata;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtTokenConfig.Issuer,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
        ValidAudience = jwtTokenConfig.Audience,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});





var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseRequestLocalization();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseStaticFiles();

string root = Path.Combine(Directory.GetCurrentDirectory(), "static");
if (!File.Exists(root))
{
    Directory.CreateDirectory(root);
}

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(root),
    RequestPath = "/static",
    EnableDefaultFiles = true
});

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Events}/{action=Spring}/{id?}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

HandleRedirects(app);

app.Run();

#region HandleRedirects
void HandleRedirects(IApplicationBuilder app)
{
	// Old Url, New Url
	var redirects = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
	{

    };

	app.Use(async (context, next) =>
	{

        if (redirects != null)
		{
			var url = context.Request.Path.Value;

			if (!string.IsNullOrEmpty(url) && redirects.TryGetValue(url, out var redirectUrl))
			{
                context.Response.Redirect(redirectUrl);
				return;
            }
		}
		await next();
	});
}
#endregion
