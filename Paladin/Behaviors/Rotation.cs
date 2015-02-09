#region

using System.Threading.Tasks;

#endregion

namespace Paladin.Behaviors
{
    internal class Rotation
    {
        internal virtual async Task<bool> CombatBehavior()
        {
            return false;
        }

        internal virtual async Task<bool> CombatBuffBehavior()
        {
            return false;
        }

        internal virtual async Task<bool> PreCombatBuffBehavior()
        {
            return false;
        }

        internal virtual async Task<bool> HealBehavior()
        {
            return false;
        }
    }
}