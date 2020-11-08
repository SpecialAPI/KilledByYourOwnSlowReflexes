using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MonoMod.RuntimeDetour;
using UnityEngine;

namespace KilledByYourOwnSlowReflexes
{
    public class KilledByYourOwnSlowReflexesModule : ETGModule
    {
        public override void Init()
        {
        }

        public override void Start()
        {
            Hook h = new Hook(
                typeof(HealthHaver).GetMethod("ApplyDamageDirectional", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(KilledByYourOwnSlowReflexesModule).GetMethod("DamageHook")
            );
        }

        public static void DamageHook(Action<HealthHaver, float, Vector2, string, CoreDamageTypes, DamageCategory, bool, PixelCollider, bool> orig, HealthHaver hh, float damage, Vector2 direction, string damageSource, CoreDamageTypes damageTypes, 
            DamageCategory damageCategory, bool ignoreInvulnerabilityFrames, PixelCollider hitPixelCollider, bool ignoreDamageCaps)
        {
            damageSource = string.Empty;
            orig(hh, damage, direction, damageSource, damageTypes, damageCategory, ignoreInvulnerabilityFrames, hitPixelCollider, ignoreDamageCaps);
            hh.lastIncurredDamageSource = string.Empty;
        }

        public override void Exit()
        {
        }

        public delegate void Action<T, T2, T3, T4, T5, T6, T7, T8, T9>(T arg, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9);
    }
}
