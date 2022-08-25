using MeltingChamber.Gameplay.Player;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.LevelPieces
{
    public class SludgeDeposit : MonoBehaviour
    {
		[SerializeField] private int _capacity = 3;
		[SerializeField] private Transform _pongSpawnOrigin;

		private PongBall.Factory _pongFactory;
		private int _fillCount;

		[Inject]
		public void Construct( PongBall.Factory pongFactory )
		{
			_pongFactory = pongFactory;
		}

		private void OnTriggerEnter2D( Collider2D collision )
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

			int depositAmount = player.DepositSludge();
			_fillCount += depositAmount;

			if ( _fillCount >= _capacity )
			{
				_fillCount = 0;

				PongBall pongBall = _pongFactory.Create();
				pongBall.transform.SetPositionAndRotation( _pongSpawnOrigin.position, _pongSpawnOrigin.rotation );
			}
		}
	}
}
