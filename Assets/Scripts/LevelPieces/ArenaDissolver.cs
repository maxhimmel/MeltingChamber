using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.LevelPieces
{
    public class ArenaDissolver : MonoBehaviour
    {
		public float CountdownRatio => _countdown / _rate;

        [SerializeField] private float _rate = 10;
        [SerializeField] private float _distanceThreshold = 0.5f;

		private ArenaBuilder _builder;
		private float _countdown = 0;
		private float _radius;

        [Inject]
		public void Construct( ArenaBuilder builder )
		{
            _builder = builder;
			_radius = builder.Radius;
		}

		private void Update()
		{
			_countdown += Time.deltaTime;
			if ( _countdown >= _rate )
			{
				OnDissolveCountdownExpired();
			}
		}

		private void OnDissolveCountdownExpired()
		{
			ResetCountdown();

			Tile tile = GetDissolvableTile();
			if ( tile != null )
			{
				_builder.RemoveTile( tile );
			}
			else
			{
				_radius -= _builder.CellSize / 2f;
			}
		}

		public void ResetCountdown()
		{
			_countdown = 0;
		}

		private Tile GetDissolvableTile()
		{
			List<Tile> viableTiles = new List<Tile>();

			float distance = _radius - _distanceThreshold;
			foreach ( Tile tile in _builder.Tiles )
			{
				float distSqr = (tile.transform.position - _builder.transform.position).sqrMagnitude;
				if ( distSqr >= distance * distance )
				{
					viableTiles.Add( tile );
				}
			}

			if ( viableTiles.Count <= 0 )
			{
				return null;
			}

			int randIdx = Random.Range( 0, viableTiles.Count );
			return viableTiles[randIdx];
		}
	}
}
