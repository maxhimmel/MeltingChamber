using UnityEngine;

namespace MeltingChamber.Utility
{
	public class MaterialToggler : MonoBehaviour,
		IToggler
	{
		[SerializeField] private Material _enabled;
		[SerializeField] private Material _disabled;

		private SpriteRenderer _renderer;

		public void Disable()
		{
			_renderer.material = _disabled;
		}

		public void Enable()
		{
			_renderer.material = _enabled;
		}

		public void Init()
		{
			_renderer = GetComponentInChildren<SpriteRenderer>();
		}
	}
}
