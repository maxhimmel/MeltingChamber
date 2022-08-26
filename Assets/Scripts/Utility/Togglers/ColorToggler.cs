using UnityEngine;

namespace MeltingChamber.Utility
{
    public class ColorToggler : MonoBehaviour,
        IToggler
    {
        [SerializeField] private Color _enabled = Color.green;
        [SerializeField] private Color _disabled = Color.red;

		private SpriteRenderer _renderer;

		public void Init()
		{
			_renderer = GetComponentInChildren<SpriteRenderer>();
		}

		public void Disable()
		{
			_renderer.color = _disabled;
		}

		public void Enable()
		{
			_renderer.color = _enabled;
		}
	}
}
