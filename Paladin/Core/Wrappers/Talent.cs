#region

using System;
using System.Collections.Generic;
using System.Linq;
using Styx;
using Styx.CommonBot.CharacterManagement;
using Styx.WoWInternals;

#endregion

namespace Paladin.Core.Wrappers
{
    internal static class Talent
    {
        private static TalentPlacementSet _learnedTalents;

        static Talent()
        {
            UpdateTalents();

            #region Lua Events

            Lua.Events.AttachEvent("ACTIVE_TALENT_GROUP_CHANGED", UpdateTalents);
            Lua.Events.AttachEvent("CHARACTER_POINTS_CHANGED", UpdateTalents);
            Lua.Events.AttachEvent("GLYPH_UPDATED", UpdateTalents);
            Lua.Events.AttachEvent("LEARNED_SPELL_IN_TAB", UpdateTalents);
            Lua.Events.AttachEvent("PLAYER_LEVEL_UP", UpdateTalents);
            Lua.Events.AttachEvent("PLAYER_SPECIALIZATION_CHANGED", UpdateTalents);

            #endregion
        }

        private static void UpdateTalents(object sender = null, LuaEventArgs args = null)
        {
            _learnedTalents = StyxWoW.Me.GetLearnedTalents();
        }

        internal static string GetTierTalent(int tier)
        {
            return
                Spellbook.Talents.Where(
                    talent =>
                        talent.Value.Item1 == tier &&
                        IsAvailable(new Tuple<int, int>(talent.Value.Item1, talent.Value.Item2)))
                    .Select(talent => talent.Key)
                    .FirstOrDefault();
        }

        internal static bool IsAvailable(Tuple<int, int> talentPair)
        {
            return _learnedTalents.Any(talent => talent.Tier == talentPair.Item1 && talent.Index == talentPair.Item2);
        }

        internal static bool IsAvailable(List<Tuple<int, int>> talentPairs)
        {
            return talentPairs.Any(IsAvailable);
        }

        internal static bool IsAvailable(string name)
        {
            return
                _learnedTalents.Any(
                    talent =>
                        Spellbook.Talents[name] != null && talent.Tier == Spellbook.Talents[name].Item1 &&
                        talent.Index == Spellbook.Talents[name].Item2);
        }

        internal static bool IsAvailable(List<string> names)
        {
            return (from talent in _learnedTalents
                from name in names
                where
                    Spellbook.Talents[name] != null && talent.Tier == Spellbook.Talents[name].Item1 &&
                    talent.Index == Spellbook.Talents[name].Item2
                select talent).Any();
        }
    }
}