# Dry

Client-side Valheim mod that blocks the **Wet** debuff on your local player.

**Thunderstore:** [DevDonkey-Dry](https://thunderstore.io/c/valheim/p/DevDonkey/Dry/)

## Install

1. Open [r2modman](https://r2modman.com/) (or Thunderstore Mod Manager) → Valheim → your profile.
2. **Online** → search `DevDonkey-Dry`, or install from the [package page](https://thunderstore.io/c/valheim/p/DevDonkey/Dry/).
3. Launch the game through the mod manager.

Manual install: put `Dry.dll` in `BepInEx/plugins/` (requires [BepInExPack_Valheim](https://thunderstore.io/c/valheim/p/denikson/BepInExPack_Valheim/)).

## Notes

- Client-side only — other players still get Wet unless they install it too.
- Works with rain and water; does not change other environmental status effects.

## Build

Requires a .NET SDK with the net4.8 targeting pack, plus publicized Valheim / BepInEx reference assemblies (see HintPaths in `Dry/Dry.csproj`).

```powershell
.\scripts\build-deploy.ps1
.\scripts\create-release.ps1
```

`create-release.ps1` writes `release/<version>/Thunderstore.zip` for upload. Bump `VERSION` in `Dry/Dry.cs` before each Thunderstore publish.
