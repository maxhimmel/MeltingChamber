using UnityEngine;

namespace MeltingChamber.Gameplay.Player
{
    public class PlayerAnimController
    {
		private readonly Animator _animator;
		private readonly SpriteRenderer _renderer;

		private readonly int _isMovingId = Animator.StringToHash( "IsMoving" );
		private readonly int _isStunnedId = Animator.StringToHash( "IsStunned" );
		private readonly int _dashId = Animator.StringToHash( "Dash" );

		public PlayerAnimController( Animator animator )
		{
			_animator = animator;
			_renderer = animator.GetComponentInChildren<SpriteRenderer>();
		}

		public void ToggleMovement( bool isMoving )
		{
			_animator.SetBool( _isMovingId, isMoving );
		}

		public void SetFacing( Vector2 direction )
		{
			bool isFacingLeft = Vector2.Dot( direction, Vector2.left ) > 0;
			_renderer.flipX = isFacingLeft;
		}

		public void Dash()
		{
			_animator.SetTrigger( _dashId );
		}

		public void SetStunned( bool isStunned )
		{
			_animator.SetBool( _isStunnedId, isStunned );
		}
    }
}
