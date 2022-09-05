using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using MinimalAPICatalog.Context;
using MinimalAPICatalog.Models;

var builder = WebApplication.CreateBuilder(args);

// Connect to Azure Key Vault
var keyVaultEndpoint = builder.Configuration["AzureKeyVault:Endpoint"];
var keyVaultTenantId = builder.Configuration["AzureKeyVault:TenantId"];
var keyVaultClientId = builder.Configuration["AzureKeyVault:ClientId"];
var keyVaultClientSecret = builder.Configuration["AzureKeyVault:ClientSecret"];

var credential = new ClientSecretCredential(keyVaultTenantId, keyVaultClientId, keyVaultClientSecret);

var client = new SecretClient(new Uri(keyVaultEndpoint), credential);

builder.Configuration.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());

// Connect to Azure MySQL Database
var connectionString = builder.Configuration.GetConnectionString("MinimalAPICatalog");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString)));

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// API Endpoints

// ******************* Categories *******************
app.MapPost("/categorias", async (Category category, AppDbContext dbContext) =>
{
    try
    {
        dbContext.Categories.Add(category);
        await dbContext.SaveChangesAsync();
        return Results.Created($"/categorias/{category.CategoryId}", category);
    }
    catch (Exception ex)
    {
        return Results.Problem(detail: $"Erro ao atualizar categoria: {ex.InnerException?.Message ?? ex.Message}.", statusCode: 500);
    }
});

app.MapGet("/categorias", async (AppDbContext dbContext)
    => Results.Ok(await dbContext.Categories.ToListAsync()));

app.MapGet("/categorias/{id:int}", async (int id, AppDbContext dbContext)
    => await dbContext.Categories.FindAsync(id) is Category category
                ? Results.Ok(category)
                : Results.NotFound($"Categoria código {id} não encontrada."));

app.MapPut("/categorias/{id:int}", async (int id, Category category, AppDbContext dbContext) =>
{
    if (category.CategoryId != id)
        return Results.BadRequest("Código da categoria alterada é diferente da categoria informada.");

    var categoryOnDb = await dbContext.Categories.FindAsync(id);
    if (categoryOnDb is null) return Results.NotFound($"Categoria de código {id} não encontrada.");

    categoryOnDb.Name = category.Name;
    categoryOnDb.Description = category.Description;
    await dbContext.SaveChangesAsync();
    
    return Results.Ok(categoryOnDb);
});

app.MapDelete("categorias/{id:int}", async (int id, AppDbContext dbContext) =>
{
    var categoryOnDb = await dbContext.Categories.FindAsync(id);
    if (categoryOnDb is null) return Results.NotFound($"Categoria de código {id} não encontrada");
    
    dbContext.Categories.Remove(categoryOnDb);
    await dbContext.SaveChangesAsync();

    return Results.NoContent();
});

// ******************* Products *******************
app.MapPost("/produtos", async (Product product, AppDbContext dbContext) =>
{
    try
    {
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();
        return Results.Created($"/produtos/{product.ProductId}", product);
    }
    catch (Exception ex)
    {
        return Results.Problem(detail: $"Erro ao atualizar produto: {ex.InnerException?.Message ?? ex.Message}.", statusCode: 500);
    }
});

app.MapGet("/produtos", async (AppDbContext dbContext)
    => Results.Ok(await dbContext.Products.ToListAsync()));

app.MapGet("/produtos/{id:int}", async (int id, AppDbContext dbContext)
    => await dbContext.Products.FindAsync(id) is Product product
                ? Results.Ok(product)
                : Results.NotFound($"Produto código {id} não encontrado."));

app.MapPut("/produtos/{id:int}", async (int id, Product product, AppDbContext dbContext) =>
{
    if (product.ProductId != id)
        return Results.BadRequest("Código do produto alterado é diferente da produto informado.");

    var productOnDb = await dbContext.Products.FindAsync(id);
    if (productOnDb is null) return Results.NotFound($"Produto de código {id} não encontrado.");

    productOnDb.Name = product.Name;
    productOnDb.Description = product.Description;
    productOnDb.Price = product.Price;
    productOnDb.Image = product.Image;
    productOnDb.BuyDate = product.BuyDate;
    productOnDb.InventoryQty = product.InventoryQty;
    productOnDb.CategoryId = product.CategoryId;
    await dbContext.SaveChangesAsync();

    return Results.Ok(productOnDb);
});

app.MapDelete("produtos/{id:int}", async (int id, AppDbContext dbContext) =>
{
    var productOnDb = await dbContext.Products.FindAsync(id);
    if (productOnDb is null) return Results.NotFound($"Produto de código {id} não encontrado");

    dbContext.Products.Remove(productOnDb);
    await dbContext.SaveChangesAsync();

    return Results.NoContent();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
