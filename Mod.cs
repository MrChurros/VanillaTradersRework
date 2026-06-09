using System.Reflection;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Routers;
using SPTarkov.Server.Core.Services;
using IOPath = System.IO.Path;

namespace VanillaTradersRework;

[Injectable(TypePriority = OnLoadOrder.TraderRegistration + 999)]
public class Mod(DatabaseService _db, ImageRouter _imageRouter) : IOnLoad
{
    private readonly string _modAvatarPath = IOPath.Combine(
        IOPath.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
        "avatars"
    );

    public Task OnLoad()
    {
        // 1. Verifica se a pasta de avatares do mod existe
        if (!Directory.Exists(_modAvatarPath))
        {
            Console.WriteLine($"[VanillaTradersRework] Pasta de avatares não encontrada: {_modAvatarPath}");
            return Task.CompletedTask;
        }

        // 2. Itera sobre cada trader registrado no banco de dados
        foreach (var (traderId, trader) in _db.GetTraders())
        {
            // 3. Pega o nome do arquivo da URL original do avatar (ex: "59b91ca086f77469a81232e4.jpg")
            var originalAvatarFileName = IOPath.GetFileName(trader.Base.Avatar);

            // 4. Constrói o caminho para a sua imagem personalizada
            var customAvatarPath = IOPath.Combine(_modAvatarPath, originalAvatarFileName);

            // 5. Se a sua imagem existir, registra a substituição
            if (File.Exists(customAvatarPath))
            {
                var originalAvatarUrlWithoutExtension = trader.Base.Avatar.Split('.').First();
                _imageRouter.AddRoute(originalAvatarUrlWithoutExtension, customAvatarPath);
                Console.WriteLine($"[VanillaTradersRework] Avatar substituído para: {trader.Base.Name}");
            }
            else
            {
                Console.WriteLine($"[VanillaTradersRework] Avatar personalizado não encontrado para: {trader.Base.Name}");
            }
        }

        Console.WriteLine("[VanillaTradersRework] Mod carregado com sucesso.");
        return Task.CompletedTask;
    }
}