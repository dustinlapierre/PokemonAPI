using Microsoft.EntityFrameworkCore;
using PokemonAPI.Data;

var builder = WebApplication.CreateBuilder(args);

//Add DB context to DI using the connection string stored in User Secrets
builder.Services.AddDbContext<AppDbContext>
    (opt => opt.UseSqlServer(builder.Configuration["SQLDbConnection"]));

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("api/v1/pokemon", async (AppDbContext context) =>
{
    var pokemon = await context.Pokemon.ToListAsync();

    return Results.Ok(pokemon);
});

app.Run();