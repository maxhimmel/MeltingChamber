using System;
using System.Collections.Generic;
using MeltingChamber.Utility.Edge;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.LevelPieces
{
    public class PongButtonController : MonoBehaviour
    {
		[SerializeField] private int _buttonCount = 5;
		[SerializeField, Range( 0, 360 )] private float _rotationOffset = 0;

		private PongButton.Factory _buttonFactory;
		private ArenaDissolver _dissolver;
		private List<PongButton> _buttons;

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

				newButton.Triggered += OnButtonTriggered;
				_buttons.Add( newButton );
			}
		}

		private PongButton CreateButton( Vector3 position )
		{
			var newButton = _buttonFactory.Create();

			newButton.transform.SetParent( transform );
			newButton.transform.position = position;

			return newButton;
		}

		private void OnButtonTriggered( object sender, EventArgs args )
		{
			if ( sender is PongButton button )
			{
				_dissolver.ResetCountdown();
			}
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
