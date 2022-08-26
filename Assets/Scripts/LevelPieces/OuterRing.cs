using MeltingChamber.Utility.Edge;
using UnityEngine;

namespace MeltingChamber.Gameplay.LevelPieces
{
	public class OuterRing : MonoBehaviour
	{
		[SerializeField] private RingConfig _ringConfig = new RingConfig();

		private EdgeBuilder _edgeBuilder;
		private PongButtonController _buttonController;

		private void Start()
		{
			_edgeBuilder.Build();
			_buttonController.Build( _ringConfig );
		}

		private void Awake()
		{
			_edgeBuilder = new EdgeBuilder(
				new EdgeRingCollision( GetComponent<EdgeCollider2D>(), _ringConfig ),
				new EdgeRenderer( GetComponent<LineRenderer>() )
			);

			_buttonController = GetComponentInChildren<PongButtonController>();
		}

		private void OnDrawGizmosSelected()
		{
			_ringConfig.OnDrawGizmos( transform );
		}
	}
}
