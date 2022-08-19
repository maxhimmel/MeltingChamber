using System.Collections;
using MeltingChamber.Gameplay.Movement;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.Player
{
    public class DamageHandler : MonoBehaviour
    {
        public bool IsStunned => _damageRoutine != null;

        [SerializeField] private float _knockbackForce = 5;
        [SerializeField] private float _duration = 0.25f;

		private Rigidbody2D _body;
        private Coroutine _damageRoutine = null;

		[Inject]
		public void Construct( Rigidbody2D body )
		{
            _body = body;
		}

        public void TakeDamage( Transform dmgSource )
		{
            if ( IsStunned )
			{
                return;
			}

            _damageRoutine = StartCoroutine( HandleDamage( dmgSource ) );
		}

        private IEnumerator HandleDamage( Transform dmgSource )
		{
            yield return new WaitForFixedUpdate();

            Vector2 knockbackDir = (transform.position - dmgSource.position).normalized;
            _body.velocity = knockbackDir * _knockbackForce;

            float timer = 0;
            while ( timer < 1 )
			{
                timer += Time.fixedDeltaTime / _duration;
                yield return new WaitForFixedUpdate();
            }

            _damageRoutine = null;
		}
    }
}
