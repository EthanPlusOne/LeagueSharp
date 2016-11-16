﻿using LeagueSharp;
using SharpDX;

namespace GosuMechanicsYasuo.Manager.Events
{
    using System;
    using Games.Mode;
    using LeagueSharp.Common;
    using Orbwalking = Orbwalking;

    internal class LoopManager : Logic
    {
        internal static void Init(EventArgs Args)
        {
            if (Utils.TickCount - lastECast - Game.Ping > 500)
            {
                lastEPos = Vector3.Zero;
            }

            if (Menu.Item("EnableSkin", true).GetValue<bool>())
            {
                ObjectManager.Player.SetSkin(ObjectManager.Player.ChampionName,
                    Menu.Item("SelectSkin", true).GetValue<StringList>().SelectedIndex);
            }

            if (Me.IsDead || Me.IsRecalling())
            {
                return;
            }

            KillSteal.Init();
            Auto.Init();

            switch (Orbwalker.ActiveMode)
            {
                case Orbwalking.OrbwalkingMode.Combo:
                    Combo.Init();
                    break;
                case Orbwalking.OrbwalkingMode.Mixed:
                    Harass.Init();
                    break;
                case Orbwalking.OrbwalkingMode.LaneClear:
                    LaneClear.Init();
                    JungleClear.Init();
                    break;
                case Orbwalking.OrbwalkingMode.LastHit:
                    LastHit.Init();
                    break;
                case Orbwalking.OrbwalkingMode.Flee:
                    Flee.Init();
                    break;
                case Orbwalking.OrbwalkingMode.WallJump:
                    WallJump.Init();
                    break;
                case Orbwalking.OrbwalkingMode.None:
                    if (Menu.Item("EQFlash", true).GetValue<KeyBind>().Active)
                    {
                        EQFlash.Init();
                    }
                    break;
            }
        }
    }
}