using _01_Framework.Application;
using InventoryManagement.Configuration;
using ServiceHost.Uploder;
using ShopManagement.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


//connection string
var connectionstring = builder.Configuration.GetConnectionString("TA_NewVersion_DB");

//services
ShopManagement.Configuration.ShopManagementBootstrapper.Configure(builder.Services, connectionstring);
DiscountManagement.Configuration.DiscountManagementBootstrapper.Configure(builder.Services,connectionstring);
InventoryManagement.Configuration.InventoryManagementBootstrapper.Configure(builder.Services, connectionstring);
CommentManagement.Configuration.CommentManagementBootstrapper.Configure(builder.Services, connectionstring);
ArticleManagement.Configuration.ArticleManagementBootstrapper.Configure(builder.Services, connectionstring);

//FileUploader System Configuration
builder.Services.AddTransient<IFileUploader, FileUploader>();

//To add new encoder To show viewdata html in application



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
