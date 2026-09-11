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

## Layout

| File | Role |
|------|------|
| `Dry/Dry.csproj` | net4.8 project; references publicized DLLs in `..\..\Libs`. |
| `Dry/Dry.cs` | BepInEx plugin entry; applies Harmony patches on load. |
| `Dry/Patches/BlockWetPatch.cs` | Blocks Wet for the local player via `SEMan.Internal_AddStatusEffect`. |
| `scripts/build-deploy.ps1` | Local build + copy into r2modman plugins. |
| `scripts/create-release.ps1` | Builds `Thunderstore.zip` (+ optional GitHub release). |
| `publish/` | Thunderstore package README and manifest template. |
| `resources/icon.png` | Thunderstore icon (256×256). |
