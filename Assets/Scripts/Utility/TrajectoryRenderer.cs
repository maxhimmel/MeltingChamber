using MeltingChamber.Extensions;
using Unity.Collections;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Utility
{
    public class TrajectoryRenderer : MonoBehaviour
    {
		private const int _maxPoints = 50;

		[SerializeField] private float _length = 7;
		[SerializeField] private LayerMask _collisionLayer = -1;

		private Rigidbody2D _body;
		private Collider2D _collider;
		private LineRenderer _renderer;
		private Vector3[] _trajectoryPositions;

		[Inject]
		public void Construct( Rigidbody2D body,
			Collider2D collider )
		{
			_body = body;
			_collider = collider;

			_trajectoryPositions = new Vector3[_maxPoints];
		}

		private void FixedUpdate()
		{
			Vector2 origin = _body.position;
			Vector2 velocity = _body.velocity;
			if ( velocity == Vector2.zero )
			{
				_renderer.enabled = false;
				return;
			}


			int pointCount = 1;
			float distance = 0;
			_trajectoryPositions[0] = origin;

			Vector2 startPos = origin;
			Vector2 trajectoryDir = velocity.normalized;

			while ( distance < _length )
			{
				var hitInfo = Physics2D.Raycast( startPos, trajectoryDir, _length, _collisionLayer );

				if ( hitInfo.collider != null )
				{
					distance += hitInfo.distance;
					Vector2 collisionOffset = trajectoryDir * _collider.bounds.extents.Max();
					_trajectoryPositions[pointCount] = hitInfo.point - collisionOffset;

					trajectoryDir = Vector2.Reflect( trajectoryDir, hitInfo.normal );
				}
				else
				{
					float remainingDistance = _length - distance;
					distance += remainingDistance;

					Vector2 prevPos = _trajectoryPositions[pointCount - 1];
					_trajectoryPositions[pointCount] = prevPos + trajectoryDir * remainingDistance;
				}

				++pointCount;
			}

			UpdateRenderer( pointCount );
		}

		private void UpdateRenderer( int pointCount )
		{
			_renderer.positionCount = pointCount;
			_renderer.SetPositions( _trajectoryPositions );

			_renderer.enabled = pointCount != 0;
		}

		private void Awake()
		{
			_renderer = GetComponent<LineRenderer>();
		}
	}
}
