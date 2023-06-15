using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Project_NGO.Data;
using Microsoft.Extensions.FileProviders;
using Project_NGO.Services.Abouts;
using Project_NGO.Repositories.Abouts;
using Project_NGO.Models;
using Project_NGO.Repositories;
using Project_NGO.Repositories.Authenication;
using Project_NGO.Repositories.CashOutRepo;
using Project_NGO.Repositories.Categories;
using Project_NGO.Repositories.NewFolder;
using Project_NGO.Repositories.ReceiptRepo;
using Project_NGO.Repositories.Chart;
using Project_NGO.Repositories.UploadFileRepo;
using Project_NGO.Repositories.UserRepo;
using Project_NGO.Requests.Program;
using Project_NGO.Services;
using Project_NGO.Services.Authenication;
using Project_NGO.Services.CashOutService;
using Project_NGO.Services.Categories;
using Project_NGO.Services.ContactForm;
using Project_NGO.Services.ReceiptService;
using Project_NGO.Services.Chart;
using Project_NGO.Services.UploadFileService;
using Project_NGO.Services.UserService;
using System.Text;
using Project_NGO.Services.Categories;
using Project_NGO.Services.ContactForm;
using FileServiceImp = Project_NGO.Services.UploadFileService.FileServiceImp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ProgramRepository, ProgramService>();
builder.Services.AddAutoMapper(typeof(ProgramProfile));
builder.Services.AddScoped<ICashOutRepository, CashOutServiceImp>();
builder.Services.AddScoped<IReceiptRepository, ReceiptServiceImp>();
// config identity
builder.Services.AddIdentity<User, IdentityRole<int>>(options => options.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

// config dbcontext
builder.Services.AddScoped<IFileRepository, FileServiceImp>();
builder.Services.AddScoped<ICategory, CategoryServiceImp>();
builder.Services.AddScoped<IContactForm, ContactFormServiceImp>();
builder.Services.AddScoped<IContactChartForm, ContactChartFormServiceImp>();
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectDb"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// config DI
builder.Services.AddScoped<IAbout, AboutServiceImp>();
builder.Services.AddScoped<IAuthRepository, AuthServiceImp>();
builder.Services.AddScoped<IUserRepository, UserServiceImp>();
builder.Services.AddScoped<IFileRepository, FileServiceImp>();
builder.Services.AddScoped<IChartRepository, ChartServiceImp>();

// config cors dung tren trinh duyet
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

// config authenicate
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    //options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
var app = builder.Build();
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    //RequestPath = "/"
});
app.MapControllers();

app.Run();