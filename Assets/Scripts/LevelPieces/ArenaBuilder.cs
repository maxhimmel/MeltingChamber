using UnityEngine;
using System.Collections.Generic;
using Zenject;
using MeltingChamber.Extensions;
using Sirenix.OdinInspector;

namespace MeltingChamber.Gameplay.LevelPieces
{
    public class ArenaBuilder : MonoBehaviour
    {
		public IEnumerable<Tile> Tiles => _tiles;
		public int TileCount => _tiles.Count;
		public float Radius => _radius;
		public float CellSize => _cellSize;

		[SerializeField] private TileProvider _tileProvider;

		[BoxGroup]
        [SerializeField] private float _radius = 8;
		[BoxGroup]
		[SerializeField] private float _cellSize = 1;

		private Tile.Factory _tileFactory;
		private List<Tile> _tiles = new List<Tile>();

		[Inject]
		public void Construct( Tile.Factory tileFactory )
		{
			_tileFactory = tileFactory;
		}

		private void Start()
		{
			foreach ( Vector3 gridPos in GetGridPositions() )
			{
				var newTile = CreateTile( gridPos );
				AddTile( newTile );
			}

			_tiles.Shuffle();
		}

		private IEnumerable<Vector3> GetGridPositions()
		{
			int ceiledRadius = Mathf.CeilToInt( _radius );
			int width = ceiledRadius * 2;
			int height = ceiledRadius * 2;

			Vector3 origin = transform.position;
			Vector3 centerOffset = new Vector3( width / 2f, height / 2f );
			Vector3 cellOffset = new Vector3( _cellSize / 2f, _cellSize / 2f );

			for ( int row = 0; row < height; ++row )
			{
				for ( int col = 0; col < width; ++col )
				{
					Vector3 offset = new Vector3( col * _cellSize, row * _cellSize );
					Vector3 pos = origin + offset - centerOffset + cellOffset;

					bool isWithinRadius = (pos - origin).sqrMagnitude <= _radius * _radius;
					if ( isWithinRadius )
					{
						yield return pos;
					}
				}
			}
		}

		private Tile CreateTile( Vector3 position )
		{
			var newTile = _tileFactory.Create( _tileProvider.GetRandomTile() );

			newTile.transform.SetParent( _tileProvider.Container );
			newTile.transform.SetPositionAndRotation( position, Quaternion.identity );
			newTile.transform.localScale = Vector3.one * _cellSize;

			return newTile;
		}

		public void AddTile( Tile tile )
		{
			_tiles.Add( tile );
		}

		public void RemoveTile( Tile tile )
		{
			if ( _tiles.Remove( tile ) )
			{
				tile.Dissolve();
			}
		}

		public Tile GetTile( int index )
		{
			return _tiles[index];
		}

#if UNITY_EDITOR
		[Header( "Editor/Tools" )]
		[SerializeField] private Color _radiusColor = Color.red;
		[SerializeField] private Color _gridColor = Color.black;

		private void OnDrawGizmosSelected()
		{
			int ceiledRadius = Mathf.CeilToInt( _radius );
			int width = ceiledRadius * 2;
			int height = ceiledRadius * 2;

			Vector3 origin = transform.position;
			Vector3 centerOffset = new Vector3( width / 2f, height / 2f );
			Vector3 cellOffset = new Vector3( _cellSize / 2f, _cellSize / 2f );

			Gizmos.color = _gridColor;
			for ( int row = 0; row < height; ++row )
			{
				for ( int col = 0; col < width; ++col )
				{
					Vector3 offset = new Vector3( col * _cellSize, row * _cellSize );
					Vector3 pos = origin + offset - centerOffset + cellOffset;

					bool isWithinRadius = (pos - origin).sqrMagnitude <= _radius * _radius;
					if ( isWithinRadius )
					{
						Gizmos.DrawCube( pos, _cellSize * Vector3.one );
					}
					Gizmos.DrawWireCube( pos, _cellSize * Vector3.one );
				}
			}

			UnityEditor.Handles.color = _radiusColor;
			UnityEditor.Handles.DrawWireDisc( origin, Vector3.back, _radius );
		}
#endif
	}
}
