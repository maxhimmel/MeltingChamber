using UnityEngine;

namespace MeltingChamber.Utility
{
    public class TimeController
    {
        private readonly float _fixedDelta;

        public TimeController()
		{
            _fixedDelta = Time.fixedDeltaTime;
		}

        public void SetScale( float scale )
		{
            Time.timeScale = scale;
            Time.fixedDeltaTime = _fixedDelta * scale;
		}
    }
}
