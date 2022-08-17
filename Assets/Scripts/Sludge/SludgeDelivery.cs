using System.Collections;
using UnityEngine;

namespace MeltingChamber.Gameplay.LevelPieces
{
    public class SludgeDelivery : MonoBehaviour
    {
        [SerializeField] private float _speed = 5;

        private Coroutine _travelRoutine;

        public void Deliver( Vector2 start, Vector2 end )
		{
            _travelRoutine = StartCoroutine( UpdateTravel( start, end ) );
		}

        private IEnumerator UpdateTravel( Vector2 start, Vector2 end )
		{
            float timer = 0;
            while ( timer < 1 )
			{
                timer += Time.deltaTime / _speed;

                Vector2 newPos = Vector3.Slerp( start, end, timer );
                transform.position = newPos;

                yield return null;
			}

            _travelRoutine = null;
            OnDelivered();
		}

        private void OnDelivered()
		{
            Destroy( gameObject );
		}
    }
}
