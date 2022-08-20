using MeltingChamber.Gameplay.LevelPieces;
using UnityEngine;

namespace MeltingChamber.Gameplay.Player
{
    public class SludgeBucket : MonoBehaviour
    {
		public bool IsFull => throw new System.NotImplementedException();

		private Collider2D _trigger;

		private void OnEnable()
		{
			_trigger.enabled = true;
		}

		private void OnDisable()
		{
			_trigger.enabled = false;
		}

		private void OnTriggerEnter2D( Collider2D collision )
		{
			Rigidbody2D body = collision.attachedRigidbody;
			if ( body == null )
			{
				return;
			}

			Sludge sludge = body.GetComponent<Sludge>();
			if ( sludge != null )
			{
				sludge.CleanUp();
			}
		}

		private void Awake()
		{
			_trigger = GetComponentInChildren<Collider2D>();
		}
	}
}
