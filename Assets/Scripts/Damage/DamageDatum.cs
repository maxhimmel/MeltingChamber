using UnityEngine;

namespace MeltingChamber.Gameplay
{
    [System.Serializable]
    public class DamageDatum
    {
        [SerializeField] private float _knockback = 15;
        [SerializeField] private bool _toggleCollider = true;

        public DamagePayload CreatePayload( Transform instigator )
		{
            return new DamagePayload( instigator )
            {
                Knockback = _knockback,
                ToggleCollider = _toggleCollider
            };
		}
    }
}
