#region

using System.Linq;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;

#endregion

namespace Paladin.Core.Extensions
{
    internal static class Unit
    {
        #region Unit Stats

        internal static float Spellpower
        {
            get
            {
                return
                    Lua.GetReturnVal<float>(
                        "return math.max(GetSpellBonusDamage(1),GetSpellBonusDamage(2),GetSpellBonusDamage(3),GetSpellBonusDamage(4),GetSpellBonusDamage(5),GetSpellBonusDamage(6),GetSpellBonusDamage(7))",
                        0);
            }
        }

        #endregion

        #region Unit States

        internal static bool IsCharmed(this WoWUnit unit)
        {
            return unit.HasAuraOfType(WoWSpellMechanic.Asleep, WoWSpellMechanic.Charmed, WoWSpellMechanic.Sapped);
        }

        internal static bool IsControllable(this WoWUnit unit)
        {
            return unit.HasAuraOfType(
                WoWSpellMechanic.Asleep, WoWSpellMechanic.Banished, WoWSpellMechanic.Charmed, WoWSpellMechanic.Fleeing,
                WoWSpellMechanic.Horrified, WoWSpellMechanic.Incapacitated, WoWSpellMechanic.Interrupted,
                WoWSpellMechanic.Polymorphed, WoWSpellMechanic.Sapped, WoWSpellMechanic.Silenced,
                WoWSpellMechanic.Stunned);
        }

        internal static bool IsCrowdControlled(this WoWUnit unit)
        {
            return unit.HasAuraOfType(
                WoWSpellMechanic.Asleep, WoWSpellMechanic.Banished, WoWSpellMechanic.Charmed,
                WoWSpellMechanic.Polymorphed, WoWSpellMechanic.Sapped);
        }

        internal static bool IsDisarmed(this WoWUnit unit)
        {
            return unit.HasAuraOfType(WoWApplyAuraType.ModDisarm);
        }

        internal static bool IsEnraged(this WoWUnit unit)
        {
            return unit.HasAuraOfType(WoWSpellMechanic.Enraged);
        }

        internal static bool IsFeared(this WoWUnit unit)
        {
            return unit.HasAuraOfType(WoWApplyAuraType.ModFear);
        }

        internal static bool IsInvulnerable(this WoWUnit unit)
        {
            return unit.HasAuraOfType(WoWSpellMechanic.Invulnerable, WoWSpellMechanic.Invulnerable2);
        }

        internal static bool IsPolymorphed(this WoWUnit unit)
        {
            return unit.HasAuraOfType(WoWSpellMechanic.Polymorphed);
        }

        internal static bool IsRooted(this WoWUnit unit)
        {
            return unit.Rooted || unit.HasAuraOfType(WoWApplyAuraType.ModRoot);
        }

        internal static bool IsSilenced(this WoWUnit unit)
        {
            return unit.Silenced || unit.HasAuraOfType(WoWApplyAuraType.ModSilence, WoWApplyAuraType.ModPacifySilence);
        }

        internal static bool IsSlowed(this WoWUnit unit)
        {
            return unit.HasAuraOfType(WoWApplyAuraType.ModDecreaseSpeed);
        }

        internal static bool IsSnared(this WoWUnit unit)
        {
            return unit.HasAuraOfType(WoWSpellMechanic.Dazed, WoWSpellMechanic.Slowed, WoWSpellMechanic.Snared);
        }

        internal static bool IsStunned(this WoWUnit unit)
        {
            return unit.Stunned || unit.HasAuraOfType(WoWApplyAuraType.ModStun);
        }

        #endregion

        #region Generic Unit Functions

        internal static int CountEnemiesInRange(this WoWUnit unit, int range)
        {
            return ObjectManager.GetObjectsOfTypeFast<WoWUnit>().Count(obj => obj.IsValidTarget(range, true));
        }

        internal static bool IsAlly(this WoWUnit unit)
        {
            return !unit.IsEnemy();
        }

        internal static bool IsEnemy(this WoWUnit unit)
        {
            return !unit.IsFriendly; // TODO: improve this
        }

        internal static bool IsValid(this WoWUnit unit)
        {
            return unit != null && unit.IsValid;
        }

        internal static bool IsValidTarget(this WoWUnit unit, int range = int.MaxValue, bool factionCheck = false)
        {
            return unit.IsValid() && unit.IsAlive && unit.CanSelect && !unit.IsNonCombatPet &&
                   (range == int.MaxValue || unit.DistanceSqr <= range * range) && (!factionCheck || unit.IsEnemy());
        }

        #endregion
    }
}