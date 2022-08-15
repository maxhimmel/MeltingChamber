using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Utility
{
    public class ForceGenerator : MonoBehaviour
    {
		[SerializeField, Min( 0 )] private float _delay = 0;

		[BoxGroup]
		[SerializeField] private bool _useRandomForce = true;
		[BoxGroup, ShowIf( "_useRandomForce" )]
		[SerializeField] private float _randomForce = 10;
		[BoxGroup, HideIf("_useRandomForce")]
		[SerializeField] private Vector2 _overrideForce = Vector2.right;

        private Rigidbody2D _body;

		[Inject]
		public void Construct( Rigidbody2D body )
		{
			_body = body;
		}

		private IEnumerator Start()
		{
			float timer = _delay;
			do
			{
				timer -= Time.fixedDeltaTime;
				yield return new WaitForFixedUpdate();
			} while ( timer > 0 );

			Vector2 force = _useRandomForce
				? Random.insideUnitCircle.normalized * _randomForce
				: _overrideForce;

			Generate( force, ForceMode2D.Impulse );
		}

		public void Generate( Vector2 force, ForceMode2D mode )
		{
			_body.AddForce( force, mode );
		}
	}
}
