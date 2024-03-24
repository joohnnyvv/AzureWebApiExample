using Cdv.Api.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("PeopleDb");

builder.Services.AddDbContext<PeopleDb>( options =>
{
        options.UseSqlServer(connectionString);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/people", async (PeopleDb db) =>
{
    var people = await db.People.ToListAsync();
    return Results.Ok(people);
});

app.Run();
