using System.Collections.Generic;
using System.Threading.Tasks;
using MeltingChamber.Gameplay.Movement;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay
{
	public class BucketFollowRenderer : MonoBehaviour,
		IBucketRenderer
	{
		private Transform FollowTarget => transform;

		[SerializeField] private float _spawnOffset = 1;

		private FollowMotor.Factory _followFactory;
		private List<FollowMotor> _followers;

		[Inject]
		public void Construct( FollowMotor.Factory followFactory )
		{
			_followFactory = followFactory;

			_followers = new List<FollowMotor>();
		}

		public void Show()
		{
			foreach ( var follower in _followers )
			{
				follower.gameObject.SetActive( true );
			}
		}

		public void Hide()
		{
			foreach ( var follower in _followers )
			{
				follower.gameObject.SetActive( false );
			}
		}

		public async Task Deposit( Transform receptacle )
		{
			List<Task> arrivals = new List<Task>();

			foreach ( var follower in _followers )
			{
				follower.SetArrivalDistance( 0 );
				follower.SetTarget( receptacle );

				arrivals.Add( WaitForArrival( follower ) );
			}

			_followers.Clear();

			await Task.WhenAll( arrivals );
		}

		private async Task WaitForArrival( FollowMotor follower )
		{
			while ( follower != null && follower.DistanceToTarget > 0.25f )
			{
				await Task.Yield();
			}

			follower.Cleanup();
		}

		public void Fill( float percentage, int max )
		{
			int fillCount = Mathf.FloorToInt( percentage * max );
			int spawnCount = fillCount - _followers.Count;

			for ( int idx = 0; idx < spawnCount; ++idx )
			{
				var newFollower = _followFactory.Create();
				_followers.Add( newFollower );

				newFollower.SetTarget( FollowTarget );

				Vector3 spawnOffset = Random.insideUnitCircle.normalized * _spawnOffset;
				newFollower.transform.position = FollowTarget.position + spawnOffset;
			}
		}
	}
}
