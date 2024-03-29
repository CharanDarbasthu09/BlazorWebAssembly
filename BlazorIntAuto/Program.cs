using BlazorIntAuto.Client.Pages;
using BlazorIntAuto.Components;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using MatBlazor;
using BlazorIntAuto.AuthenticationStateSyncer;
using Blazored.Modal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddMatBlazor();
builder.Services.AddHttpClient();


builder.Services
    .AddAuth0WebAppAuthentication(options =>
    {
      options.Domain = builder.Configuration["Auth0:Domain"];
      options.ClientId = builder.Configuration["Auth0:ClientId"];
    });

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddBlazoredModal();

var app = builder.Build();

app.UseAntiforgery();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseWebAssemblyDebugging();
}
else
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthorization();
app.UseAuthentication();


app.MapGet("/Account/Login", async (HttpContext httpContext, string redirectUri = "/") =>
{
  var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
          .WithRedirectUri(redirectUri)
          .Build();

  await httpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
});

app.MapGet("/Account/Logout", async (HttpContext httpContext, string redirectUri = "/") =>
{
  var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
          .WithRedirectUri(redirectUri)
          .Build();

  await httpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
  await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

await app.RunAsync();
