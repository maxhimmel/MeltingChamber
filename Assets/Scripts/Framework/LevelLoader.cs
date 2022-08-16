using UnityEngine.SceneManagement;

namespace MeltingChamber.Framework
{
    public class LevelLoader
    {
        public int CurrentLevelIndex => SceneManager.GetActiveScene().buildIndex;
        public int TotalLevels => SceneManager.sceneCountInBuildSettings;

        public void Load( string name )
		{
            SceneManager.LoadScene( name );
		}

        public void Load( int index )
        {
            SceneManager.LoadScene( index );
        }
    }
}
