#region

using System.Collections.Generic;
using System.Threading.Tasks;
using Paladin.Core;
using Paladin.Core.Extensions;
using Paladin.Core.Wrappers;
using Styx;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using CW = Paladin.Core.Wrappers.Spell;

#endregion

namespace Paladin.Behaviors.Retribution
{
    internal static class Generic
    {
        #region Generic Vars

        private static WoWUnit CurrentTarget
        {
            get { return StyxWoW.Me.CurrentTarget; }
        }

        private static WoWUnit Player
        {
            get { return StyxWoW.Me; }
        }

        #endregion

        #region Spells

        internal static async Task<bool> ChangeSeal(bool multiTargetBehavior = false)
        {
            if (multiTargetBehavior)
            {
                return !Player.HasAura(Spellbook.Auras["SealOfRighteousness"]) &&
                       await CW.CastSpell(Spellbook.Spells["SealOfRighteousness"]);
            }

            return !Player.HasAura(Spellbook.Auras["SealOfTruth"]) &&
                   await CW.CastSpell(Spellbook.Spells["SealOfTruth"]);
        }

        internal static async Task<bool> CrusaderStrike()
        {
            return await CW.CastSpell(Spellbook.Spells["CrusaderStrike"], CurrentTarget);
        }

        internal static async Task<bool> DivineStorm(bool divineCrusaderBuff = true,
            bool finalVerdictBuff = true,
            int holyPower = 0)
        {
            if ((divineCrusaderBuff && !Player.HasAura(Spellbook.Auras["DivineCrusader"])) ||
                (finalVerdictBuff && !Player.HasAura(Spellbook.Auras["FinalVerdict"])) ||
                Player.CurrentHolyPower < holyPower)
                return false;

            return await CW.CastSpell(Spellbook.Spells["DivineStorm"]);
        }

        internal static async Task<bool> ExecutionSentence()
        {
            return await CW.CastSpell(Spellbook.Spells["ExecutionSentence"], CurrentTarget, true);
        }

        internal static async Task<bool> Exorcism(bool multiTargetBehavior = false)
        {
            if (multiTargetBehavior && !Glyph.IsAvailable(Spellbook.Glyphs["GlyphOfMassExorcism"]))
                return false;

            return await CW.CastSpell(Spellbook.Spells["Exorcism"], CurrentTarget);
        }

        internal static async Task<bool> FinalVerdict(int holyPower = 5)
        {
            if (Player.CurrentHolyPower < holyPower)
                return false;

            return await CW.CastSpell(Spellbook.Spells["FinalVerdict"], CurrentTarget);
        }

        internal static async Task<bool> HammerOfTheRighteous()
        {
            return await CW.CastSpell(Spellbook.Spells["HammerOfTheRighteous"], CurrentTarget);
        }

        internal static async Task<bool> HammerOfWrath()
        {
            if (CurrentTarget.HealthPercent > 35 &&
                !Player.HasAura(new List<int> { Spellbook.Auras["AvengingWrath"], Spellbook.Auras["CrusadersFury"] }))
                return false;

            return await CW.CastSpell(Spellbook.Spells["HammerOfWrath"], CurrentTarget);
        }

        internal static async Task<bool> HolyAvenger(bool avengingWrathBuff = true, int holyPowerLimit = 2)
        {
            if (Talent.GetTierTalent(4) != "HolyAvenger" ||
                (avengingWrathBuff && !Player.HasAura(Spellbook.Auras["AvengingWrath"])) ||
                (!Player.HasAura(Spellbook.Auras["Seraphim"]) && Player.CurrentHolyPower > holyPowerLimit))
                return false;

            return await CW.CastSpell(Spellbook.Spells["HolyAvenger"]);
        }

        internal static async Task<bool> HolyPrism()
        {
            return await CW.CastSpell(Spellbook.Spells["HolyPrism"], CurrentTarget);
        }

        internal static async Task<bool> Judgement()
        {
            return await CW.CastSpell(Spellbook.Spells["Judgement"], CurrentTarget);
        }

        internal static async Task<bool> LightsHammer() // TODO: Add a MEC
        {
            return await CW.CastSpell(Spellbook.Spells["LightsHammer"], CurrentTarget.Location);
        }

        internal static async Task<bool> SacredShield(List<WoWSpell> spellsToCastFirst = null)
        {
            if (Player.HasAura(Spellbook.Auras["SacredShield"]) ||
                (spellsToCastFirst == null || !CW.IsReadyBeforeGlobalCooldownEnds(spellsToCastFirst)))
                return false;

            return await CW.CastSpell(Spellbook.Spells["SacredShield"], Player);
        }

        internal static async Task<bool> Seraphim(bool avengingWrathBuff = true)
        {
            if (avengingWrathBuff && Spellbook.Spells["AvengingWrath"].CooldownTimeLeft.Seconds < 15)
                return false;

            return await CW.CastSpell(Spellbook.Spells["Seraphim"]);
        }

        internal static async Task<bool> TemplarsVerdict(int holyPower = 5)
        {
            if (Player.CurrentHolyPower < holyPower)
                return false;

            return await CW.CastSpell(Spellbook.Spells["TemplarsVerdict"], CurrentTarget);
        }

        internal static async Task<bool> TierFiveTalent(bool multiTargetBehavior = false)
        {
            switch (Talent.GetTierTalent(4))
            {
                case "HolyAvenger":
                    return await HolyAvenger();

                case "DivinePurpose":
                    if (multiTargetBehavior)
                        return await DivineStorm(false, false);

                    return Talent.GetTierTalent(6) == "FinalVerdict" ? await FinalVerdict(0) : await TemplarsVerdict(0);

                default:
                    return false;
            }
        }

        internal static async Task<bool> TierSixTalent()
        {
            switch (Talent.GetTierTalent(5))
            {
                case "HolyPrism":
                    return await HolyPrism();

                case "LightsHammer":
                    return await LightsHammer();

                case "ExecutionSentence":
                    return await ExecutionSentence();

                default:
                    return false;
            }
        }

        #endregion
    }
}