using AutoMapper;
using CloudSaaSCodingTask.Core.Repositories;
using CloudSaaSCodingTask.Infrastructure.Data;
using CloudSaaSCodingTask.Infrastructure.Repositories;
using CloudSaaSCodingTask.Web.Helpers;
using CloudSaaSCodingTask.Web.Mapping;
using CloudSaaSCodingTask.Web.Seed;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args);
var dbType = builder.Configuration["DbType"].ToString();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();

builder.Services.AddCustomDbContext(builder.Configuration)
    .AddAutoMapper();

builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(EFGenericRepository<>));
builder.Services.AddScoped(sp =>
{
    var client = new HttpClient();
    client.BaseAddress = new Uri(builder.Configuration["APIBaseAddress"].ToString());
    return client;
});

builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(typeof(Program).Assembly);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if(dbType != "0")
    {
        dataContext.Database.EnsureCreated();
    }

    dataContext.Seed();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API V1");
    });
}

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features
        .Get<IExceptionHandlerPathFeature>()
        .Error;
    var response = new { error = exception.Message, statusCode = context.Response.StatusCode };
    await context.Response.WriteAsJsonAsync(response);
}));

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
