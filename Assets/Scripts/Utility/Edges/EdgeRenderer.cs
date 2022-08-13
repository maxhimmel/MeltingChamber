using System.Collections.Generic;
using UnityEngine;

namespace MeltingChamber.Utility.Edge
{
    public class EdgeRenderer
    {
		private readonly LineRenderer _renderer;

		public EdgeRenderer( LineRenderer lineRenderer )
		{
			_renderer = lineRenderer;
		}

		public LineRenderer Build( IList<Vector2> points )
		{
			int pointCount = points.Count;
			Vector3[] renderPoints = new Vector3[pointCount];

			for ( int idx = 0; idx < pointCount; ++idx )
			{
				renderPoints[idx] = points[idx];
			}

			_renderer.positionCount = pointCount;
			_renderer.SetPositions( renderPoints );
			return _renderer;
		}
	}
}
