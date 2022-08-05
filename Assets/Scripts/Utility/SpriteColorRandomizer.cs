using MeltingChamber.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace MeltingChamber.Utility
{
    public class SpriteColorRandomizer : MonoBehaviour
    {
		[SerializeField] private bool _randomizeOnStart = true;

		[GUIColor( "GetRandomColor" )]
		[BoxGroup, SerializeField, MinMaxSlider( 0, 1 )] private Vector2 _hueRange = new Vector2( 0, 1 );
		[GUIColor( "GetRandomColor" )]
		[BoxGroup, SerializeField, MinMaxSlider( 0, 1 )] private Vector2 _saturationRange = new Vector2( 0, 1 );
		[GUIColor( "GetRandomColor" )]
		[BoxGroup, SerializeField, MinMaxSlider( 0, 1 )] private Vector2 _valueRange = new Vector2( 0, 1 );
		[GUIColor( "GetRandomColor" )]
		[BoxGroup, SerializeField, MinMaxSlider( 0, 1 )] private Vector2 _alphaRange = new Vector2( 0, 1 );

		private SpriteRenderer _renderer;

		private void Start()
		{
			if ( _randomizeOnStart )
			{
				RandomizeColor();
			}
		}

		[ContextMenu( "Randomize Color" )]
		public void RandomizeColor()
		{
			_renderer.color = GetRandomColor();
		}

		private Color GetRandomColor()
		{
			return Random.ColorHSV(
				_hueRange.Random(), _hueRange.Random(),
				_saturationRange.Random(), _saturationRange.Random(),
				_valueRange.Random(), _valueRange.Random(),
				_alphaRange.Random(), _alphaRange.Random()
			);
		}

		private void Awake()
		{
			_renderer = GetComponentInChildren<SpriteRenderer>();
		}
	}
}
