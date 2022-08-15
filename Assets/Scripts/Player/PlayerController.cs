using MeltingChamber.Gameplay.Movement;
using MeltingChamber.ReConsts;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
		[SerializeField] private float _reflectorMoveSpeed = 5;

		private Rewired.Player _input;
		private CharacterMotor _motor;
		private DashController _dashController;
		private Reflector _reflector;

		private Vector2 _directedAimInput = Vector2.up;
		private Vector2 _directedMoveInput = Vector2.right;
		private float _initialMoveSpeed;

		[Inject]
		public void Construct( Rewired.Player input,
			CharacterMotor motor,
			DashController dashController,
			Reflector reflector )
		{
			_input = input;
			_motor = motor;
			_dashController = dashController;
			_reflector = reflector;

			reflector.enabled = false;
			_initialMoveSpeed = _motor.MaxSpeed;
		}

		private void Update()
		{
			HandleReflector();
			HandleMovement();
		}

		private void HandleReflector()
		{
			var aimInput = GetAxisInput( Action.AimHorizontal, Action.AimVertical, ref _directedAimInput );
			_reflector.transform.rotation = Quaternion.LookRotation( Vector3.forward, _directedAimInput );

			if ( _input.GetButtonDown( Action.Reflect ) )
			{
				_dashController.Cancel();

				_reflector.enabled = true;
				_motor.SetMaxSpeed( _reflectorMoveSpeed );
			}
			else if ( _input.GetButtonUp( Action.Reflect ) )
			{
				_reflector.enabled = false;
				_motor.SetMaxSpeed( _initialMoveSpeed );
			}
		}

		private void HandleMovement()
		{
			var moveInput = GetAxisInput( Action.MoveHorizontal, Action.MoveVertical, ref _directedMoveInput );

			if ( !_dashController.IsDashing )
			{
				_motor.SetDesiredVelocity( moveInput );

				if ( _input.GetButtonDown( Action.Dash ) && !_reflector.enabled )
				{
					_dashController.Dash( _directedMoveInput );
				}
			}
		}

		private Vector2 GetAxisInput( int horizontalId, int verticalId, ref Vector2 validDirectedInput )
		{
			var rawInput = _input.GetAxis2D( horizontalId, verticalId );
			var result = Vector2.ClampMagnitude( rawInput, 1 );

			if ( result != Vector2.zero )
			{
				validDirectedInput = result;
			}

			return result;
		}
	}
}