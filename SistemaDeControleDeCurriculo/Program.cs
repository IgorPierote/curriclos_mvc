using CurriculoMVC.Data;
using CurriculoMVC.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CurriculoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }));

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Adicione este bloco para inicializar o banco de dados
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CurriculoContext>();
        context.Database.EnsureCreated(); // Isso garante que o banco de dados seja criado

        // Verifica se já existem currículos no banco de dadosC
        if (!context.Curriculos.Any())
        {
            // Adiciona dados de exemplo
            context.Curriculos.AddRange(
                new Curriculo
                {
                    Nome = "João Silva",
                    CPF = "123.456.789-00",
                    Endereco = "Rua A, 123",
                    Telefone = "(11) 98765-4321",
                    Email = "joao@email.com",
                    PretensaoSalarial = 5000.00M,
                    CargoPretendido = "Desenvolvedor",
                    FormacaoAcademica = "Bacharel em Ciência da Computação",
                    ExperienciasProfissionais = "2 anos como desenvolvedor júnior",
                    Idiomas = "Inglês intermediário"
                },
                new Curriculo
                {
                    Nome = "Maria Santos",
                    CPF = "987.654.321-00",
                    Endereco = "Av. B, 456",
                    Telefone = "(11) 91234-5678",
                    Email = "maria@email.com",
                    PretensaoSalarial = 6000.00M,
                    CargoPretendido = "Analista de Sistemas",
                    FormacaoAcademica = "Mestrado em Engenharia de Software",
                    ExperienciasProfissionais = "3 anos como analista de sistemas",
                    Idiomas = "Inglês fluente, Espanhol básico"
                }
            );
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao inicializar o banco de dados.");
    }
}

app.Run();