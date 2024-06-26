using Microsoft.AspNetCore.OData;
using TestODataWithEf.DotNet6;
using TestODataWithEf.DotNet6.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddOData(cfg =>
{
    cfg.Select().Filter().OrderBy();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FakeContext>();

builder.Services.Configure<MySqlContextConfig>(
    builder.Configuration.GetSection(MySqlContextConfig.SECTION));

var app = builder.Build();

await FakeContext.PopulateAsync(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
