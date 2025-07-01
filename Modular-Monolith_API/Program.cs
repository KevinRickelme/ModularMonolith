using Common.Infra.IoC;
using Empresas.Infra.InversionOfControl.IoC;
using EmpresasFuncionarios.Infra.InversionOfControl.IoC;
using Funcionarios.Infra.InversionOfControl.IoC;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.UseIISIntegration();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfraestructureFuncionarios(builder.Configuration);
builder.Services.AddInfraestructureEmpresas(builder.Configuration);
builder.Services.AddInfraestructureEmpresasFuncionarios(builder.Configuration);
builder.Services.AddInfraestructureCommon(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost8080",
        policy =>
        {
            policy.WithOrigins("http://localhost:8080")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Funcionarios.Application.AssemblyReferences.AssemblyReference.Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowLocalhost8080");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
