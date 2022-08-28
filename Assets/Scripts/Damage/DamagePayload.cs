using UnityEngine;

namespace MeltingChamber
{
    public class DamagePayload
    {
        public Transform Instigator;
        public float Knockback;
        public bool ToggleCollider;

        public DamagePayload( Transform instigator )
		{
            Instigator = instigator;
        }

        public Vector3 GetKnockbackVelocity( Transform victim )
        {
            return GetKnockbackDirection( victim ) * Knockback;
        }

        public Vector3 GetKnockbackDirection( Transform victim )
		{
            Vector3 dir = (victim.position - Instigator.position).normalized;
            return dir;
		}
    }
}
