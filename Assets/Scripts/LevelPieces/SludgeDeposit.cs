using MeltingChamber.Gameplay.Player;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.LevelPieces
{
    public class SludgeDeposit : MonoBehaviour
    {
		private readonly int _depositId = Animator.StringToHash( "Deposit" );
		private readonly int _spawnId = Animator.StringToHash( "Spawn" );

		[SerializeField] private int _capacity = 3;
		[SerializeField] private Transform _depositOrigin;
		[SerializeField] private Transform _pongSpawnOrigin;

		[Header( "Animations" )]
		[SerializeField] private Animator _depositAnimController;
		[SerializeField] private Animator _pongSpawnAnimController;

		private PongBall.Factory _pongFactory;
		private int _fillCount;

		[Inject]
		public void Construct( PongBall.Factory pongFactory )
		{
			_pongFactory = pongFactory;
		}

		private async void OnTriggerEnter2D( Collider2D collision )
		{
			Rigidbody2D body = collision.attachedRigidbody;
			if ( body == null )
			{
				return;
			}

			PlayerController player = body.GetComponent<PlayerController>();
			if ( player == null )
			{
				return;
			}

			int depositAmount = await player.DepositSludge( _depositOrigin );
			if ( depositAmount <= 0 )
			{
				return;
			}

			_fillCount += depositAmount;

			_depositAnimController.SetTrigger( _depositId );

			if ( _fillCount >= _capacity )
			{
				_fillCount = 0;

				_pongSpawnAnimController.SetTrigger( _spawnId );

				PongBall pongBall = _pongFactory.Create();
				pongBall.transform.SetPositionAndRotation( _pongSpawnOrigin.position, _pongSpawnOrigin.rotation );
			}
		}
	}
}
