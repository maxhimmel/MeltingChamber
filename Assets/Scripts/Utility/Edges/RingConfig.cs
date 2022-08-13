using MeltingChamber.Extensions;
using UnityEngine;

namespace MeltingChamber.Utility.Edge
{
    [System.Serializable]
    public class RingConfig
    {
        public int Resolution => _resolution;
        public float Radius => _radius;

        [SerializeField] private int _resolution;
        [SerializeField] private float _radius;

		[Header( "Editor/Tools" )]
		[SerializeField] private Color _gizmoColor = Color.green;

        public void OnDrawGizmos( Transform owner )
		{
			Gizmos.color = _gizmoColor;

			float angleStep = 360f / _resolution;
			for ( int idx = 0; idx < _resolution; ++idx )
			{
				float rad1 = Mathf.Deg2Rad * (angleStep * idx);
				Vector2 offset1 = Vector3.right * Mathf.Cos( rad1 ) + Vector3.up * Mathf.Sin( rad1 );
				Vector2 pos1 = owner.position.XY() + offset1 * _radius;

				float rad2 = Mathf.Deg2Rad * (angleStep * (idx + 1));
				Vector2 offset2 = Vector3.right * Mathf.Cos( rad2 ) + Vector3.up * Mathf.Sin( rad2 );
				Vector2 pos2 = owner.position.XY() + offset2 * _radius;

				Gizmos.DrawLine( pos1, pos2 );
			}
		}
    }
}
