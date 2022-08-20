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
		private DamageHandler _damageHandler;
		private SludgeBucket _sludgeBucket;

		private Vector2 _directedAimInput = Vector2.up;
		private Vector2 _directedMoveInput = Vector2.right;
		private float _initialMoveSpeed;

		[Inject]
		public void Construct( Rewired.Player input,
			CharacterMotor motor,
			DashController dashController,
			Reflector reflector,
			DamageHandler damageHandler,
			SludgeBucket sludgeBucket )
		{
			_input = input;
			_motor = motor;
			_dashController = dashController;
			_reflector = reflector;
			_damageHandler = damageHandler;
			_sludgeBucket = sludgeBucket;

			_initialMoveSpeed = _motor.MaxSpeed;
		}

		public bool TakeDamage( Transform dmgSource )
		{
			if ( _dashController.IsDashing )
			{
				return false;
			}

			_motor.ClearMovement();
			_damageHandler.TakeDamage( dmgSource );
			SetReflectorActive( false );

			return true;
		}

		private void Update()
		{
			if ( _damageHandler.IsStunned )
			{
				return;
			}

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
				_sludgeBucket.enabled = false;
				SetReflectorActive( true );
			}
			else if ( _input.GetButtonUp( Action.Reflect ) )
			{
				SetReflectorActive( false );
			}
		}

		private void SetReflectorActive( bool isActive )
		{
			_reflector.enabled = isActive;

			float moveSpeed = isActive ? _reflectorMoveSpeed : _initialMoveSpeed;
			_motor.SetMaxSpeed( moveSpeed );
		}

		private async void HandleMovement()
		{
			var moveInput = GetAxisInput( Action.MoveHorizontal, Action.MoveVertical, ref _directedMoveInput );

			if ( !_dashController.IsDashing )
			{
				_motor.SetDesiredVelocity( moveInput );

				if ( _input.GetButtonDown( Action.Dash ) && !_reflector.enabled )
				{
					_sludgeBucket.enabled = true;
					await _dashController.Dash( _directedMoveInput );
					_sludgeBucket.enabled = false;
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

		private void Start()
		{
			_reflector.enabled = false;
			_sludgeBucket.enabled = false;
		}
	}
}