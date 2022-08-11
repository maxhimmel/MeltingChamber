using UnityEngine;

namespace MeltingChamber.Gameplay
{
    public class Reflector : MonoBehaviour
    {
		[SerializeField] private int _resolution = 5;
		[SerializeField] private float _radiusOffset = 0;
		[SerializeField] private float _diameter = 2.5f;
		[SerializeField] private Vector3 _centerOffset = new Vector3( 0, 0.75f, 0 );

		private EdgeCollider2D _edgeCollider;

		private void Start()
		{
			GenerateCollision();
		}

		private void GenerateCollision()
		{
			Vector3 origin = _centerOffset;

			float radius = _diameter / 2f;
			Vector3 lhsPos = origin - Vector3.right * radius;
			Vector3 rhsPos = origin + Vector3.right * radius;

			Vector3 centerPivot = origin + Vector3.up * _radiusOffset;
			Vector3 lhsRelativeCenter = lhsPos - centerPivot;
			Vector3 rhsRelativeCenter = rhsPos - centerPivot;

			Vector2[] points = new Vector2[_resolution + 1];
			for ( int idx = 0; idx <= _resolution; ++idx )
			{
				float t = idx / (float)_resolution;
				Vector3 pos = Vector3.Slerp( lhsRelativeCenter, rhsRelativeCenter, t ) + centerPivot;

				points[idx] = pos;
			}

			_edgeCollider.points = points;
		}

		private void Awake()
		{
			_edgeCollider = GetComponentInChildren<EdgeCollider2D>();
		}

#if UNITY_EDITOR
		[Header( "Editor/Tools" )]
		[SerializeField] private Color _gizmoColor = Color.green;

		private void OnDrawGizmosSelected()
		{
			Vector3 origin = transform.position + _centerOffset;

			float radius = _diameter / 2f;
			Vector3 lhsPos = origin - transform.right * radius;
			Vector3 rhsPos = origin + transform.right * radius;

			Vector3 centerPivot = origin + transform.up * _radiusOffset;
			Vector3 lhsRelativeCenter = lhsPos - centerPivot;
			Vector3 rhsRelativeCenter = rhsPos - centerPivot;

			Gizmos.color = _gizmoColor;
			for ( int idx = 0; idx < _resolution; ++idx )
			{
				float t1 = idx / (float)_resolution;
				Vector3 pos1 = Vector3.Slerp( lhsRelativeCenter, rhsRelativeCenter, t1 ) + centerPivot;

				float t2 = (idx + 1) / (float)_resolution;
				Vector3 pos2 = Vector3.Slerp( lhsRelativeCenter, rhsRelativeCenter, t2 ) + centerPivot;

				Gizmos.DrawLine( pos1, pos2 );
			}
		}
#endif
	}
}
