using HarmonyLib;
using System.Linq;
using TMPro;
using UnityEngine;

namespace ColorsMod.Patches
{
    public static partial class PlayerTabPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(PlayerTab.Update))]
        public static void UpdatePostfix(PlayerTab __instance)
        {
            if (
                !__instance.transform.FindChild("setflag")
                && __instance.transform.FindChild("Text").gameObject.GetComponent<TextMeshPro>() is var tmp
                && tmp
            )
            {
                var noAuth = string.IsNullOrWhiteSpace(ColorsModPlugin.ConfAuthor);
                var auth = noAuth ? "SpaceColors" : $"{ColorsModPlugin.ConfAuthor} with Colors";

                tmp.text = $"SpaceColors\n<size=80%>(By Spacewreck)</size>";

                var flag = new GameObject("setflag");
                flag.transform.SetParent(__instance.transform);
            }

            var chips = __instance.ColorChips.ToArray();

            for (var i = 0; i < chips.Count; i++)
            {
                chips[i].Inner.color = Palette.PlayerColors.ElementAtOrDefault(i);
            }
        }
    }
}