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
		[SerializeField] private float _anticDuration = 1;
		[SerializeField] private float _anticBlinkRate = 0.065f;
		[Space]
		[SerializeField] private float _dissolveDuration = 0.5f;
		[Space]
		[SerializeField] private Shader _dissolveShader;
		[SerializeField] private string _dissolveParamName = "_DissolveAmount";

		[Header( "Collision" )]
		[SerializeField] private LayerMask _knockbackMask = 0;
		[SerializeField] private DamageDatum _damageData = new DamageDatum();

		private static Collider2D[] _overlaps = new Collider2D[5];

		private IToggler _rendererToggler;
		private SpriteRenderer _renderer;
		private BoxCollider2D _collider;

        public void Dissolve()
		{
			StartCoroutine( UpdateDissolve() );
		}

        private IEnumerator UpdateDissolve()
		{
			yield return PlayWarningAntic();
			yield return PlayDissolveAntic();

			Cleanup();
		}

		private IEnumerator PlayWarningAntic()
		{
			bool toggle = true;
			float blinkTimer = 0;
			float warningAnticTimer = 0;
			while ( warningAnticTimer < 1 )
			{
				warningAnticTimer += Time.deltaTime / _anticDuration;
				blinkTimer += Time.deltaTime / _anticBlinkRate;

				if ( blinkTimer >= 1 )
				{
					blinkTimer = 0;
					toggle = !toggle;

					System.Action toggleAction = toggle
						? _rendererToggler.Enable
						: _rendererToggler.Disable;
					toggleAction();
				}

				yield return new WaitForFixedUpdate();
			}
		}

		private IEnumerator PlayDissolveAntic()
		{
			_renderer.material = new Material( _dissolveShader );

			float dissolveTimer = 0;
			while ( dissolveTimer < 1 )
			{
				dissolveTimer += Time.deltaTime / _dissolveDuration;
				_renderer.material.SetFloat( _dissolveParamName, dissolveTimer );

				yield return new WaitForFixedUpdate();
			}
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
			_renderer = GetComponentInChildren<SpriteRenderer>();

			_rendererToggler = GetComponentInChildren<IToggler>();
			_rendererToggler.Init();

			_collider = GetComponentInChildren<BoxCollider2D>();
		}

		public class Factory : PlaceholderFactory<Object, Tile> { }
	}
}
