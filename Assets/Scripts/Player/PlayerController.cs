using MeltingChamber.Gameplay.Movement;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
		private Rewired.Player _input;
		private CharacterMotor _motor;
		private DashController _dashController;

		private Vector2 _directedMoveInput = Vector2.right;

		[Inject]
		public void Construct( Rewired.Player input,
			CharacterMotor motor,
			DashController dashController )
		{
			_input = input;
			_motor = motor;
			_dashController = dashController;
		}

		private void Update()
		{
			HandleMovement();
		}

		private void HandleMovement()
		{
			var moveInput = GetMoveInput();
			if ( moveInput != Vector2.zero )
			{
				_directedMoveInput = moveInput;
			}

			if ( !_dashController.IsDashing )
			{
				_motor.SetDesiredVelocity( moveInput );

				if ( _input.GetButtonDown( ReConsts.Action.Dash ) )
				{
					_dashController.Dash( _directedMoveInput );
				}
			}
		}

		private Vector2 GetMoveInput()
		{
			var rawInput = _input.GetAxis2D( ReConsts.Action.MoveHorizontal, ReConsts.Action.MoveVertical );
			return Vector2.ClampMagnitude( rawInput, 1 );
		}
	}
}