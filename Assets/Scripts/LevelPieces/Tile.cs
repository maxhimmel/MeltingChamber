using System.Collections;
using MeltingChamber.Utility;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.LevelPieces
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private float _dissolveDuration = 0.5f;
        [SerializeField] private float _dissolveAnimSpeed = 1;

		private IToggler _rendererToggler;

        public void Dissolve()
		{
			StartCoroutine( UpdateDissolve() );
		}

        private IEnumerator UpdateDissolve()
		{
			bool toggle = true;
			float animTimer = 0;

			float dissolveTimer = 0;
			while ( dissolveTimer < 1 )
			{
				dissolveTimer += Time.deltaTime / _dissolveDuration;
				animTimer += Time.deltaTime / _dissolveAnimSpeed;

				if ( animTimer >= 1 )
				{
					animTimer = 0;
					toggle = !toggle;

					System.Action toggleAction = toggle ? _rendererToggler.Enable : _rendererToggler.Disable;
					toggleAction();
				}

				yield return new WaitForFixedUpdate();
			}

			Cleanup();
		}

		private void Cleanup()
		{
			Destroy( gameObject );
		}

		private void Awake()
		{
			_rendererToggler = GetComponentInChildren<IToggler>();
			_rendererToggler.Init();
		}

		public class Factory : PlaceholderFactory<Object, Tile> { }
	}
}
