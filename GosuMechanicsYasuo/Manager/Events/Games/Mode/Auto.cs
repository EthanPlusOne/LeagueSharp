﻿namespace GosuMechanicsYasuo.Manager.Events.Games.Mode
{
    using System.Collections.Generic;
    using System.Linq;
    using Spells;
    using LeagueSharp;
    using LeagueSharp.Common;
    using Orbwalking = Orbwalking;

    internal class Auto : Logic
    {
        internal static void Init()
        {
            if (Menu.Item("AutoR", true).GetValue<bool>() && R.IsReady())
            {
                var enemiesKnockedUp =
                    HeroManager.Enemies.Where(x => x.IsValidTarget(R.Range))
                        .Where(x => x.HasBuffOfType(BuffType.Knockup) || x.HasBuffOfType(BuffType.Knockback));

                var enemies = enemiesKnockedUp as IList<Obj_AI_Hero> ?? enemiesKnockedUp.ToList();

                if (enemies.Count >= Menu.Item("AutoRCount", true).GetValue<Slider>().Value &&
                    Me.HealthPercent >= Menu.Item("AutoRMyHp", true).GetValue<Slider>().Value &&
                    Me.CountEnemiesInRange(1500) <= Menu.Item("AutoRRangeCount", true).GetValue<Slider>().Value)
                {
                    R.Cast();
                }
            }

            if (Menu.Item("AutoQ", true).GetValue<KeyBind>().Active)
            {
                if (Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.Combo &&
                    Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.LaneClear &&
                    Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.Flee &&
                    Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.WallJump &&
                    Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.LastHit)
                {
                    if (Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Mixed &&
                        Me.CountEnemiesInRange(Q.Range) > 0)
                    {
                        return;
                    }

                    if (Menu.Item("AutoQ3", true).GetValue<bool>() && Q3.IsReady() && SpellManager.HaveQ3)
                    {
                        SpellManager.CastQ3();
                    }
                    else if (!SpellManager.HaveQ3 && Q.IsReady())
                    {
                        var target = TargetSelector.GetTarget(Q.Range, TargetSelector.DamageType.Physical);

                        if (target.IsValidTarget(Q.Range))
                        {
                            var qPred = Q.GetPrediction(target, true);

                            if (qPred.Hitchance >= HitChance.VeryHigh)
                            {
                                Q.Cast(qPred.CastPosition, true);
                            }
                        }
                        else
                        {
                            var minions = MinionManager.GetMinions(Me.Position, Q.Range, MinionTypes.All,
                                MinionTeam.NotAlly);

                            if (minions.Any())
                            {
                                var qFarm =
                                    MinionManager.GetBestLineFarmLocation(
                                        minions.Select(x => x.Position.To2D()).ToList(), Q.Width, Q.Range);

                                if (qFarm.MinionsHit >= 1)
                                {
                                    Q.Cast(qFarm.Position, true);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
