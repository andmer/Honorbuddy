#region

using System;
using System.Collections.Generic;
using Styx;
using Styx.WoWInternals;

#endregion

namespace Paladin.Core
{
    internal static class Spellbook
    {
        internal static Dictionary<string, int> Auras = new Dictionary<string, int>();
        internal static Dictionary<string, uint> Glyphs = new Dictionary<string, uint>();
        internal static Dictionary<string, WoWSpell> Spells = new Dictionary<string, WoWSpell>();
        internal static Dictionary<string, Tuple<int, int>> Talents = new Dictionary<string, Tuple<int, int>>();

        static Spellbook()
        {
            switch (StyxWoW.Me.Specialization)
            {
                case WoWSpec.PaladinHoly:
                    //Spells.Add("test", WoWSpell.FromId(13));
                    break;

                case WoWSpec.PaladinProtection:
                    //Spells.Add("test", WoWSpell.FromId(13));
                    break;

                case WoWSpec.PaladinRetribution:

                    #region Auras

                    Auras.Add("AvengingWrath", 31884);
                    Auras.Add("BlazingContempt", 166831);
                    Auras.Add("CrusadersFury", 165442);
                    Auras.Add("DivineCrusader", 144595);
                    Auras.Add("DivinePurpose", 90174);
                    Auras.Add("FinalVerdict", 157048);
                    Auras.Add("MaraadsTruth", 156990);
                    Auras.Add("LiadrinsRighteousness", 156989);
                    Auras.Add("SacredShield", 20925);
                    Auras.Add("SealOfRighteousness", 20154);
                    Auras.Add("SealOfTruth", 31801);
                    Auras.Add("Seraphim", 152262);

                    #endregion

                    #region Glyphs

                    Glyphs.Add("GlyphOfMassExorcism", 1009);

                    #endregion

                    #region Spells

                    Spells.Add("AvengingWrath", WoWSpell.FromId(31884));
                    Spells.Add("BlessingOfKings", WoWSpell.FromId(20217));
                    Spells.Add("BlessingOfMight", WoWSpell.FromId(19740));
                    Spells.Add("BlindingLight", WoWSpell.FromId(115750));
                    Spells.Add("Cleanse", WoWSpell.FromId(4987));
                    Spells.Add("CrusaderStrike", WoWSpell.FromId(35395));
                    Spells.Add("DivineProtection", WoWSpell.FromId(498));
                    Spells.Add("DivineShield", WoWSpell.FromId(642));
                    Spells.Add("DivineStorm", WoWSpell.FromId(53385));
                    Spells.Add("Emancipate", WoWSpell.FromId(121783));
                    Spells.Add("EternalFlame", WoWSpell.FromId(114163));
                    Spells.Add("ExecutionSentence", WoWSpell.FromId(114157));
                    Spells.Add("Exorcism", WoWSpell.FromId(879));
                    Spells.Add("FinalVerdict", WoWSpell.FromId(157048));
                    Spells.Add("FistOfJustice", WoWSpell.FromId(105593));
                    Spells.Add("FlashOfLight", WoWSpell.FromId(19750));
                    Spells.Add("HammerOfTheRighteous", WoWSpell.FromId(53595));
                    Spells.Add("HammerOfWrath", WoWSpell.FromId(24275));
                    Spells.Add("HandOfFreedom", WoWSpell.FromId(1044));
                    Spells.Add("HandOfProtection", WoWSpell.FromId(1022));
                    Spells.Add("HandOfPurity", WoWSpell.FromId(114039));
                    Spells.Add("HandOfSacrifice", WoWSpell.FromId(6940));
                    Spells.Add("HolyAvenger", WoWSpell.FromId(105809));
                    Spells.Add("HolyPrism", WoWSpell.FromId(114165));
                    Spells.Add("Judgement", WoWSpell.FromId(20271));
                    Spells.Add("LayOnHands", WoWSpell.FromId(633));
                    Spells.Add("LightsHammer", WoWSpell.FromId(114158));
                    Spells.Add("Rebuke", WoWSpell.FromId(96231));
                    Spells.Add("Reckoning", WoWSpell.FromId(62124));
                    Spells.Add("Redemption", WoWSpell.FromId(7328));
                    Spells.Add("Repentance", WoWSpell.FromId(20066));
                    Spells.Add("RighteousFury", WoWSpell.FromId(25780));
                    Spells.Add("SacredShield", WoWSpell.FromId(20925));
                    Spells.Add("SealOfInsight", WoWSpell.FromId(20165));
                    Spells.Add("SealOfJustice", WoWSpell.FromId(20164));
                    Spells.Add("SealOfRighteousness", WoWSpell.FromId(20154));
                    Spells.Add("SealOfTruth", WoWSpell.FromId(31801));
                    Spells.Add("Seraphim", WoWSpell.FromId(152262));
                    Spells.Add("SpeedOfLight", WoWSpell.FromId(85499));
                    Spells.Add("TemplarsVerdict", WoWSpell.FromId(85256));
                    Spells.Add("TurnEvil", WoWSpell.FromId(10326));
                    Spells.Add("WordOfGlory", WoWSpell.FromId(136494));

                    #endregion

                    #region Talents

                    Talents.Add("SpeedOfLight", new Tuple<int, int>(0, 0));
                    Talents.Add("LongArmOfTheLaw", new Tuple<int, int>(0, 1));
                    Talents.Add("PursuitOfJustice", new Tuple<int, int>(0, 2));
                    Talents.Add("FistOfJustice", new Tuple<int, int>(1, 0));
                    Talents.Add("Repentance", new Tuple<int, int>(1, 1));
                    Talents.Add("BlindingLight", new Tuple<int, int>(1, 2));
                    Talents.Add("SelflessHealer", new Tuple<int, int>(2, 0));
                    Talents.Add("EternalFlame", new Tuple<int, int>(2, 1));
                    Talents.Add("SacredShield", new Tuple<int, int>(2, 2));
                    Talents.Add("HandOfPurity", new Tuple<int, int>(3, 0));
                    Talents.Add("UnbreakableSpirit", new Tuple<int, int>(3, 1));
                    Talents.Add("Clemency", new Tuple<int, int>(3, 2));
                    Talents.Add("HolyAvenger", new Tuple<int, int>(4, 0));
                    Talents.Add("SanctifiedWrath", new Tuple<int, int>(4, 1));
                    Talents.Add("DivinePurpose", new Tuple<int, int>(4, 2));
                    Talents.Add("HolyPrism", new Tuple<int, int>(5, 0));
                    Talents.Add("LightsHammer", new Tuple<int, int>(5, 1));
                    Talents.Add("ExecutionSentence", new Tuple<int, int>(5, 2));
                    Talents.Add("EmpoweredSeals", new Tuple<int, int>(6, 0));
                    Talents.Add("Seraphim", new Tuple<int, int>(6, 1));
                    Talents.Add("FinalVerdict", new Tuple<int, int>(6, 2));

                    #endregion

                    break;
            }
        }
    }
}