#region

using System.Collections.Generic;
using System.Linq;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;

#endregion

namespace Paladin.Core.Extensions
{
    internal static class Aura
    {
        internal static bool HasAura(this WoWUnit unit,
            int auraId,
            int stacks = 0,
            double timeLeft = 0,
            WoWUnit auraSource = null)
        {
            if (!unit.IsValid)
                return false;

            List<WoWAura> auralist = unit.GetAllAuras();

            return
                auralist.Any(
                    aura =>
                        (auraSource == null || !auraSource.IsValid || aura.CreatorGuid == auraSource.Guid) &&
                        aura.SpellId == auraId && aura.StackCount >= stacks &&
                        aura.TimeLeft.TotalMilliseconds > timeLeft);
        }

        internal static bool HasAura(this WoWUnit unit,
            List<int> auraIds,
            int stacks = 0,
            double timeLeft = 0,
            WoWUnit auraSource = null)
        {
            if (!unit.IsValid)
                return false;

            List<WoWAura> auralist = unit.GetAllAuras();

            return
                auralist.Any(
                    aura =>
                        aura != null &&
                        (auraSource == null || !auraSource.IsValid || aura.CreatorGuid == auraSource.Guid) &&
                        auraIds.Contains(aura.SpellId) && aura.StackCount >= stacks &&
                        aura.TimeLeft.TotalMilliseconds > timeLeft);
        }

        internal static bool HasAura(this WoWUnit unit,
            List<string> auraNames,
            int stacks = 0,
            double timeLeft = 0,
            WoWUnit auraSource = null)
        {
            if (!unit.IsValid)
                return false;

            List<WoWAura> auralist = unit.GetAllAuras();
            var hashes = new HashSet<string>(auraNames);

            return
                auralist.Any(
                    aura =>
                        aura != null &&
                        (auraSource == null || !auraSource.IsValid || aura.CreatorGuid == auraSource.Guid) &&
                        hashes.Contains(aura.Name) && aura.StackCount >= stacks &&
                        aura.TimeLeft.TotalMilliseconds > timeLeft);
        }

        internal static bool HasAuraOfType(this WoWUnit unit, params WoWSpellMechanic[] mechanics)
        {
            if (!unit.IsValid)
                return false;

            var hashes = new HashSet<WoWSpellMechanic>(mechanics);
            List<WoWAura> auralist = unit.GetAllAuras();

            return auralist.Any(aura => aura != null && aura.Spell != null && hashes.Contains(aura.Spell.Mechanic));
        }

        internal static bool HasAuraOfType(this WoWUnit unit, WoWApplyAuraType applyType)
        {
            if (!unit.IsValid)
                return false;

            List<WoWAura> auralist = unit.GetAllAuras();

            return
                auralist.Any(
                    aura =>
                        aura.Spell != null &&
                        aura.Spell.SpellEffects.Any(spellEffect => applyType == spellEffect.AuraType));
        }

        public static bool HasAuraOfType(this WoWUnit unit, params WoWApplyAuraType[] applyType)
        {
            if (!unit.IsValid)
                return false;

            List<WoWAura> auralist = unit.GetAllAuras();
            var hashes = new HashSet<WoWApplyAuraType>(applyType);

            return
                auralist.Any(
                    aura =>
                        aura.Spell != null &&
                        aura.Spell.SpellEffects.Any(spellEffect => hashes.Contains(spellEffect.AuraType)));
        }
    }
}