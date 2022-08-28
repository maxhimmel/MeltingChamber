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
		private Rigidbody2D _body;

		private void OnEnable()
		{
			_body.simulated = true;
			_renderToggler.Enable();
		}

		private void OnDisable()
		{
			_body.simulated = false;
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
			_body = GetComponent<Rigidbody2D>();

			_renderToggler = GetComponentInChildren<IToggler>();
			_renderToggler.Init();
		}

		public class Factory : PlaceholderFactory<PongButton> { }
	}
}
