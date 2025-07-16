using Supabase;
using WiseBlog.Components;

var builder = WebApplication.CreateBuilder(args);

// Load Supabase settings from appsettings.json or environment
var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabaseKey = builder.Configuration["Supabase:Key"];

var supabaseOptions = new SupabaseOptions
{
    AutoConnectRealtime = false
};

// Create and initialize Supabase client
var client = new Supabase.Client(supabaseUrl, supabaseKey, supabaseOptions);
await client.InitializeAsync(); // ✅ REQUIRED

// Register services
builder.Services.AddSingleton(client);
builder.Services.AddScoped<WiseBlog.Data.SupabaseAuthService>();
builder.Services.AddScoped<WiseBlog.Data.BlogService>();
builder.Services.AddScoped<WiseBlog.Data.CategoryService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();