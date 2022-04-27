using System.Collections.Generic;
using Core.Character;
using Damage.Character;
using Locomotion.Controllers;
using UnityEngine;

namespace Damage.Controllers
{
    public abstract class DamageController : Controller
    {
        [SerializeField] protected bool takeFallDamage;
    
        [Tooltip("A velocity threshold if exceeded during falling will damage the character on landing")]
        [SerializeField] protected float fallDamageVelocityThreshold = 10f;
    
        [Tooltip("How much damage character takes based on velocity units exceeding fallDamageVelocityThreshold")]
        [SerializeField] protected float fallDamagePerUnit = 2f;

        [Space]
    
        private MotionController _motionController;
    
        public override void Initialize(Core.Character.Character character)
        {
            base.Initialize(character);

            if (!(character is VitalCharacter vitalCharacter))
            {
                Debug.LogError("Damage can't be applied, Character needs to be a Vital Character");
            
                return;
            }
            
            #region Fall Damage

            character.GetController(out _motionController);
        
            _motionController.OnGroundStateChange += grounded =>
            {
                if (grounded && takeFallDamage)
                {
                    float landingVelocity =  - _motionController.GetVelocity().y;
                            
                    if (landingVelocity > fallDamageVelocityThreshold)
                    {
                        Damage fallDamage = new Damage
                        (new Dictionary<Damage.DamageType, float>
                        {
                            //Square fall damage so it increases exponentially
                            {Damage.DamageType.Fall, Mathf.Pow((landingVelocity - fallDamageVelocityThreshold) * fallDamagePerUnit, 2)}
                        }, vitalCharacter.Damagable);
                                
                        vitalCharacter.Damager.DealDamage(fallDamage);
                    }
                }
            };

            #endregion
        }
    }
}
