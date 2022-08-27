using System;
using MeltingChamber.Utility;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.LevelPieces
{
    public class PongButton : MonoBehaviour
    {
		public event EventHandler Triggered;

		private IToggler _renderToggler;

		private void OnEnable()
		{
			_renderToggler.Enable();
		}

		private void OnDisable()
		{
			_renderToggler.Disable();
		}

		private void OnTriggerEnter2D( Collider2D collision )
		{
			Rigidbody2D body = collision.attachedRigidbody;
			if ( body == null )
			{
				return;
			}

			PongBall pongBall = body.GetComponent<PongBall>();
			if ( pongBall != null )
			{
				Triggered?.Invoke( this, EventArgs.Empty );
			}
		}

		private void Awake()
		{
			_renderToggler = GetComponentInChildren<IToggler>();
			_renderToggler.Init();
		}

		public class Factory : PlaceholderFactory<PongButton> { }
	}
}
