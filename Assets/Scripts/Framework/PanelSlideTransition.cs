using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MeltingChamber.Framework
{
    public class PanelSlideTransition : MonoBehaviour,
        ITransitionController
    {
        [SerializeField] private RectTransform _panel;

        [Space]
        [SerializeField, Min( 0 )] private float _duration = 1;

        [Space]
        [SerializeField] private AnimationCurve _animation = AnimationCurve.EaseInOut( 0, 0, 1, 1 );
        [SerializeField] private Vector2 _closePosition = Vector2.zero;
        [SerializeField] private Vector2 _openPosition = new Vector2( 0, 1080f );

        public async Task Open()
		{
            await SlidePanel( _closePosition, _openPosition );
        }

        public async Task Close()
        {
            await SlidePanel( _openPosition, _closePosition );
        }

        private async Task SlidePanel( Vector2 startValue, Vector2 endValue )
		{
            float timer = _duration > 0 ? 0 : 1;
            while ( timer < 1 )
			{
                timer += Time.deltaTime / _duration;
                float anim = _animation.Evaluate( timer );

                Vector2 newPos = Vector2.LerpUnclamped( startValue, endValue, anim );
                SetPanelPosition( newPos );

                await Task.Yield();
			}

            SetPanelPosition( endValue );
		}

        private void SetPanelPosition( Vector2 newPos )
		{
            _panel.anchoredPosition = newPos;
		}
    }
}
