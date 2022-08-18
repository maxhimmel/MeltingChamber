using System.Collections;
using MeltingChamber.Gameplay.Player;
using UnityEngine;

namespace MeltingChamber.Gameplay.LevelPieces
{
    public class Sludge : MonoBehaviour
    {
        [SerializeField] private float _lifetime = 2.5f;

        private void Start()
        {
            StartCoroutine( UpdateLifetime() );
        }

        private IEnumerator UpdateLifetime()
		{
            float timer = 0;
            while ( timer < 1 )
			{
                timer += Time.deltaTime / _lifetime;
                yield return null;
			}

            OnExpiration();
		}

        private void OnExpiration()
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

			player.TakeDamage( transform );
		}
	}
}
