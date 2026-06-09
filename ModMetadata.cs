using SPTarkov.Server.Core.Models.Spt.Mod;

namespace VanillaTradersRework;

public record ModMetadata : AbstractModMetadata
{
    public override string ModGuid { get; init; } =
        "com.ricardoschurhaus.vanillatradersrework";

    public override string Name { get; init; } =
        "Vanilla Traders Rework";

    public override string Author { get; init; } =
        "Ricardo Schurhaus";

    public override SemanticVersioning.Version Version { get; init; } =
        new(1, 0, 1);

    public override string Url { get; init; } = "";

    public override string License { get; init; } = "MIT";

    public override SemanticVersioning.Range SptVersion { get; init; } =
        new("~4.0.0");

    public override List<string> Contributors { get; init; } = new();

    public override List<string> Incompatibilities { get; init; } = new();

    public override Dictionary<string, SemanticVersioning.Range> ModDependencies { get; init; } = new();

    public override bool? IsBundleMod { get; init; } = false;
}