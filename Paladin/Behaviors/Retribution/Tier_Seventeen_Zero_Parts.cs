#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Paladin.Core;
using Paladin.Core.Extensions;
using Paladin.Core.Wrappers;
using Styx;
using Styx.Common;
using Styx.CommonBot;
using Styx.WoWInternals;

#endregion

namespace Paladin.Behaviors.Retribution
{
    internal class TierSeventeenZeroParts : Rotation
    {
        #region Behavior Overrides

        internal override async Task<bool> CombatBehavior()
        {
            if (SpellManager.GlobalCooldown || !StyxWoW.Me.Combat || StyxWoW.Me.IsCasting)
                return true;

            if (StyxWoW.Me.CountEnemiesInRange(8) >= 4)
                return await MultiTargetBehavior();

            return await SingleTargetBehavior();
        }

        internal override async Task<bool> PreCombatBuffBehavior()
        {
            if (SpellManager.GlobalCooldown || !StyxWoW.Me.IsAlive || StyxWoW.Me.IsCasting || StyxWoW.Me.IsOnTransport ||
                StyxWoW.Me.Mounted)
                return true;

            return await Generic.SacredShield();
        }

        #endregion

        #region Behavior Selectors

        internal async Task<bool> MultiTargetBehavior()
        {
            switch (Talent.GetTierTalent(6))
            {
                case "EmpoweredSeals":
                    return await MultiTargetBehaviorEmpoweredSeals();

                case "Seraphim":
                    return await MultiTargetBehaviorSeraphim();

                case "FinalVerdict":
                    return await MultiTargetBehaviorFinalVerdict();

                default:
                    return false;
            }
        }

        internal async Task<bool> SingleTargetBehavior()
        {
            switch (Talent.GetTierTalent(6))
            {
                case "EmpoweredSeals":
                    return await SingleTargetBehaviorEmpoweredSeals();

                case "Seraphim":
                    return await SingleTargetBehaviorSeraphim();

                case "FinalVerdict":
                    return await SingleTargetBehaviorFinalVerdict();

                default:
                    return false;
            }
        }

        #endregion

        #region Rotations

        #region Multi Target Behavior

        internal static async Task<bool> MultiTargetBehaviorEmpoweredSeals()
        {
            if (await Generic.ChangeSeal(true))
                return true;

            if (await Generic.DivineStorm(true, false))
                return true;

            if (await Generic.DivineStorm(false, false, 5))
                return true;

            if (await Generic.TierSixTalent())
                return true;

            if (await Generic.TierFiveTalent())
                return true;

            if (await Generic.HammerOfTheRighteous())
                return true;

            if (await Generic.Exorcism(true))
                return true;

            if (await Generic.HammerOfWrath())
                return true;

            if (await Generic.Judgement())
                return true;

            if (await Generic.DivineStorm(false, false, 3))
                return true;

            return
                await
                    Generic.SacredShield(
                        new List<WoWSpell>
                        {
                            Spellbook.Spells["ExecutionSentence"],
                            Spellbook.Spells["Exorcism"],
                            Spellbook.Spells["HammerOfTheRighteous"],
                            Spellbook.Spells["HammerOfWrath"],
                            Spellbook.Spells["HolyPrism"],
                            Spellbook.Spells["Judgement"],
                            Spellbook.Spells["LightsHammer"]
                        });
        }

        internal static async Task<bool> MultiTargetBehaviorSeraphim()
        {
            if (await Generic.ChangeSeal(true))
                return true;

            if (await Generic.Seraphim())
                return true;

            if (await Generic.DivineStorm(true, false))
                return true;

            if (await Generic.DivineStorm(false, false, 5))
                return true;

            if (await Generic.TierSixTalent())
                return true;

            if (await Generic.TierFiveTalent())
                return true;

            if (await Generic.HammerOfTheRighteous())
                return true;

            if (await Generic.Exorcism(true))
                return true;

            if (await Generic.HammerOfWrath())
                return true;

            if (await Generic.Judgement())
                return true;

            if (await Generic.DivineStorm(false, false, 3))
                return true;

            return
                await
                    Generic.SacredShield(
                        new List<WoWSpell>
                        {
                            Spellbook.Spells["ExecutionSentence"],
                            Spellbook.Spells["Exorcism"],
                            Spellbook.Spells["HammerOfTheRighteous"],
                            Spellbook.Spells["HammerOfWrath"],
                            Spellbook.Spells["HolyPrism"],
                            Spellbook.Spells["Judgement"],
                            Spellbook.Spells["LightsHammer"],
                            Spellbook.Spells["Seraphim"]
                        });
        }

