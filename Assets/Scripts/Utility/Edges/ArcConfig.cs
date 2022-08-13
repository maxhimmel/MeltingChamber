using UnityEngine;

namespace MeltingChamber.Utility.Edge
{
    [System.Serializable]
    public class ArcConfig
    {
        public int Resolution => _resolution;
        public float Diameter => _diameter;
        public float RadiusOffset => _radiusOffset;
        public Vector3 CenterOffset => _centerOffset;

        [SerializeField] private int _resolution;
        [SerializeField] private float _diameter;
        [SerializeField] private float _radiusOffset;
        [SerializeField] private Vector3 _centerOffset;

		[Header( "Editor/Tools" )]
		[SerializeField] private Color _gizmoColor = Color.green;

        public void OnDrawGizmos( Transform owner )
		{
			Vector3 origin = owner.position + _centerOffset;

			float radius = _diameter / 2f;
			Vector3 lhsPos = origin - owner.right * radius;
			Vector3 rhsPos = origin + owner.right * radius;

			Vector3 centerPivot = origin + owner.up * _radiusOffset;
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
    }
}
