using BepInEx;
using HarmonyLib;

namespace Dry;

[BepInPlugin(GUID, NAME, VERSION)]
public class Dry : BaseUnityPlugin
{
  public const string GUID = "matthb1.dry";
  public const string NAME = "Dry";
  public const string VERSION = "1.0.1";

  private void Awake()
  {
    Logger.LogInfo($"{NAME} {VERSION} loaded — blocking Wet on local player.");
    new Harmony(GUID).PatchAll();
  }

  // Clears Wet already on the character (e.g. saved while wet before the mod).
  private void Update()
  {
    var player = Player.m_localPlayer;
    if (!player) return;
    var seman = player.GetSEMan();
    if (seman == null) return;
    if (seman.HaveStatusEffect(SEMan.s_statusEffectWet))
      seman.RemoveStatusEffect(SEMan.s_statusEffectWet, true);
  }
}
