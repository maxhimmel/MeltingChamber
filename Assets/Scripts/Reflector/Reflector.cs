using MeltingChamber.Utility.Edge;
using UnityEngine;

namespace MeltingChamber.Gameplay
{
    public class Reflector : MonoBehaviour
    {
		[SerializeField] private ArcConfig _arcConfig = new ArcConfig();

		private EdgeBuilder _edgeBuilder;

		private void OnEnable()
		{
			if ( _edgeBuilder != null && _edgeBuilder.Edge != null )
			{
				_edgeBuilder.Edge.enabled = true;
			}
		}

		private void OnDisable()
		{
			if ( _edgeBuilder != null && _edgeBuilder.Edge != null )
			{
				_edgeBuilder.Edge.enabled = false;
			}
		}

		private void Start()
		{
			_edgeBuilder.Build();
		}

		private void Awake()
		{
			_edgeBuilder = new EdgeBuilder(
				new EdgeArcCollision( GetComponent<EdgeCollider2D>(), _arcConfig ),
				new EdgeRenderer( GetComponent<LineRenderer>() )
			);
		}

		private void OnDrawGizmosSelected()
		{
			_arcConfig.OnDrawGizmos( transform );
		}
	}
}
