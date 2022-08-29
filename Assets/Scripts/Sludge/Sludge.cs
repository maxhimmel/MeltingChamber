using System.Collections;
using MeltingChamber.Gameplay.Player;
using UnityEngine;

namespace MeltingChamber.Gameplay.LevelPieces
{
    public class Sludge : MonoBehaviour
    {
        [SerializeField] private float _lifetime = 2.5f;
		[SerializeField] private DamageDatum _damageData = new DamageDatum();

		[Header( "Animations" )]
		[SerializeField] private Transform _vfxContainer;

		private ParticleSystem[] _vfx;

        private void Start()
        {
			StartCoroutine( HandleSpawnVfx() );
            StartCoroutine( UpdateLifetime() );
        }

		private IEnumerator HandleSpawnVfx()
		{
			int randIdx = Random.Range( 0, _vfx.Length );

			var vfx = _vfx[randIdx];
			vfx.Play( true );

			yield break;

			////// Wait a frame for an opportunity to let a particle spawn ...
			////yield return null;

			//while ( vfx.isPlaying )
			//{
			//	foreach ( Transform child in vfx.transform )
			//	{
			//		var childVfx = child.GetComponent<ParticleSystem>();
			//		Debug.LogWarning( childVfx.name );
			//		Debug.Log( childVfx.particleCount );
			//		Debug.Log( childVfx.isEmitting );
			//		Debug.Log( childVfx.IsAlive() );
			//	}
			//	yield return null;
			//}

			////int particleCount = 1;
			////while ( particleCount > 0 )
			////{
			////	int particleSum = 0;
			////	for ( int idx = 0; idx < vfx.subEmitters.subEmittersCount; ++idx )
			////	{
			////		var subVfx = vfx.subEmitters.GetSubEmitterSystem( idx );
			////		particleSum += subVfx.particleCount;
			////	}

			////	particleCount = particleSum;
			////	yield return null;
			////}

			//Debug.LogError( "DESPAWN" );
		}

        private IEnumerator UpdateLifetime()
		{
            float timer = 0;
            while ( timer < 1 )
			{
                timer += Time.deltaTime / _lifetime;
                yield return null;
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
			_vfx = new ParticleSystem[_vfxContainer.childCount];
			for ( int idx = 0; idx < _vfxContainer.childCount; ++idx )
			{
				var child = _vfxContainer.GetChild( idx );
				_vfx[idx] = child.GetComponent<ParticleSystem>();
			}
		}
	}
}