        internal static async Task<bool> MultiTargetBehaviorFinalVerdict()
        {
            if (await Generic.ChangeSeal(true))
                return true;

            if (await Generic.DivineStorm(false))
                return true;

            if (await Generic.FinalVerdict())
                return true;

            if (await Generic.TierSixTalent())
                return true;

            if (await Generic.TierFiveTalent())
                return true;

            if (await Generic.HammerOfTheRighteous())
                return true;

            if (await Generic.Exorcism(true))
                return true;

            if (await Generic.HammerOfWrath())
                return true;

            if (await Generic.Judgement())
                return true;

            if (await Generic.FinalVerdict(3))
                return true;

            return
                await
                    Generic.SacredShield(
                        new List<WoWSpell>
                        {
                            Spellbook.Spells["ExecutionSentence"],
                            Spellbook.Spells["Exorcism"],
                            Spellbook.Spells["HammerOfTheRighteous"],
                            Spellbook.Spells["HammerOfWrath"],
                            Spellbook.Spells["HolyPrism"],
                            Spellbook.Spells["Judgement"],
                            Spellbook.Spells["LightsHammer"]
                        });
        }

        #endregion

        #region Single Target Behavior

        internal static async Task<bool> SingleTargetBehaviorEmpoweredSeals()
        {
            if (await Generic.ChangeSeal())
                return true;

            if (await Generic.TemplarsVerdict())
                return true;

            if (await Generic.TierSixTalent())
                return true;

            if (await Generic.TierFiveTalent())
                return true;

            if (await Generic.HammerOfWrath())
                return true;

            if (await Generic.CrusaderStrike())
                return true;

            if (await Generic.Judgement())
                return true;

            if (await Generic.Exorcism())
                return true;

            if (await Generic.DivineStorm(true, false))
                return true;

            if (await Generic.TemplarsVerdict(3))
                return true;

            return
                await
                    Generic.SacredShield(
                        new List<WoWSpell>
                        {
                            Spellbook.Spells["CrusaderStrike"],
                            Spellbook.Spells["ExecutionSentence"],
                            Spellbook.Spells["Exorcism"],
                            Spellbook.Spells["HammerOfWrath"],
                            Spellbook.Spells["HolyPrism"],
                            Spellbook.Spells["Judgement"],
                            Spellbook.Spells["LightsHammer"]
                        });
        }

        internal static async Task<bool> SingleTargetBehaviorSeraphim()
        {
            if (await Generic.ChangeSeal())
                return true;

            if (await Generic.Seraphim())
                return true;

            if (await Generic.TemplarsVerdict())
                return true;

            if (await Generic.TierSixTalent())
                return true;

            if (await Generic.TierFiveTalent())
                return true;

            if (await Generic.HammerOfWrath())
                return true;

            if (await Generic.CrusaderStrike())
                return true;

            if (await Generic.Judgement())
                return true;

            if (await Generic.Exorcism())
                return true;

            if (await Generic.DivineStorm(true, false))
                return true;

            if (await Generic.TemplarsVerdict(3))
                return true;

            return
                await
                    Generic.SacredShield(
                        new List<WoWSpell>
                        {
                            Spellbook.Spells["CrusaderStrike"],
                            Spellbook.Spells["ExecutionSentence"],
                            Spellbook.Spells["Exorcism"],
                            Spellbook.Spells["HammerOfWrath"],
                            Spellbook.Spells["HolyPrism"],
                            Spellbook.Spells["Judgement"],
                            Spellbook.Spells["LightsHammer"],
                            Spellbook.Spells["Seraphim"]
                        });
        }

        internal static async Task<bool> SingleTargetBehaviorFinalVerdict()
        {
            if (await Generic.ChangeSeal())
                return true;

            if (await Generic.DivineStorm())
                return true;

            if (await Generic.FinalVerdict())
                return true;

            if (await Generic.TierSixTalent())
                return true;

            if (await Generic.TierFiveTalent())
                return true;

            if (await Generic.HammerOfWrath())
                return true;

            if (await Generic.CrusaderStrike())
                return true;

            if (await Generic.Judgement())
                return true;

            if (await Generic.Exorcism())
                return true;

            if (await Generic.FinalVerdict(3))
                return true;

            return
                await
                    Generic.SacredShield(
                        new List<WoWSpell>
                        {
                            Spellbook.Spells["CrusaderStrike"],
                            Spellbook.Spells["ExecutionSentence"],
                            Spellbook.Spells["Exorcism"],
                            Spellbook.Spells["HammerOfWrath"],
                            Spellbook.Spells["HolyPrism"],
                            Spellbook.Spells["Judgement"],
                            Spellbook.Spells["LightsHammer"]
                        });
        }

        #endregion

        #endregion
    }
}