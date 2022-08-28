using System.Collections;
using MeltingChamber.Gameplay.Player;
using MeltingChamber.Utility;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.LevelPieces
{
    public class Tile : MonoBehaviour
    {
		[Header( "Animation" )]
        [SerializeField] private float _dissolveDuration = 0.5f;
        [SerializeField] private float _dissolveAnimSpeed = 1;

		[Header( "Collision" )]
		[SerializeField] private LayerMask _knockbackMask = 0;
		[SerializeField] private DamageDatum _damageData = new DamageDatum();

		private IToggler _rendererToggler;
		private BoxCollider2D _collider;

		private static Collider2D[] _overlaps = new Collider2D[5];

        public void Dissolve()
		{
			StartCoroutine( UpdateDissolve() );
		}

        private IEnumerator UpdateDissolve()
		{
			bool toggle = true;
			float animTimer = 0;
			float dissolveTimer = 0;

			while ( dissolveTimer < 1 )
			{
				dissolveTimer += Time.deltaTime / _dissolveDuration;
				animTimer += Time.deltaTime / _dissolveAnimSpeed;

				if ( animTimer >= 1 )
				{
					animTimer = 0;
					toggle = !toggle;

					System.Action toggleAction = toggle 
						? _rendererToggler.Enable 
						: _rendererToggler.Disable;
					toggleAction();
				}

				yield return new WaitForFixedUpdate();
			}

			Cleanup();
		}

		private void Cleanup()
		{
			int collisionCount = EvaluateDissolveOverlaps();
			for ( int idx = 0; idx < collisionCount; ++idx )
			{
				Collider2D otherCollider = _overlaps[idx];
				Rigidbody2D body = otherCollider.attachedRigidbody;
				if ( body == null )
				{
					continue;
				}

				PlayerController player = body.GetComponent<PlayerController>();
				if ( player != null )
				{
					DamagePayload dmgPayload = _damageData.CreatePayload( transform.parent );
					player.TakeDamage( dmgPayload );
				}
			}

			Destroy( gameObject );
		}

		private int EvaluateDissolveOverlaps()
		{
			Vector2 boxSize = new Vector2()
			{
				x = _collider.size.x * transform.lossyScale.x,
				y = _collider.size.y * transform.lossyScale.y
			};
			float angle = transform.eulerAngles.z;

			return Physics2D.OverlapBoxNonAlloc( transform.position, boxSize, angle, _overlaps, _knockbackMask );
		}

		private void Awake()
		{
			_rendererToggler = GetComponentInChildren<IToggler>();
			_rendererToggler.Init();

			_collider = GetComponentInChildren<BoxCollider2D>();
		}

		public class Factory : PlaceholderFactory<Object, Tile> { }
	}
}
