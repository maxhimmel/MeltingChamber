using MeltingChamber.Extensions;
using UnityEngine;

namespace MeltingChamber.Utility.Edge
{
	public class EdgeRingCollision : IEdgeCollision
	{
		private readonly EdgeCollider2D _edge;
		private readonly RingConfig _config;

		public EdgeRingCollision( EdgeCollider2D edge,
			RingConfig config )
		{
			_edge = edge;
			_config = config;
		}

		public EdgeCollider2D Build()
		{
			Vector2[] colliderPoints = new Vector2[_config.Resolution + 1];

			float angleStep = 360f / _config.Resolution;
			for ( int idx = 0; idx <= _config.Resolution; ++idx )
			{
				float radian = Mathf.Deg2Rad * (angleStep * idx);
				Vector2 offsetDir = Vector3.right * Mathf.Cos( radian ) + Vector3.up * Mathf.Sin( radian );
				Vector2 pos = _edge.transform.position.XY() + offsetDir * _config.Radius;

				colliderPoints[idx] = pos;
			}

			_edge.points = colliderPoints;
			return _edge;
		}

		public void OnDrawGizmos( Transform owner )
		{
			_config.OnDrawGizmos( owner );
		}
	}
}
