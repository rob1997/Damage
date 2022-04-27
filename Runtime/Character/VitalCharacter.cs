using UnityEngine;

namespace Damage.Character
{
    public abstract class VitalCharacter : Core.Character.Character
    {
        [Space] 
    
        [SerializeField] private Damagable damagable;
    
        public Damager Damager { get; } = new Damager();
    
        public Damagable Damagable => damagable;
    }
}
