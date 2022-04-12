using HarmonyLib;
using ColorsMod.Components;
using UnityEngine;

namespace ColorsMod.Patches
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.SetPlayerMaterialColors), typeof(int), typeof(Renderer))]
    public static class PlayerControlPatch
    {
        public static void Postfix(int colorId, Renderer rend)
        {
            var customColor = Manager.GetOrDefault(colorId);
            if (customColor is { Update: not null })
            {
                PlayerColorComponent.Initialize(rend.gameObject, colorId);
                return;
            }
            
            PlayerColorComponent.Clear(rend.gameObject);
        }
    }
}