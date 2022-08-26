using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.LevelPieces
{
    public class PongButton : MonoBehaviour
    {
		private void OnTriggerEnter2D( Collider2D collision )
		{
			Rigidbody2D body = collision.attachedRigidbody;
			if ( body == null )
			{
				return;
			}

			PongBall pongBall = body.GetComponent<PongBall>();
			if ( pongBall != null )
			{
				Debug.Log( "Button hit!", this );
			}
		}

		public class Factory : PlaceholderFactory<PongButton> { }
	}
}
