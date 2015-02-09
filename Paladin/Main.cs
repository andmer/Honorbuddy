#region

using System.Collections.Generic;
using CommonBehaviors.Actions;
using Paladin.Behaviors;
using Paladin.Behaviors.Retribution;
using Paladin.Core.Wrappers;
using Styx;
using Styx.Common;
using Styx.CommonBot.Routines;
using Styx.TreeSharp;

#endregion

namespace Paladin
{
    public class Paladin : CombatRoutine
    {
        public override WoWClass Class
        {
            get { return WoWClass.Paladin; }
        }

        public override string Name
        {
            get { return "Paladin# by Legacy3"; }
        }

        public override void Initialize()
        {
            var welcome = new List<string>
            {
                "----------------------------------------------------",
                "Welcome to my little CR!",
                "It supports Ret atm, Holy will follow tho",
                "I am recoding the Rotation atm, it's alraedy decent tho :^",
                "No Empowered Seals yet sorry!",
                "The glyph api is broken atm, no mass exorcism due to that",
                "The new rotation will also contain a cleave one",
                "Use wings on your own!",
                "If you use seraphim it will wait for wings if their cd is < 15 seconds",
                "Leave any feedback in the thread, remember this is an alpha!",
                "No fancy debug prints yet sorry :^(",
                "----------------------------------------------------"
            };

            foreach (var msg in welcome)
            {
                Logging.Write(msg);
            }
        }

        #region Behavior Overrides

        private static Rotation Rotation
        {
            get
            {
                switch (Inventory.CountEquippedItems(new List<uint> { 115565, 115566, 15567, 15568, 15569 }))
                {
                    case 2:
                    case 3:
                        return new TierSeventeenZeroParts();

                    case 4:
                    case 5:
                        return new TierSeventeenZeroParts();

                    default:
                        return new TierSeventeenZeroParts();
                }
            }
        }

        public override Composite CombatBehavior
        {
            get { return new ActionRunCoroutine(ctx => Rotation.CombatBehavior()); }
        }

        public override Composite CombatBuffBehavior
        {
            get { return new ActionRunCoroutine(ctx => Rotation.CombatBuffBehavior()); }
        }

        public override Composite HealBehavior
        {
            get { return new ActionRunCoroutine(ctx => Rotation.HealBehavior()); }
        }

        public override Composite PreCombatBuffBehavior
        {
            get { return new ActionRunCoroutine(ctx => Rotation.PreCombatBuffBehavior()); }
        }

        #endregion
    }
}