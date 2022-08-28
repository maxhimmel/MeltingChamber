using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay
{
    public class DamageHandler : MonoBehaviour
    {
        public bool IsStunned => _damageRoutine != null;

        [SerializeField] private float _duration = 0.25f;

		private Rigidbody2D _body;
        private Coroutine _damageRoutine = null;

		[Inject]
		public void Construct( Rigidbody2D body )
		{
            _body = body;
		}

        public async Task TakeDamage( DamagePayload payload )
		{
            if ( IsStunned )
			{
                return;
			}

            _damageRoutine = StartCoroutine( HandleDamage( payload ) );

            while ( IsStunned )
			{
                await Task.Yield();
			}
		}

        private IEnumerator HandleDamage( DamagePayload payload )
		{
            yield return new WaitForFixedUpdate();

            _body.velocity = payload.GetKnockbackVelocity( transform );

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
