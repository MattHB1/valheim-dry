using HarmonyLib;

namespace Dry;

/// <summary>
/// Reject Wet at the shared SEMan add path so rain/swim never apply the debuff
/// to the local player. Other players / NPCs are unchanged.
///
/// Important: do not touch SEMan.m_character — it looks public in the publicized
/// compile Libs but is private in the real game DLL (FieldAccessException → soft-lock).
/// </summary>
[HarmonyPatch(typeof(SEMan), nameof(SEMan.Internal_AddStatusEffect))]
internal static class BlockWetPatch
{
  private static bool Prefix(SEMan __instance, int nameHash)
  {
    if (nameHash != SEMan.s_statusEffectWet) return true;

    var local = Player.m_localPlayer;
    if (!local) return true;
    if (__instance != local.GetSEMan()) return true;

    return false; // skip original → Wet is never added
  }
}
