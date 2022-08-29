using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.Movement
{
    public class FollowMotor : MonoBehaviour
    {
		[SerializeField] private float _arrivalDistance = 1;

        private CharacterMotor _motor;
        private Transform _target;

		[Inject]
		public void Construct( CharacterMotor motor )
		{
            _motor = motor;
		}

        public void SetTarget( Transform target )
		{
            _target = target;
		}

		private void Update()
		{
			if ( _target != null )
			{
				Vector2 moveDir = (_target.position - transform.position);
				float distanceToTarget = moveDir.magnitude;

				Vector2 desiredVelocity = distanceToTarget > _arrivalDistance
					? moveDir / distanceToTarget // normalized moveDir
					: Vector2.zero;

				_motor.SetDesiredVelocity( desiredVelocity );
			}
		}

		public class Factory : PlaceholderFactory<FollowMotor> { }
    }
}
