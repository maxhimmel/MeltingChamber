using MeltingChamber.Extensions;
using MeltingChamber.Utility.Edge;
using UnityEngine;

namespace MeltingChamber.Gameplay.LevelPieces
{
	public class OuterRing : MonoBehaviour
	{
		[SerializeField] private RingConfig _ringConfig = new RingConfig();

		private EdgeBuilder _edgeBuilder;

		private void Start()
		{
			_edgeBuilder.Build();
		}

		private void Awake()
		{
			_edgeBuilder = new EdgeBuilder(
				new EdgeRingCollision( GetComponent<EdgeCollider2D>(), _ringConfig ),
				new EdgeRenderer( GetComponent<LineRenderer>() )
			);
		}

		private void OnDrawGizmosSelected()
		{
			_ringConfig.OnDrawGizmos( transform );
		}
	}
}
