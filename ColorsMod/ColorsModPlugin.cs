using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using ColorsMod.Components;
using ColorsMod.Types;
using System.Linq;
using UnityEngine;

namespace ColorsMod
{
    [BepInProcess("Among Us.exe")]
    [BepInAutoPlugin("some.colors")]
    public sealed partial class ColorsModPlugin : BasePlugin
    {
        public static ColorsModPlugin? Instance { get; private set; }

        public static void DisableSomeColors() => IncludeColors = false;

        public static bool IncludeColors { get; set; } = true;
        public static bool LoadDefaultColors { get; set; } = true;
        public static string? ConfAuthor { get; set; } = null;

        public Harmony Harmony { get; } = new(Id);

        public override void Load()
        {
            Instance ??= this;
            LoadDefaultColors = true;
            Manager.LoadSkidColors();
            Harmony.PatchAll();

            AwakeHooks.OnAwake += () =>
            {
                ModManager.Instance.ShowModStamp();
                ColorComponent.Register();
                InitColors();
            };
        }

        private static void InitColors()
        {
            if (IncludeColors)
            {
                Manager.RegisterColors(new IBaseColor[] {
                    new Static(new Color32(76, 47, 39, 255), "Acajou"),
                    new Static(new Color32(0, 72, 186, 255), "AbsoluteZero"),
                    new Static(new Color32(201, 255, 229, 255), "AeroBlue"),
                    new Static(new Color32(178, 132, 190, 255), "AfricanViolet"),
                    new Static(new Color32(93, 138, 168, 255), "AirForceBlueRAF"),
                    new Static(new Color32(0, 48, 143, 255), "AirForceBlueUSAF"),
                    new Static(new Color32(114, 160, 193, 255), "AirSuperiorityBlue"),
                    new Static(new Color32(175, 48, 42, 255), "AlabamaCrimson"),
                    new Static(new Color32(242, 240, 230, 255), "Alabaster"),
                    new Static(new Color32(240, 248, 255, 255), "AliceBlue"),
                    new Static(new Color32(132, 222, 2, 255), "AlienArmpit"),
                    new Static(new Color32(227, 38, 54, 255), "AlizarinCrimson"),
                    new Static(new Color32(196, 98, 16, 255), "AlloyOrange"),
                    new Static(new Color32(239, 222, 205, 255), "Almond"),
                    new Static(new Color32(229, 43, 80, 255), "Amaranth"),
                    new Static(new Color32(159, 43, 104, 255), "AmaranthDeepPurple"),
                    new Static(new Color32(241, 156, 187, 255), "AmaranthPink"),
                    new Static(new Color32(171, 39, 79, 255), "AmaranthPurple"),
                    new Static(new Color32(211, 33, 45, 255), "AmaranthRed"),
                    new Static(new Color32(59, 122, 87, 255), "Amazon"),
                    new Static(new Color32(0, 196, 176, 255), "Amazonite"),
                    new Static(new Color32(255, 191, 0, 255), "Amber"),
                    new Static(new Color32(255, 126, 0, 255), "AmberSAEECE"),
                    new Static(new Color32(59, 59, 109, 255), "AmericanBlue"),
                    new Static(new Color32(128, 64, 64, 255), "AmericanBrown"),
                    new Static(new Color32(211, 175, 55, 255), "AmericanGold"),
                    new Static(new Color32(52, 179, 52, 255), "AmericanGreen"),
                    new Static(new Color32(255, 139, 0, 255), "AmericanOrange"),
                    new Static(new Color32(255, 152, 153, 255), "AmericanPink"),
                    new Static(new Color32(67, 28, 83, 255), "AmericanPurple"),
                    new Static(new Color32(176, 191, 26, 255), "AcidGreen"),
                    new Static(new Color32(124, 185, 232, 255), "Aero"),
                    new Static(new Color32(115, 134, 120, 255), "Xanadu "),
                    new Static(new Color32(15, 77, 146, 255), "YaleBlue"),
                    new Static(new Color32(28, 40, 65, 255), "YankeesBlue"),
                    new Static(new Color32(252, 232, 131, 255), "YellowCrayola"),
                    new Static(new Color32(239, 204, 0, 255), "YellowMunsell"),
                    new Static(new Color32(254, 223, 0, 255), "YellowPantone "),
                    new Static(new Color32(254, 254, 51, 255), "YellowRYB"),
                    new Static(new Color32(154, 205, 50, 255), "YellowGreen"),
                    new Static(new Color32(255, 174, 66, 255), "YellowOrange"),
                    new Static(new Color32(255, 240, 0, 255), "YellowRose"),
                    new Static(new Color32(0, 20, 168, 255), "Zaffre"),
                    new Static(new Color32(57, 167, 142, 255), "Zomp"),
                    new Static(new Color32(214, 186, 0, 255), "Gold"),
                    new Static(new Color32(154, 140, 61, 255), "Olive"),
                    new Static(new Color32(22, 132, 176, 255), "Turquoise"),
                    new Hue(1f, 1f, 5f, "Rainbow")
                    }
                );
            }

            if (Manager.ConfColors.Any())
            {
                Manager.RegisterColors(Manager.ConfColors);
            }

            if (!LoadDefaultColors && (Manager.ConfColors.Any() || IncludeColors))
            {
                Palette.PlayerColors = Palette.PlayerColors.Skip(Manager.OriginalCount).ToArray();
                Palette.ShadowColors = Palette.ShadowColors.Skip(Manager.OriginalCount).ToArray();
            }

        }
    }
}