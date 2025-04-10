using Empresas.Infra.InversionOfControl.IoC;
using EmpresasFuncionarios.Infra.InversionOfControl.IoC;
using Funcionarios.Infra.InversionOfControl.IoC;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfraestructureFuncionarios(builder.Configuration);
builder.Services.AddInfraestructureEmpresas(builder.Configuration);
builder.Services.AddInfraestructureEmpresasFuncionarios(builder.Configuration);



builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Funcionarios.Application.AssemblyReferences.AssemblyReference.Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
