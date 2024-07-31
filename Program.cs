using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_API_MIN.Data;
using WEB_API_MIN.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnetion");
builder.Services.AddDbContext<DatosBD>(options =>
        options.UseNpgsql(connectionString));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/posts/create", async (Informacion e, DatosBD db) =>
{
    db.Informaciones.Add(e);
    await db.SaveChangesAsync();
    return Results.Created($"/Informaciones/{e.Id}", e);
});


app.MapGet("/posts/{Nombre}", async (string Nombre, DatosBD db) =>
{
    var informacion = await db.Informaciones
                              .Where(i => i.Nombre == Nombre)
                              .SingleOrDefaultAsync();

    return informacion != null
        ? Results.Ok(informacion)
        : Results.NotFound();
});


app.MapGet("/posts/all", async (DatosBD db) => await db.Informaciones.ToListAsync());

app.MapPut("/posts/{Id}", async (string Id, Informacion e, DatosBD db) =>
{
    if (e.Id != Id)
        return Results.BadRequest();

    var Informacion = await db.Informaciones.FindAsync(Id);
    if (Informacion is null) return Results.NotFound();

    Informacion.Id = e.Id;
    Informacion.Nombre = e.Nombre;
    Informacion.Descripcion = e.Descripcion;

    await db.SaveChangesAsync();
    return Results.Ok(Informacion);


});

app.MapDelete("/posts/{Id}", async (string Id, DatosBD db) =>
{
    var informacion = await db.Informaciones.FindAsync(Id);
    if (informacion is null) return Results.NotFound();
    db.Informaciones.Remove(informacion);
    await db.SaveChangesAsync();
    return Results.NoContent();
});




app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}