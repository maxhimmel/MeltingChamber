using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.Movement
{
    public class CharacterMotor : MonoBehaviour
    {
        [SerializeField] private float _acceleration = 10;
        [SerializeField] private float _maxSpeed = 10;

		private Rigidbody2D _body;

        private Vector2 _desiredVelocity;
        private Vector2 _velocity;

        [Inject]
		public void Construct( Rigidbody2D body )
		{
			_body = body;
		}

        public void SetDesiredVelocity( Vector2 direction )
		{
            _desiredVelocity = direction * _maxSpeed;
		}

		private void FixedUpdate()
		{
			_velocity = _body.velocity;

			float moveDelta = _acceleration * Time.deltaTime;
			_velocity = Vector2.MoveTowards( _velocity, _desiredVelocity, moveDelta );

			_body.velocity = _velocity;
		}
	}
}