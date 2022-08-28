using UnityEngine;

namespace MeltingChamber.Utility
{
	public class SpriteToggler : MonoBehaviour,
		IToggler
	{
		[SerializeField] private Sprite _enabled;
		[SerializeField] private Sprite _disabled;

		private SpriteRenderer _renderer;

		public void Disable()
		{
			_renderer.sprite = _disabled;
		}

		public void Enable()
		{
			_renderer.sprite = _enabled;
		}

		public void Init()
		{
			_renderer = GetComponentInChildren<SpriteRenderer>();
		}
	}
}
