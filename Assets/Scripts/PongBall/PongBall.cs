using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay
{
    public class PongBall : MonoBehaviour
    {
		[SerializeField] private float _moveSpeed = 10;

		private Rigidbody2D _body;

		[Inject]
		public void Construct( Rigidbody2D body )
		{
            _body = body;
		}

		private void OnCollisionEnter2D( Collision2D collision )
		{
			var contact = collision.GetContact( 0 );
			var reflect = Vector2.Reflect( -collision.relativeVelocity, contact.normal );

			_body.velocity = reflect.normalized * _moveSpeed;
		}

		public class Factory : PlaceholderFactory<PongBall> { }
	}
}
