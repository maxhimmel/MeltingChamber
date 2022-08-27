using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.LevelPieces
{
    public class SludgeSpawner : MonoBehaviour
    {
		[SerializeField] private float _spawnRate = 5;
		[SerializeField] private SludgeDelivery _deliveryPrefab;

		private ArenaBuilder _arena;
        private BoxCollider2D[] _spawnAreas;
		private float _nextSpawnTime = 0;

		[Inject]
		public void Construct( ArenaBuilder arena )
		{
			_arena = arena;
			_nextSpawnTime = Time.timeSinceLevelLoad + _spawnRate;
		}

		private void Update()
		{
			if ( _nextSpawnTime <= Time.timeSinceLevelLoad )
			{
				_nextSpawnTime = Time.timeSinceLevelLoad + _spawnRate;

				Vector2 startPos = GetRandomStartPos();
				Vector2 endPos = GetRandomEndPos();

				var newDelivery = CreateDelivery( startPos );
				newDelivery.Deliver( startPos, endPos );
			}
		}

		private Vector2 GetRandomStartPos()
		{
			BoxCollider2D randArea = GetRandomStartArea();
			Bounds areaBounds = randArea.bounds;
			return new Vector2()
			{
				x = Random.Range( areaBounds.min.x, areaBounds.max.x ),
				y = Random.Range( areaBounds.min.y, areaBounds.max.y )
			};
		}

		private BoxCollider2D GetRandomStartArea()
		{
			int randIndex = Random.Range( 0, _spawnAreas.Length );
			return _spawnAreas[randIndex];
		}

		private Vector2 GetRandomEndPos()
		{
			if ( _arena.TileCount <= 0 )
			{
				return Random.insideUnitCircle;
			}

			int randIndex = Random.Range( 0, _arena.TileCount );
			Tile randTile = _arena.GetTile( randIndex );
			return randTile.transform.position;
		}

		private SludgeDelivery CreateDelivery( Vector2 startPos )
		{
			return Instantiate( _deliveryPrefab, startPos, Quaternion.identity );
		}

		private void Awake()
		{
			_spawnAreas = GetComponentsInChildren<BoxCollider2D>();
		}
	}
}
