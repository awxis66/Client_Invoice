using Client_Invoice_System.Components;
using Client_Invoice_System.Data;
using Client_Invoice_System.Repositories;
using Client_Invoice_System.Repository;
using Client_Invoice_System.Services;
using Microsoft.EntityFrameworkCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<ResourceRepository>();
builder.Services.AddScoped<InvoiceService>();
builder.Services.AddScoped<InvoiceRepository>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<ActiveClientRepository>();
builder.Services.AddScoped<OwnerRepository>();
builder.Services.AddScoped<CountryCurrencyRepository>();

//var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"]);
//builder.Services.AddAuthentication(options =>
//{
//    //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.RequireHttpsMetadata = false;
//    options.SaveToken = true;
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ClockSkew = TimeSpan.Zero
//    };
//});
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
