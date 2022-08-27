using MeltingChamber.Gameplay.LevelPieces;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.Player
{
    public class SludgeBucket : MonoBehaviour
    {
		public bool IsFull => _fillCount >= _capacity;

		[SerializeField] private int _capacity = 3;

		private IBucketRenderer _bucketRenderer;
		private Collider2D _trigger;
		private int _fillCount;

		[Inject]
		public void Construct( IBucketRenderer bucketRenderer )
		{
			_bucketRenderer = bucketRenderer;
		}

		public int Deposit()
		{
			int depositAmount = _fillCount;
			if ( depositAmount <= 0 )
			{
				return 0;
			}

			_fillCount = 0;
			_bucketRenderer.Deposit();

			return depositAmount;
		}

		private void OnTriggerEnter2D( Collider2D collision )
		{
			if ( IsFull )
			{
				return;
			}

			Rigidbody2D body = collision.attachedRigidbody;
			if ( body == null )
			{
				return;
			}

			Sludge sludge = body.GetComponent<Sludge>();
			if ( sludge != null )
			{
				sludge.CleanUp();

				++_fillCount;
				_bucketRenderer.Fill( (float)_fillCount / _capacity, _capacity );
			}
		}

		private void OnEnable()
		{
			_trigger.enabled = true;
		}

		private void OnDisable()
		{
			_trigger.enabled = false;
		}

		private void Awake()
		{
			_trigger = GetComponentInChildren<Collider2D>();
		}
	}
}
