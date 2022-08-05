using System.Collections;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.Movement
{
    public class DashController : MonoBehaviour
    {
        public bool IsDashing => _dashRoutine != null;

        [SerializeField] private float _distance = 5;
        [SerializeField] private float _duration = 0.3f;

        private CharacterMotor _motor;

        private Coroutine _dashRoutine;
        private float _initialMaxSpeed = -1;
        private float _initialAcceleration = -1;

        [Inject]
		public void Construct( CharacterMotor motor )
		{
            _motor = motor;

            _initialMaxSpeed = motor.MaxSpeed;
            _initialAcceleration = motor.Acceleration;
		}

        public void Cancel()
		{
            if ( IsDashing )
			{
                StopCoroutine( _dashRoutine );
                _dashRoutine = null;

                _motor.SetMaxSpeed( _initialMaxSpeed );
                _motor.SetAcceleration( _initialAcceleration );
			}
		}

        public void Dash( Vector2 direction )
		{
            if ( IsDashing )
			{
                return;
			}

            _dashRoutine = StartCoroutine( UpdateDash( direction ) );
		}

        private IEnumerator UpdateDash( Vector2 direction )
		{
            float dashSpeed = _distance / _duration;
            Vector2 normalizedDirection = direction.normalized;

            _motor.SetMaxSpeed( dashSpeed );
            _motor.SetAcceleration( float.MaxValue );

            float duration = _duration;
            while ( duration > 0 )
			{
                _motor.SetDesiredVelocity( normalizedDirection );

                duration -= Time.fixedDeltaTime;
                if ( duration > 0 )
                {
                    yield return new WaitForFixedUpdate();
                }
            }

            _motor.SetMaxSpeed( _initialMaxSpeed );
            _motor.SetAcceleration( _initialAcceleration );

            _dashRoutine = null;
		}
    }
}
