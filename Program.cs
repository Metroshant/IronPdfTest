using IronPdfTest;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddMvcLocalization()
    .AddDataAnnotationsLocalization(
        options => options.DataAnnotationLocalizerProvider = (t, f) => f.Create(typeof(SharedResources)));
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var cultures = new List<CultureInfo> {
        new CultureInfo("en-CA"),
        new CultureInfo("fr-CA")
    };
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-CA");
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});

builder.Services.AddScoped(typeof(SharedViewLocalizer));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
}
);
app.Run();
