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
		private CircleCollider2D _collider;
		private LineRenderer _renderer;
		private Vector3[] _trajectoryPositions;

		[Inject]
		public void Construct( Rigidbody2D body,
			Collider2D collider )
		{
			_body = body;
			_collider = collider as CircleCollider2D ?? throw new System.InvalidCastException();

			_trajectoryPositions = new Vector3[_maxPoints];
		}

		private void FixedUpdate()
		{
			Vector2 velocity = _body.velocity;
			if ( velocity == Vector2.zero )
			{
				_renderer.enabled = false;
				return;
			}


			int pointCount = 1;
			float remainingDistance = _length;
			_trajectoryPositions[0] = _body.position;

			Vector2 startPos = _body.position;
			Vector2 trajectoryDir = velocity.normalized;

			while ( remainingDistance > 0 )
			{
				float radius = _collider.radius * _collider.transform.lossyScale.Max();
				var hitInfo = Physics2D.CircleCast( startPos, radius, trajectoryDir, remainingDistance, _collisionLayer );

				if ( hitInfo.collider != null )
				{
					remainingDistance -= hitInfo.distance;
					startPos = hitInfo.centroid;
					trajectoryDir = Vector2.Reflect( trajectoryDir, hitInfo.normal );

					_trajectoryPositions[pointCount] = hitInfo.centroid;
				}
				else
				{
					Vector2 prevPos = _trajectoryPositions[pointCount - 1];
					_trajectoryPositions[pointCount] = prevPos + trajectoryDir * remainingDistance;

					remainingDistance = 0;
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
