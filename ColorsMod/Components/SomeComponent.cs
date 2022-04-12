using System;
using ColorsMod.Types;
using UnhollowerBaseLib.Attributes;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace ColorsMod.Components
{
    public sealed class ColorComponent : MonoBehaviour
    {
        private static string _gameObjectName = nameof(ColorComponent) + new Guid();

        [HideFromIl2Cpp]
        public static void Register()
        {
            if (GameObject.Find(_gameObjectName)) return;

            ClassInjector.RegisterTypeInIl2Cpp<ColorComponent>();
            ClassInjector.RegisterTypeInIl2Cpp<PlayerColorComponent>();

            var epicColors = new GameObject(_gameObjectName);
            DontDestroyOnLoad(epicColors);
            epicColors.AddComponent<ColorComponent>();
        }

        public ColorComponent(IntPtr ptr) : base(ptr) { }

        public void Update()
        {
            for (var i = 0; i < Palette.PlayerColors.Length; i++)
            {
                var customColor = Manager.GetOrDefault(i);
                if (customColor is null or { Update: null }) continue;

                customColor.Update(i);
            }
        }
    }
}