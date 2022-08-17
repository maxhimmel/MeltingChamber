using System.Threading.Tasks;

namespace MeltingChamber.Framework
{
    public class LevelManager
    {
        private LevelLoader _levelLoader;
		private ITransitionController _transitionController;

		public LevelManager( LevelLoader levelLoader,
            ITransitionController transitionController )
		{
            _levelLoader = levelLoader;
            _transitionController = transitionController;
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
    }
}
