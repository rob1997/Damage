using System;
using System.Collections.Generic;

namespace Damage
{
    [Serializable]
    public class Damage
    {
        //!!!INDEXES MUST BE UNIQUE
        //!!!NEVER CHANGE THE INDEX NUMBERS CAUSES A SHUFFLE ISSUE ON CUSTOM EDITOR
        public enum DamageType
        {
            Fall = 0,
            Cold = 1,
            Fire = 2,
            Magic = 3,
            Poison = 4
        }

        /// <summary>
        /// Damage type and Raw damage amount sent
        /// </summary>
        public Dictionary<DamageType, float> Hits { get; set; }
    
        public Damager Damager { get; set; }

        public Damagable Damagable
        {
            get => _damagable;

            private set
            {
                _damagable = value;

                CalculateDamageDealt();
            }
        }

        private Damagable _damagable;
    
        public float DamageDealt { get; private set; }

        public Damage(Dictionary<DamageType, float> hits, Damagable damagable)
        {
            Hits = hits;
            Damagable = damagable;
        }
    
        private void CalculateDamageDealt()
        {
            foreach (KeyValuePair<DamageType, float> hit in Hits)
            {
                Damagable.Resistance resistance = _damagable.resistance[hit.Key];

                if (!resistance.invulnerable) DamageDealt += hit.Value - (resistance.value * hit.Value);
            }
        }
    }
}