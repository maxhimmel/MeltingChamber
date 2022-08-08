using MeltingChamber.Extensions;
using UnityEngine;

namespace MeltingChamber.Gameplay.LevelPieces
{
	public class OuterRing : MonoBehaviour
	{
		[Header( "Collision" )]
		[SerializeField] private int _resolution = 20;
		[SerializeField] private float _radius = 10;

		private EdgeCollider2D _edgeCollider;

		private void Start()
		{
			GenerateCollision();
		}

		private void GenerateCollision()
		{
			Vector2[] colliderPoints = new Vector2[_resolution + 1];

			float angleStep = 360f / _resolution;
			for ( int idx = 0; idx <= _resolution; ++idx )
			{
				float radian = Mathf.Deg2Rad * (angleStep * idx);
				Vector2 offsetDir = Vector3.right * Mathf.Cos( radian ) + Vector3.up * Mathf.Sin( radian );
				Vector2 pos = transform.position.XY() + offsetDir * _radius;

				colliderPoints[idx] = pos;
			}

			_edgeCollider.points = colliderPoints;
		}

		private void Awake()
		{
			_edgeCollider = GetComponentInChildren<EdgeCollider2D>();
		}

#if UNITY_EDITOR
		[Header( "Editor/Tools" )]
		[SerializeField] private Color _radiusColor = Color.green;

		private void OnDrawGizmosSelected()
		{
			UnityEditor.Handles.color = _radiusColor;
			UnityEditor.Handles.DrawWireDisc( transform.position, Vector3.back, _radius );
		}
#endif
	}
}
