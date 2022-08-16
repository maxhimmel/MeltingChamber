using MeltingChamber.Framework;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MeltingChamber.UI
{
    public class MainMenu : MonoBehaviour
    {
		[SerializeField] private Button _startButton;

		private LevelManager _levelManager;

		[Inject]
		public void Construct( LevelManager levelManager )
		{
            _levelManager = levelManager;
		}

		private void Start()
		{
			_startButton.onClick.AddListener( LoadNextLevel );
		}

		private async void LoadNextLevel()
		{
			_startButton.interactable = false;
			await _levelManager.LoadNextLevel();
		}
	}
}
