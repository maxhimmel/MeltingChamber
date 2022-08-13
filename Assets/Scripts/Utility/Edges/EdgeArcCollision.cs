using UnityEngine;

namespace MeltingChamber.Utility.Edge
{
	public class EdgeArcCollision : IEdgeCollision
	{
		private readonly EdgeCollider2D _edge;
		private readonly ArcConfig _config;

		public EdgeArcCollision( EdgeCollider2D edge,
			ArcConfig config )
		{
			_edge = edge;
			_config = config;
		}

		public EdgeCollider2D Build()
		{
			Vector3 origin = _config.CenterOffset;

			float radius = _config.Diameter / 2f;
			Vector3 lhsPos = origin - Vector3.right * radius;
			Vector3 rhsPos = origin + Vector3.right * radius;

			Vector3 centerPivot = origin + Vector3.up * _config.RadiusOffset;
			Vector3 lhsRelativeCenter = lhsPos - centerPivot;
			Vector3 rhsRelativeCenter = rhsPos - centerPivot;

			Vector2[] points = new Vector2[_config.Resolution + 1];
			for ( int idx = 0; idx <= _config.Resolution; ++idx )
			{
				float t = idx / (float)_config.Resolution;
				Vector3 pos = Vector3.Slerp( lhsRelativeCenter, rhsRelativeCenter, t ) + centerPivot;

				points[idx] = pos;
			}

			_edge.points = points;
			return _edge;
		}

		public void OnDrawGizmos( Transform owner )
		{
			_config.OnDrawGizmos( owner );
		}
	}
}
