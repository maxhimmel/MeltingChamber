using System.Threading.Tasks;
using MeltingChamber.Utility;

namespace MeltingChamber.Framework
{
    public class LevelManager
    {
        public event System.Action Paused;
        public event System.Action Resumed;

        public bool IsPaused { get; private set; }

        private readonly LevelLoader _levelLoader;
		private readonly ITransitionController _transitionController;
		private readonly TimeController _timeController;

		public LevelManager( LevelLoader levelLoader,
            ITransitionController transitionController,
            TimeController timeController )
		{
            IsPaused = false;

            _levelLoader = levelLoader;
            _transitionController = transitionController;
			_timeController = timeController;
		}

        public async Task LoadNextLevel()
		{
            await _transitionController.Close();

            int currentLevel = _levelLoader.CurrentLevelIndex;
            int nextLevel = ++currentLevel;

            if ( nextLevel >= _levelLoader.TotalLevels )
			{
                nextLevel = 0;
			}

            _levelLoader.Load( nextLevel );
		}

        public void TogglePauseState()
		{
			if ( IsPaused )
			{
				Resume();
			}
            else
			{
                Pause();
			}
		}

		public void Pause()
		{
            _timeController.SetScale( 0 );

            IsPaused = true;
            Paused?.Invoke();
		}

        public void Resume()
        {
            _timeController.SetScale( 1 );

            IsPaused = false;
            Resumed?.Invoke();
        }
    }
}
