using System.Collections;
using TMPro;
using UnityEngine;

namespace MeltingChamber.Gameplay
{
	public class BucketTextCountRenderer : MonoBehaviour,
		IBucketRenderer
	{
		[SerializeField] private float _countdownDuration = 0.25f;

		private TextMeshPro _textMesh;
		private int _currentFillCount;
		private Coroutine _depositRoutine;

		public void Fill( float percentage, int max )
		{
			CancelCountdown();

			int num = Mathf.FloorToInt( percentage * max );
			_currentFillCount = num;
			_textMesh.text = $"{num}";
		}

		public void Deposit( Transform receptacle )
		{
			CancelCountdown();
			_depositRoutine = StartCoroutine( Countdown( _currentFillCount, _countdownDuration ) );
		}

		private void CancelCountdown()
		{
			if ( _depositRoutine != null )
			{
				StopCoroutine( _depositRoutine );
				_depositRoutine = null;
			}
		}

		private IEnumerator Countdown( int start, float duration )
		{
			float durationPerNumber = duration / start;
			while ( start > 0 )
			{
				_textMesh.text = $"{--start}";
				yield return new WaitForSeconds( durationPerNumber );
			}

			_textMesh.text = string.Empty;
			_depositRoutine = null;
		}

		public void Show()
		{
			_textMesh.enabled = true;
		}

		public void Hide()
		{
			_textMesh.enabled = false;
			CancelCountdown();
		}

		private void Awake()
		{
			_textMesh = GetComponent<TextMeshPro>();
		}
	}
}
