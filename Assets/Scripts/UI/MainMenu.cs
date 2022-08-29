using MeltingChamber.Framework;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MeltingChamber.UI
{
    public class MainMenu : MonoBehaviour
    {
		[SerializeField] private Button _startButton;
		[SerializeField] private Button _exitButton;

		private LevelManager _levelManager;

		[Inject]
		public void Construct( LevelManager levelManager )
		{
            _levelManager = levelManager;
		}

		private void Start()
		{
			_startButton.onClick.AddListener( LoadNextLevel );
			_exitButton.onClick.AddListener( Quit );
		}

		private async void LoadNextLevel()
		{
			_startButton.interactable = false;
			await _levelManager.LoadNextLevel();
		}

		private void Quit()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.ExitPlaymode();
#else
			Application.Quit();
#endif
		}
	}
}
