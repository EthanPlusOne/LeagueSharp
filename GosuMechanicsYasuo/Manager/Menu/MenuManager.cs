﻿namespace GosuMechanicsYasuo.Manager.Menu
{
    using Evade;
    using System.Linq;
    using LeagueSharp;
    using LeagueSharp.Common;
    using Orbwalking = Orbwalking;

    internal class MenuManager : Logic
    {
        internal static void Init()
        {
            Menu = new Menu("GosuMechanics Yasuo Rework", "GosuMechanics Yasuo Rework", true);

            var orbMenu = Menu.AddSubMenu(new Menu("Orbwalker", "Orbwalker"));
            {
                Orbwalker = new Orbwalking.Orbwalker(orbMenu);
            }

            var comboMenu = Menu.AddSubMenu(new Menu("Combo", "combo"));
            {
                comboMenu.AddItem(new MenuItem("ComboQ", "Use Q", true).SetValue(true));
                comboMenu.AddItem(new MenuItem("ComboW", "Use W", true).SetValue(false));
                comboMenu.AddItem(new MenuItem("ComboE", "Use E", true).SetValue(true));
                comboMenu.AddItem(new MenuItem("ComboEWall", "Use E to Wall Position", true).SetValue(false));
                comboMenu.AddItem(
                    new MenuItem("ComboERange", "Use E| Target Distance to Player >= x", true).SetValue(
                        new Slider(375, 0, 475)));
                comboMenu.AddItem(
                    new MenuItem("ComboEGap", "Use E GapCloser| Target Distance to Player >=x", true).SetValue(
                        new Slider(230, 0, 1300)));
                comboMenu.AddItem(
                    new MenuItem("ComboEMode", "Use E Mode: ", true).SetValue(new StringList(new[] {"Target", "Mouse"})));
                comboMenu.AddItem(new MenuItem("ComboEQ", "Use EQ", true).SetValue(true));
                comboMenu.AddItem(new MenuItem("ComboEQ3", "Use EQ3", true).SetValue(true));
                comboMenu.AddItem(
                    new MenuItem("ComboR", "Use R", true).SetValue(new KeyBind('R', KeyBindType.Toggle, true)));
                comboMenu.AddItem(
                    new MenuItem("ComboRHp", "Use R|When target HealthPercent <= x%", true).SetValue(new Slider(50)));
                comboMenu.AddItem(
                    new MenuItem("ComboRCount", "Use R|When knockedUp enemy Count >= x", true).SetValue(
                        new Slider(2, 1, 5)));
                comboMenu.AddItem(
                    new MenuItem("ComboRAlly", "Use R| When Have Ally In Range", true).SetValue(true));
                comboMenu.AddItem(new MenuItem("ComboIgnite", "Use Ignite", true).SetValue(true));
                comboMenu.AddItem(new MenuItem("ComboItems", "Use Items", true).SetValue(true));
            }

            var harassMenu = Menu.AddSubMenu(new Menu("Harass", "Harass"));
            {
                harassMenu.AddItem(new MenuItem("HarassQ", "Use Q", true).SetValue(true));
                harassMenu.AddItem(new MenuItem("HarassQ3", "Use Q3", true).SetValue(true));
                harassMenu.AddItem(new MenuItem("HarassTower", "Under Tower", true).SetValue(true));
            }

            var laneClearMenu = Menu.AddSubMenu(new Menu("LaneClear", "LaneClear"));
            {
                laneClearMenu.AddItem(new MenuItem("LaneClearQ", "Use Q", true).SetValue(true));
                laneClearMenu.AddItem(new MenuItem("LaneClearQ3", "Use Q3", true).SetValue(true));
                laneClearMenu.AddItem(
                    new MenuItem("LaneClearQ3count", "Use Q3| Hit Minions >= x", true).SetValue(new Slider(3, 1, 5)));
                laneClearMenu.AddItem(new MenuItem("LaneClearE", "Use E", true).SetValue(true));
                laneClearMenu.AddItem(new MenuItem("LaneClearETurret", "Use E Under Turret", true).SetValue(false));
                laneClearMenu.AddItem(new MenuItem("LaneClearItems", "Use Items", true).SetValue(true));
            }

            var jungleClearMenu = Menu.AddSubMenu(new Menu("JungleClear", "JungleClear"));
            {
                jungleClearMenu.AddItem(new MenuItem("JungleClearQ", "Use Q", true).SetValue(true));
                jungleClearMenu.AddItem(new MenuItem("JungleClearQ3", "Use Q3", true).SetValue(true));
                jungleClearMenu.AddItem(new MenuItem("JungleClearE", "Use E", true).SetValue(true));
            }

            var lastHitMenu = Menu.AddSubMenu(new Menu("LastHit", "LastHit"));
            {
                lastHitMenu.AddItem(new MenuItem("LastHitQ", "Use Q", true).SetValue(true));
                lastHitMenu.AddItem(new MenuItem("LastHitQ3", "Use Q3", true).SetValue(true));
                lastHitMenu.AddItem(new MenuItem("LastHitE", "Use E", true).SetValue(true));
            }

            var fleeMenu = Menu.AddSubMenu(new Menu("Flee", "Flee"));
            {
                fleeMenu.AddItem(new MenuItem("FleeQ", "Use Q", true).SetValue(true));
                fleeMenu.AddItem(new MenuItem("FleeE", "Use E", true).SetValue(true));
            }

            var miscMenu = Menu.AddSubMenu(new Menu("Misc", "Misc"));
            {
                var qMenu = miscMenu.AddSubMenu(new Menu("Q Settings", "Q Settings"));
                {
                    qMenu.AddItem(new MenuItem("KillStealQ", "Use Q KillSteal", true).SetValue(true));
                    qMenu.AddItem(new MenuItem("KillStealQ3", "Use Q3 KillSteal", true).SetValue(true));
                    qMenu.AddItem(new MenuItem("Q3Int", "Use Q3 Interrupter", true).SetValue(true));
                    qMenu.AddItem(new MenuItem("Q3Anti", "Use Q3 AntiGapcloser", true).SetValue(true));
                    qMenu.AddItem(
                        new MenuItem("AutoQ", "Auto Q", true).SetValue(new KeyBind('T', KeyBindType.Toggle, true)));
                    qMenu.AddItem(
                        new MenuItem("AutoQ3", "Auto Q3", true).SetValue(false));
                }

                var wMenu = miscMenu.AddSubMenu(new Menu("W Settings", "W Settings"));
                {
                    var wWhitelistMenu = wMenu.AddSubMenu(new Menu("Combo W Target", "Combo W Target"));
                    {
                        foreach (var hero in HeroManager.Enemies)
                        {
                            wWhitelistMenu.AddItem(
                                new MenuItem("ComboW" + hero.ChampionName.ToLower(), hero.ChampionName, true).SetValue(true));
                        }
                    }
                }

                var eMenu = miscMenu.AddSubMenu(new Menu("E Settings", "E Settings"));
                {
                    eMenu.AddItem(new MenuItem("KillStealE", "Use E KillSteal", true).SetValue(true));
                    eMenu.AddItem(new MenuItem("ETower", "Dont Jump turrets", true).SetValue(true));
                }

                var rMenu = miscMenu.AddSubMenu(new Menu("R Settings", "R Settings"));
                {
                    var rWhitelist = rMenu.AddSubMenu(new Menu("R Whitelist", "R Whitelist"));
                    {
                        foreach (var hero in HeroManager.Enemies)
                        {
                            rWhitelist.AddItem(
                                new MenuItem("R" + hero.ChampionName.ToLower(), hero.ChampionName, true).SetValue(true));
                        }
                    }

                    var autoR = rMenu.AddSubMenu(new Menu("Auto R", "Auto R"));
                    {
                        autoR.AddItem(new MenuItem("AutoR", "Auto R", true)).SetValue(true);
                        autoR.AddItem(
                            new MenuItem("AutoRCount", "Auto R|When knockedUp enemy Count >= x", true).SetValue(
                                new Slider(3, 1, 5)));
                        autoR.AddItem(
                            new MenuItem("AutoRRangeCount", "Auto R|When all Enemy Count >= x", true).SetValue(
                                new Slider(2, 1, 5)));
                        autoR.AddItem(
                            new MenuItem("AutoRMyHp", "Auto R|When Player HealthPercent >= x%", true).SetValue(
                                new Slider(50)));
                    }
                }

                var evadeSettings = miscMenu.AddSubMenu(new Menu("Evade Settings", "Evade Settings"));
                {
                    var evadespellSettings = evadeSettings.AddSubMenu(new Menu("Dodge Spells", "Dodge Spells"));
                    {
                        var evadeSpells = evadespellSettings.AddSubMenu(new Menu("Evade spells", "evadeSpells"));
                        {
                            foreach (var spell in EvadeSpellDatabase.Spells)
                            {
                                var subMenu = evadeSpells.AddSubMenu(new Menu("Yasuo " + spell.Slot, spell.Name));
                                {
                                    subMenu.AddItem(
                                        new MenuItem("DangerLevel" + spell.Name, "Danger level", true).SetValue(
                                            new Slider(spell.DangerLevel, 5, 1)));

                                    if (spell.Slot == SpellSlot.E)
                                    {
                                        subMenu.AddItem(new MenuItem("ETower", "Under Tower", true).SetValue(false));
                                    }

                                    subMenu.AddItem(new MenuItem("Enabled" + spell.Name, "Enabled", true).SetValue(true));
                                }
                            }
                        }

                        var skillShotMenu = evadespellSettings.AddSubMenu(new Menu("Skillshots", "Skillshots"));
                        {
                            foreach (
                                var hero in
                                HeroManager.Enemies.Where(
                                    i => SpellDatabase.Spells.Any(a => a.ChampionName == i.ChampionName)))
                            {
                                skillShotMenu.AddSubMenu(new Menu(hero.ChampionName, "Evade" + hero.ChampionName.ToLower()));
                            }

                            foreach (
                                var spell in
                                SpellDatabase.Spells.Where(
                                    i => HeroManager.Enemies.Any(a => a.ChampionName == i.ChampionName)))
                            {
                                var subMenu =
                                    skillShotMenu.SubMenu("Evade" + spell.ChampionName.ToLower())
                                        .AddSubMenu(new Menu(spell.SpellName + " " + spell.Slot,
                                            "EvadeSpell" + spell.MenuItemName));
                                {
                                    subMenu.AddItem(
                                        new MenuItem("DangerLevel" + spell.MenuItemName, "Danger Level", true).SetValue(
                                            new Slider(spell.DangerValue, 1, 5)));
                                    subMenu.AddItem(
                                        new MenuItem("Enabled" + spell.MenuItemName, "Enabled", true).SetValue(
                                            !spell.DisabledByDefault));
                                }
                            }
                        }
                    }

                    var evadeMenu = evadeSettings.AddSubMenu(new Menu("Evade Target", "EvadeTarget"));
                    {
                        evadeMenu.AddItem(new MenuItem("EvadeTargetW", "Use W", true).SetValue(true));
                        evadeMenu.AddItem(new MenuItem("EvadeTargetE", "Use E (To Dash Behind WindWall)", true).SetValue(true));
                        evadeMenu.AddItem(new MenuItem("EvadeTargetETower", "-> Under Tower", true).SetValue(false));
                        evadeMenu.AddItem(new MenuItem("BAttack", "Basic Attack", true).SetValue(true));
                        evadeMenu.AddItem(new MenuItem("BAttackHpU", "-> If Hp <", true).SetValue(new Slider(35)));
                        evadeMenu.AddItem(new MenuItem("CAttack", "Crit Attack", true).SetValue(true));
                        evadeMenu.AddItem(new MenuItem("CAttackHpU", "-> If Hp <", true).SetValue(new Slider(40)));

                        foreach (var hero in
                            HeroManager.Enemies.Where(i => EvadeTargetManager.Spells.Any(a => a.ChampionName == i.ChampionName)))
                        {
                            evadeMenu.AddSubMenu(new Menu("-> " + hero.ChampionName, "ET_" + hero.ChampionName));
                        }

                        foreach (
                            var spell in
                            EvadeTargetManager.Spells.Where(
                                i => HeroManager.Enemies.Any(a => a.ChampionName == i.ChampionName)))
                        {
                            evadeMenu.SubMenu("ET_" + spell.ChampionName)
                                .AddItem(
                                    new MenuItem(spell.MissileName, spell.MissileName + " (" + spell.Slot + ")", true)
                                        .SetValue(false));
                        }
                    }
                }

                var skinMenu = miscMenu.AddSubMenu(new Menu("SkinChange", "SkinChange"));
                {
                    skinMenu.AddItem(new MenuItem("EnableSkin", "Enabled", true).SetValue(false)).ValueChanged += EnbaleSkin;
                    skinMenu.AddItem(
                        new MenuItem("SelectSkin", "Select Skin: ", true).SetValue(
                            new StringList(new[]
                            {
                                "Classic", "High Noon", "Project: Yasuo", "Blood Moon", "Others", "Others1", "Others2",
                                "Others3", "Others4"
                            })));
                }

                miscMenu.AddItem(new MenuItem("EQFlash", "EQFlash", true).SetValue(new KeyBind('A', KeyBindType.Press)));

            }

            var drawMenu = Menu.AddSubMenu(new Menu("Draw", "Draw"));
            {
                drawMenu.AddItem(new MenuItem("DrawQ", "Draw Q Range", true).SetValue(true));
                drawMenu.AddItem(new MenuItem("DrawQ3", "Draw Q3 Range", true).SetValue(true));
                drawMenu.AddItem(new MenuItem("DrawW", "Draw W Range", true).SetValue(true));
                drawMenu.AddItem(new MenuItem("DrawE", "Draw E Range", true).SetValue(true));
                drawMenu.AddItem(new MenuItem("DrawR", "Draw R Range", true).SetValue(true));
                drawMenu.AddItem(new MenuItem("DrawSpots", "Draw WallJump Spots", true).SetValue(true));
                drawMenu.AddItem(new MenuItem("DrawAutoQ", "Draw Auto Q Status", true).SetValue(true));
                drawMenu.AddItem(new MenuItem("DrawRStatus", "Draw Combo R Status", true).SetValue(true));
            }

            Menu.AddItem(new MenuItem("Credit", "Credit: tulisan69", true));
            Menu.AddItem(new MenuItem("Rework", "Rework: NightMoon", true));

            Menu.AddToMainMenu();
        }
    }
}