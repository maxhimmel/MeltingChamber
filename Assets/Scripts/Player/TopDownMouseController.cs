using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.Player
{
    public class TopDownMouseController : MonoBehaviour
    {
        private Rewired.Player _input;
        private Rewired.CustomController _controller;
		private ICameraResolver _cameraResolver;

        [Inject]
		public void Construct( Rewired.Player input,
			ICameraResolver cameraResolver )
		{
            _input = input;
			_cameraResolver = cameraResolver;

			string controllerID = ReConsts.CustomController.TopDownMouse.name;
			_controller = _input.controllers.GetControllerWithTag<Rewired.CustomController>( controllerID );

			Debug.Assert( _controller != null, $"No custom controller exists w/tag '{controllerID}'", this );

			_controller.SetAxisUpdateCallback( GetAxisValue );
			_input.controllers.AddLastActiveControllerChangedDelegate( OnLastActiveControllerChanged );
		}

		private float GetAxisValue( int index )
		{
            if ( index == ReConsts.CustomController.TopDownMouse.Axis.Horizontal )
			{
				return GetDirectionToMouse().x;
			}
            else if ( index == ReConsts.CustomController.TopDownMouse.Axis.Vertical )
			{
				return GetDirectionToMouse().y;
			}
			else
			{
				return 0;
			}
		}

		private Vector2 GetDirectionToMouse()
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = transform.position.z;

			Camera camera = _cameraResolver.Camera;
			mousePos = camera.ScreenToWorldPoint( mousePos );

			return (mousePos - transform.position).normalized;
		}

        private void OnLastActiveControllerChanged( Rewired.Player input, Rewired.Controller controller )
		{
            if ( controller.type == Rewired.ControllerType.Mouse )
			{
                _input.controllers.AddController( _controller, false );
			}
            else if ( controller.type == Rewired.ControllerType.Joystick )
			{
                _input.controllers.RemoveController( _controller );
			}
		}

		private void OnDestroy()
		{
			if ( _input != null )
			{
                _input.controllers.RemoveLastActiveControllerChangedDelegate( OnLastActiveControllerChanged );
			}
		}
	}
}
