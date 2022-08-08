using UnityEngine;
using UnityEngine.UIElements;

namespace MeltingChamber.Utility
{
    public class Rotator : MonoBehaviour
    {
		[SerializeField] private Vector3 _axis = Vector3.forward;
        [SerializeField] private float _anglePerSec = 360;

		private void FixedUpdate()
		{
			Vector3 rotationDelta = _axis * _anglePerSec * Time.deltaTime;
			transform.rotation *= Quaternion.Euler( rotationDelta );
		}
	}
}
