using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeltingChamber.Extensions;
using MeltingChamber.Utility.Edge;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.LevelPieces
{
    public class PongButtonController : MonoBehaviour
    {
		[Header( "Spawn Config" )]
		[SerializeField] private int _buttonCount = 5;
		[SerializeField, Range( 0, 360 )] private float _rotationOffset = 0;

		[Header( "Interactions" )]
		[PropertyRange( 1, nameof( _buttonCount ) )]
		[SerializeField] private int _maxActiveButtonCount = 2;
		[SerializeField] private float _reactivateDelay = 5;

		private PongButton.Factory _buttonFactory;
		private ArenaDissolver _dissolver;
		private List<PongButton> _buttons;
		private int _nextButtonIndex;

		[Inject]
		public void Construct( PongButton.Factory buttonFactory, 
			ArenaDissolver dissolver )
		{
            _buttonFactory = buttonFactory;
			_dissolver = dissolver;

			_buttons = new List<PongButton>( _buttonCount );
		}

		public void Build( RingConfig config )
		{
			foreach ( Vector3 spawnPos in GetSpawnPositions( config.Radius ) )
			{
				var newButton = CreateButton( spawnPos );

				newButton.enabled = false;
				newButton.Triggered += OnButtonTriggered;

				_buttons.Add( newButton );
			}

			InitializeFirstActiveButtons();
		}

		private void InitializeFirstActiveButtons()
		{
			_buttons.Shuffle();
			_nextButtonIndex = 0;

			for ( int idx = 0; idx < _maxActiveButtonCount; ++idx )
			{
				var button = GetNextButton();
				button.enabled = true;
			}
		}

		private PongButton GetNextButton()
		{
			var result = _buttons[_nextButtonIndex];
			
			++_nextButtonIndex;
			if ( _nextButtonIndex >= _buttons.Count )
			{
				_nextButtonIndex = 0;
				_buttons.Shuffle();
			}

			return result;
		}

		private IEnumerable<Vector3> GetSpawnPositions( float radius )
		{
			Vector3 origin = transform.position;
			for ( int idx = 0; idx < _buttonCount; ++idx )
			{
				float percentage = (idx + 1) / (float)_buttonCount;
				float radian = Mathf.Deg2Rad * (360f * percentage + _rotationOffset);

				Vector3 offset = transform.right * Mathf.Cos( radian ) + transform.up * Mathf.Sin( radian );
				Vector3 spawnPos = origin + offset * radius;

				yield return spawnPos;
			}
		}

		private PongButton CreateButton( Vector3 position )
		{
			var newButton = _buttonFactory.Create();

			newButton.transform.SetParent( transform );
			newButton.transform.position = position;
			newButton.transform.rotation = Quaternion.LookRotation( Vector3.forward, transform.position - position );

			return newButton;
		}

		private void OnButtonTriggered( object sender, EventArgs args )
		{
			if ( sender is PongButton button )
			{
				_dissolver.ResetCountdown();
				button.enabled = false;

				HandleReactivation();
			}
		}

		private async void HandleReactivation()
		{
			await Task.Delay( TimeSpan.FromSeconds( _reactivateDelay ) );

			var button = GetNextButton();
			button.enabled = true;
		}


#if UNITY_EDITOR
		[Header( "Editor/Tools" )]
		[SerializeField] private Color _gizmoColor = Color.yellow;
		[SerializeField] private float _gizmoRingRadius = 9.35f;
		[SerializeField] private float _gizmoButtonRadius = 0.35f;

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = _gizmoColor;

			foreach ( Vector3 spawnPos in GetSpawnPositions( _gizmoRingRadius ) )
			{
				Gizmos.DrawSphere( spawnPos, _gizmoButtonRadius );
			}
		}
#endif
	}
}
