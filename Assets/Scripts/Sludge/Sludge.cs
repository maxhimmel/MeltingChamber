using System.Collections;
using System.Collections.Generic;
using MeltingChamber.Gameplay.Player;
using UnityEngine;

namespace MeltingChamber.Gameplay.LevelPieces
{
    public class Sludge : MonoBehaviour
    {
        [SerializeField] private float _lifetime = 2.5f;
		[SerializeField] private DamageDatum _damageData = new DamageDatum();

		[Header( "Animations" )]
		[SerializeField] private float _sludgeAnimEndTime = 2.9f;
		[SerializeField] private Transform _vfxContainer;

		private List<ParticleSystem> _vfx;

        private void Start()
        {
			StartCoroutine( HandleSpawnVfx() );
            StartCoroutine( UpdateLifetime() );
        }

		private IEnumerator HandleSpawnVfx()
		{
			int randIdx = Random.Range( 0, _vfx.Count );

			var vfx = _vfx[randIdx];
			vfx.Play( true );

			float timer = 0;
			while ( timer < _sludgeAnimEndTime )
			{
				timer += Time.deltaTime;
				yield return null;
			}

			vfx.Pause( true );
		}

        private IEnumerator UpdateLifetime()
		{
            float timer = 0;
            while ( timer < 1 )
			{
                timer += Time.deltaTime / _lifetime;
                yield return new WaitForFixedUpdate();
			}

			CleanUp();
		}

		public void CleanUp()
		{
			Destroy( gameObject );
		}

		private void OnTriggerEnter2D( Collider2D collision )
		{
			Rigidbody2D body = collision.attachedRigidbody;
			if ( body == null )
			{
				return;
			}

			PlayerController player = body.GetComponent<PlayerController>();
			if ( player == null )
			{
				return;
			}

			var dmgPayload = _damageData.CreatePayload( transform );
			player.TakeDamage( dmgPayload );
		}

		private void Awake()
		{
			_vfx = new List<ParticleSystem>( _vfxContainer.childCount );
			for ( int idx = 0; idx < _vfxContainer.childCount; ++idx )
			{
				var child = _vfxContainer.GetChild( idx );
				if ( !child.gameObject.activeInHierarchy )
				{
					continue;
				}

				_vfx.Add( child.GetComponent<ParticleSystem>() );
			}
		}
	}
}
