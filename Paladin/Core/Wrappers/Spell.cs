#region

using System;
using Styx.WoWInternals.World;

#region

#region

using System.Collections.Generic;
using System.Linq;

#region

using System.Threading.Tasks;
using Buddy.Coroutines;
using Paladin.Core.Extensions;
using Styx;
using Styx.CommonBot;
using Styx.CommonBot.Coroutines;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;

#endregion

#endregion

#endregion

#endregion

//TODO: Add a range check

namespace Paladin.Core.Wrappers
{
    internal static class Spell
    {
        #region Cast Functions

        internal static async Task<bool> CastSpell(WoWSpell spell)
        {
            // spell not learned
            //if (!StyxWoW.Me.KnowsSpell(spell.Id))
                //return false;

            // spell not ready
            if (!SpellManager.CanCast(spell))
                return false;

            // cast fails
            if (!SpellManager.Cast(spell))
                return false;

            // hackish way to keep it async
            await Coroutine.Sleep(1);

            // cast successful
            return true;
        }

        internal static async Task<bool> CastSpell(WoWSpell spell, WoWUnit target, bool factionCheck = false)
        {
            // target is not valid
            if (!target.IsValidTarget(int.MaxValue, factionCheck))
                return false;

            // spell not learned
            //if (!StyxWoW.Me.KnowsSpell(spell.Id))
                //return false;

            // spell not ready
            if (!SpellManager.CanCast(spell))
                return false;

            // target is not in our LoS
            //if (!target.InLineOfSpellSight)
                //return false;

            // cast fails
            if (!SpellManager.Cast(spell, target))
                return false;

            // hackish way to keep it async
            await Coroutine.Sleep(1);

            // cast successful
            return true;
        }

        internal static async Task<bool> CastSpell(WoWSpell spell, WoWPoint location)
        {
            // spell not learned
            //if (!StyxWoW.Me.KnowsSpell(spell.Id))
                //return false;

            // spell not ready
            if (!SpellManager.CanCast(spell))
                return false;

            // location is empty
            if (location == WoWPoint.Empty)
                return false;

            // location is not in our LoS
            // TODO: check if this works
            //if (!GameWorld.IsInLineOfSpellSight(StyxWoW.Me.GetTraceLinePos(), location))
                //return false;

            // cast fails
            if (!SpellManager.Cast(spell))
                return false;

            // cursor animation doesn't change within 1k ms
            if (!await Coroutine.Wait(1000, () => StyxWoW.Me.CurrentPendingCursorSpell != null))
                return false;

            // click the ground location; not sure if a while(StyxWoW.Me.CurrentPendingCursorSpell != null) is better here
            SpellManager.ClickRemoteLocation(location);

            // wait our current latency
            await CommonCoroutines.SleepForLagDuration();

            // cast successful
            return true;
        }

        #endregion

        #region Generic Functions

        internal static bool IsReadyBeforeGlobalCooldownEnds(WoWSpell spell, bool calculatedCooldown = true)
        {
            return spell != null &&
                   spell.CooldownTimeLeft.Milliseconds <
                   (calculatedCooldown
                       ? (1500 - Math.Min(StyxWoW.Me.HasteModifier, 50) * 100)
                       : SpellManager.GlobalCooldownLeft.Milliseconds);
        }

        internal static bool IsReadyBeforeGlobalCooldownEnds(List<WoWSpell> spells)
        {
            return spells.All(spell => IsReadyBeforeGlobalCooldownEnds(spell));
        }

        #endregion
    }
}