# Dry

Client-side Valheim BepInEx/Harmony mod that blocks the **Wet** debuff on your local player.

## Requirements

- [.NET SDK](https://dotnet.microsoft.com/download) with net4.8 targeting pack
- Shared libs at `C:\Users\matth\Documents\code\Libs` (same as Server Devcommands; already publicized)
- [r2modman](https://thunderstore.io/) with a Valheim profile that has BepInEx

After a Valheim update, refresh Libs with `valheim-dev\scripts\sync-libs.ps1`.

## Build & deploy

```powershell
.\scripts\build-deploy.ps1
# or: .\scripts\build-deploy.ps1 -Profile "Default"
```

That builds `Dry.dll` and copies it to:

`%APPDATA%\r2modmanPlus-local\Valheim\profiles\<Profile>\BepInEx\plugins\MattHB1-Dry\`

Launch Valheim **through r2modman**, stand in rain/water, and confirm Wet does not appear.

## Layout (C# cheat-sheet for Python folks)

| File | Role |
|------|------|
| `Dry/Dry.csproj` | Project file — like `pyproject.toml` / deps list. Targets **net4.8**, references DLLs in `..\..\Libs`. |
| `Dry/Dry.cs` | Mod entry point (`BaseUnityPlugin`). `Awake` = constructor-ish startup; Harmony `PatchAll` applies patches. |
| `Dry/Patches/BlockWetPatch.cs` | Harmony **Prefix** on `SEMan.Internal_AddStatusEffect` — return `false` skips the game method (Wet never applied). |
| `scripts/build-deploy.ps1` | `dotnet build` + copy into r2modman plugins. |

BepInEx loads every `*.dll` under `BepInEx/plugins/`. Harmony rewrites game methods at runtime — no Valheim source needed.
