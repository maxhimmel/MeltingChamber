using UnityEngine;

namespace MeltingChamber.Utility.Edge
{
    public class EdgeBuilder
    {
		public EdgeCollider2D Edge => _edgeCollider;
		public LineRenderer Renderer => _lineRenderer;

		private readonly IEdgeCollision _collisionBuilder;
		private readonly EdgeRenderer _rendererBuilder;

		private EdgeCollider2D _edgeCollider;
		private LineRenderer _lineRenderer;

		public EdgeBuilder( IEdgeCollision collision,
            EdgeRenderer renderer )
		{
			_collisionBuilder = collision;
			_rendererBuilder = renderer;
		}

		public void Build()
		{
			_edgeCollider = _collisionBuilder.Build();
			_lineRenderer = _rendererBuilder.Build( _edgeCollider.points );
		}

		public void OnDrawGizmos( Transform owner )
		{
			_collisionBuilder.OnDrawGizmos( owner );
		}
    }
}
