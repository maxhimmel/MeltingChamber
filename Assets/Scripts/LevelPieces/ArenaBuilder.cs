using UnityEngine;
using System.Collections.Generic;
using Zenject;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MeltingChamber.Gameplay.LevelPieces
{
    public class ArenaBuilder : MonoBehaviour
    {
        [SerializeField] private float _radius = 8;
        [SerializeField] private Vector2 _cellSize = Vector2.one;

		private PlaceholderFactory<Transform> _tileFactory;

		[Inject]
		public void Construct( PlaceholderFactory<Transform> tileFactory )
		{
			_tileFactory = tileFactory;
		}

		private void Start()
		{
			foreach ( Vector3 gridPos in GetGridPositions() )
			{
				var newTile = _tileFactory.Create();
				newTile.SetPositionAndRotation( gridPos, Quaternion.identity );
			}
		}

		private IEnumerable<Vector3> GetGridPositions()
		{
			int ceiledRadius = Mathf.CeilToInt( _radius );
			int width = ceiledRadius * 2;
			int height = ceiledRadius * 2;

			Vector3 origin = transform.position;
			Vector3 centerOffset = new Vector3( width / 2f, height / 2f );
			Vector3 cellOffset = new Vector3( _cellSize.x / 2f, _cellSize.y / 2f );

			for ( int row = 0; row < height; ++row )
			{
				for ( int col = 0; col < width; ++col )
				{
					Vector3 offset = new Vector3( col * _cellSize.x, row * _cellSize.y );
					Vector3 pos = origin + offset - centerOffset + cellOffset;

					bool isWithinRadius = (pos - origin).sqrMagnitude <= _radius * _radius;
					if ( isWithinRadius )
					{
						yield return pos;
					}
				}
			}
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
			Vector3 cellOffset = new Vector3( _cellSize.x / 2f, _cellSize.y / 2f );

			Gizmos.color = _gridColor;
			for ( int row = 0; row < height; ++row )
			{
				for ( int col = 0; col < width; ++col )
				{
					Vector3 offset = new Vector3( col * _cellSize.x, row * _cellSize.y );
					Vector3 pos = origin + offset - centerOffset + cellOffset;

					bool isWithinRadius = (pos - origin).sqrMagnitude <= _radius * _radius;
					if ( isWithinRadius )
					{
						Gizmos.DrawCube( pos, _cellSize );
					}
					else
					{
						Gizmos.DrawWireCube( pos, _cellSize );
					}
				}
			}

			Handles.color = _radiusColor;
			Handles.DrawWireDisc( origin, Vector3.back, _radius );
		}
#endif
	}
}
