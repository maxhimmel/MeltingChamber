using MeltingChamber.Gameplay.Movement;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
		private Rewired.Player _input;
		private CharacterMotor _motor;

		[Inject]
		public void Construct( Rewired.Player input,
			CharacterMotor motor )
		{
			_input = input;
			_motor = motor;
		}

		private void Update()
		{
			var moveInput = GetMoveInput();
			_motor.SetDesiredVelocity( moveInput );
		}

		private Vector2 GetMoveInput()
		{
			var rawInput = _input.GetAxis2D( ReConsts.Action.MoveHorizontal, ReConsts.Action.MoveVertical );
			return Vector2.ClampMagnitude( rawInput, 1 );
		}
	}
}