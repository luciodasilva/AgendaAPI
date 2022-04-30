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


// Enpoints para clientes

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

// Enpoints para Funcionarios

app.MapGet("/funcionario", (Context context)
    => context.Funcionarios.ToList());

app.MapPost("/funcionario", async (Funcionario funcionario, Context db) =>
{
    db.Funcionarios.Add(funcionario);
    await db.SaveChangesAsync();

    return Results.Created($"/categorias/{funcionario.Id}", funcionario);
});



app.MapGet("/funcionario{id:int}", async (int id, Context db) =>
{
    return await db.Funcionarios.FindAsync(id)
                 is Funcionario funcionario
                 ? Results.Ok(funcionario)
                 : Results.NotFound();
});

app.MapPut("/funcionario{id:int}", async (int id, Funcionario funcionario, Context db) =>
{
    if (funcionario.Id != id)
    {
        return Results.BadRequest();
    }
    var funcionarioDB = await db.Funcionarios.FindAsync(id);

    if (funcionarioDB is null) return Results.NotFound();

    funcionarioDB.Nome = funcionario.Nome;
    funcionarioDB.RegistroFuncionario = funcionario.RegistroFuncionario;
    funcionarioDB.Cargo = funcionario.Cargo;
    funcionarioDB.Telefone = funcionario.Telefone;

    await db.SaveChangesAsync();

    return Results.Ok(funcionarioDB);
});



app.MapDelete("/funcionario{id:int}", async (int id, Context db) =>
{
    var funcionario = await db.Funcionarios.FindAsync(id);

    if (funcionario is null)
    {
        return Results.NotFound();
    }

    db.Funcionarios.Remove(funcionario);
    await db.SaveChangesAsync();

    return Results.NoContent();
});


// Enpoints para Horarios

app.MapGet("/horario", (Context context)
    => context.Horarios.ToList());

app.MapPost("/horario", async (Horario horario, Context db) =>
{
    db.Horarios.Add(horario);
    await db.SaveChangesAsync();

    return Results.Created($"/categorias/{horario.Id}", horario);
});



app.MapGet("/horario{id:int}", async (int id, Context db) =>
{
    return await db.Horarios.FindAsync(id)
                 is Horario horario
                 ? Results.Ok(horario)
                 : Results.NotFound();
});

app.MapPut("/horario{id:int}", async (int id, Horario horario, Context db) =>
{
    if (horario.Id != id)
    {
        return Results.BadRequest();
    }
    var horarioDB = await db.Horarios.FindAsync(id);

    if (horarioDB is null) return Results.NotFound();

    horarioDB.DataHora = horario.DataHora;

    await db.SaveChangesAsync();

    return Results.Ok(horarioDB);
});



app.MapDelete("/horario{id:int}", async (int id, Context db) =>
{
    var horario = await db.Horarios.FindAsync(id);

    if (horario is null)
    {
        return Results.NotFound();
    }

    db.Horarios.Remove(horario);
    await db.SaveChangesAsync();

    return Results.NoContent();
});


app.Run();
