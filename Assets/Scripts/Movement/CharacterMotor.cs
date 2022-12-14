using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.Movement
{
    public class CharacterMotor : MonoBehaviour
    {
		public float MaxSpeed => _maxSpeed;
		public float Acceleration => _acceleration;
		public Vector2 Velocity => _body.velocity;

		[SerializeField] private float _maxSpeed = 10;
		[SerializeField] private float _acceleration = 10;

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

		public void SetMaxSpeed( float maxSpeed )
		{
			_maxSpeed = maxSpeed;
		}

		public void SetAcceleration( float acceleration )
		{
			_acceleration = acceleration;
		}

		public void ClearMovement()
		{
			_velocity = Vector2.zero;
			_desiredVelocity = Vector2.zero;
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