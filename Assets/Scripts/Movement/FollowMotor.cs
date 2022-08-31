using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.Movement
{
    public class FollowMotor : MonoBehaviour
    {
		public float DistanceToTarget { get; private set; }

		[SerializeField] private float _arrivalDistance = 1;

        private CharacterMotor _motor;
        private Transform _target;

		[Inject]
		public void Construct( CharacterMotor motor )
		{
            _motor = motor;

			DistanceToTarget = Mathf.Infinity;
		}

		public void SetArrivalDistance( float distance )
		{
			_arrivalDistance = distance;
		}

        public void SetTarget( Transform target )
		{
            _target = target;

			if ( target == null )
			{
				DistanceToTarget = 0;
			}
		}

		private void Update()
		{
			if ( _target != null )
			{
				Vector2 moveDir = (_target.position - transform.position);
				DistanceToTarget = moveDir.magnitude;

				Vector2 desiredVelocity = DistanceToTarget > _arrivalDistance
					? moveDir / DistanceToTarget // normalized moveDir
					: Vector2.zero;

				_motor.SetDesiredVelocity( desiredVelocity );
			}
		}

		public void Cleanup()
		{
			Destroy( gameObject );
		}

		public class Factory : PlaceholderFactory<FollowMotor> { }
    }
}
