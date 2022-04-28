using AgendaAPI.DataContext;
using AgendaAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/cliente", (Context context)
    => context.Clientes.ToList());

app.MapPost("/cliente", async (Cliente cliente, Context db) =>
{
    db.Clientes.Add(cliente);
    await db.SaveChangesAsync();

    return Results.Created($"/categorias/{cliente.Id}", cliente);
});

app.MapGet("/cliente{id:int}", async(int id, Context db) =>
{
    return await db.Clientes.FindAsync(id)
                 is Cliente cliente
                 ? Results.Ok(cliente)
                 :Results.NotFound();
});

app.MapPut("/cliente{id:int}", async (int id, Cliente cliente, Context db) =>
{
    if (cliente.Id != id)
    {
        return Results.BadRequest();
    }
    var clienteDB = await db.Clientes.FindAsync(id);

    if (clienteDB is null) return Results.NotFound();

    clienteDB.Nome = cliente.Nome;
    clienteDB.Sexo = cliente.Sexo;
    clienteDB.Telefone = cliente.Telefone;
    clienteDB.DataNascimento = cliente.DataNascimento;

    await db.SaveChangesAsync();

    return Results.Ok(clienteDB);
});

app.MapDelete("/cliente{id:int}", async (int id, Context db) =>
{
    var cliente = await db.Clientes.FindAsync(id);

    if (cliente is null)
    {
        return Results.NotFound();
    }

    db.Clientes.Remove(cliente);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();
