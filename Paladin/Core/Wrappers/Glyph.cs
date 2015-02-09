#region

using System.Collections.Generic;
using System.Linq;
using Styx;
using Styx.WoWInternals;

#endregion

namespace Paladin.Core.Wrappers
{
    internal static class Glyph
    {
        private static readonly List<uint> ActiveGlyphs = new List<uint>();

        static Glyph()
        {
            UpdateGlyphs();

            #region Lua Events

            Lua.Events.AttachEvent("ACTIVE_TALENT_GROUP_CHANGED", UpdateGlyphs);
            Lua.Events.AttachEvent("GLYPH_UPDATED", UpdateGlyphs);
            Lua.Events.AttachEvent("PLAYER_SPECIALIZATION_CHANGED", UpdateGlyphs);

            #endregion
        }

        private static void UpdateGlyphs(object sender = null, LuaEventArgs args = null)
        {
            ActiveGlyphs.Clear();

            foreach (var glyph in StyxWoW.Me.Glyphs.Where(glyph => glyph.Enabled))
            {
                ActiveGlyphs.Add(glyph.ItemId);
            }
        }

        internal static bool IsAvailable(uint id)
        {
            return ActiveGlyphs.Contains(id);
        }

        internal static bool IsAvailable(List<uint> ids)
        {
            return ids.Any(IsAvailable);
        }
    }
}