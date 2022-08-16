using System.Threading.Tasks;
using UnityEngine;

namespace MeltingChamber.Framework
{
    public class TransitionController
    {
		private readonly TransitionData _data;

		public TransitionController( TransitionData data )
		{
			_data = data;
		}

        public async Task Open()
        {
            int milliseconds = (int)(_data.OpenDuration * 1000);
            await Task.Delay( milliseconds );
        }

        public async Task Close()
        {
            int milliseconds = (int)(_data.CloseDuration * 1000);
            await Task.Delay( milliseconds );
        }
    }

    [System.Serializable]
	public class TransitionData
	{
        public float OpenDuration => _openDuration;
        public float CloseDuration => _closeDuration;

        [SerializeField] private float _openDuration = 1;
        [SerializeField] private float _closeDuration = 1;
	}
}
