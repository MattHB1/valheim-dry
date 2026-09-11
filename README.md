# Dry

Client-side Valheim BepInEx/Harmony mod that blocks the **Wet** debuff on your local player.

**Thunderstore:** [DevDonkey-Dry](https://thunderstore.io/c/valheim/p/DevDonkey/Dry/)

## Install (players)

1. Open [r2modman](https://r2modman.com/) (or Thunderstore Mod Manager) → Valheim → your profile.
2. **Online** → search `DevDonkey-Dry` or [open the package page](https://thunderstore.io/c/valheim/p/DevDonkey/Dry/) → **Install with Mod Manager**.
3. Launch the game **through r2modman**.

Manual: download from Thunderstore and put `Dry.dll` in `BepInEx/plugins/` (needs [BepInExPack_Valheim](https://thunderstore.io/c/valheim/p/denikson/BepInExPack_Valheim/)).

## Develop

### Requirements

- [.NET SDK](https://dotnet.microsoft.com/download) with net4.8 targeting pack
- Shared libs at `C:\Users\matth\Documents\code\Libs` (same as Server Devcommands; already publicized)
- r2modman with a Valheim profile that has BepInEx

After a Valheim update, refresh Libs with `valheim-dev\scripts\sync-libs.ps1`.

### Build & local deploy

```powershell
.\scripts\build-deploy.ps1
# or: .\scripts\build-deploy.ps1 -Profile "Default"
```

Copies `Dry.dll` into your r2modman profile plugins folder. Launch Valheim **through r2modman** to test.

### Publish a new Thunderstore version

Same idea as ValheimPlus: **source in git**, ship the DLL via Thunderstore.

1. Bump `VERSION` in `Dry/Dry.cs` (and `publish/manifest.json` if you keep it in sync).
2. Run:
   ```powershell
   .\scripts\create-release.ps1
   # optional: also attach assets on GitHub
   .\scripts\create-release.ps1 -GitHubRelease
   ```
3. Upload `release/<version>/Thunderstore.zip` at [thunderstore.io/c/valheim/create/package](https://thunderstore.io/c/valheim/create/package/) (team **DevDonkey**, package name **Dry**).

Thunderstore rejects re-uploads of an existing `version_number` — always bump first.

## Layout (C# cheat-sheet for Python folks)

| File | Role |
|------|------|
| `Dry/Dry.csproj` | Project file — like `pyproject.toml` / deps list. Targets **net4.8**, references DLLs in `..\..\Libs`. |
| `Dry/Dry.cs` | Mod entry point (`BaseUnityPlugin`). `Awake` = constructor-ish startup; Harmony `PatchAll` applies patches. |
| `Dry/Patches/BlockWetPatch.cs` | Harmony **Prefix** on `SEMan.Internal_AddStatusEffect` — return `false` skips the game method (Wet never applied). |
| `scripts/build-deploy.ps1` | Local: `dotnet build` + copy into r2modman plugins. |
| `scripts/create-release.ps1` | Release: Thunderstore.zip (+ optional `gh release`). |
| `publish/` | Thunderstore package README (+ manifest template). |
| `resources/icon.png` | Thunderstore icon (256×256). |

BepInEx loads every `*.dll` under `BepInEx/plugins/`. Harmony rewrites game methods at runtime — no Valheim source needed.
