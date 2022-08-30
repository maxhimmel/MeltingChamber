using System;
using MeltingChamber.Framework;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MeltingChamber.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _tutorialButton;
        [SerializeField] private Button _exitButton;

		private LevelManager _levelManager;
		private Canvas _menuRoot;

		[Inject]
		public void Construct( LevelManager levelManager )
		{
			_levelManager = levelManager;
			_levelManager.Paused += OnPaused;
			_levelManager.Resumed += OnResumed;
		}

		private void OnPaused()
		{
			_menuRoot.enabled = true;
		}

		private void OnResumed()
		{
			_menuRoot.enabled = false;
		}

		private void Start()
		{
			_menuRoot.enabled = false;

			_resumeButton.onClick.AddListener( Resume );
            _exitButton.onClick.AddListener( Quit );
		}

		private void Resume()
		{
			_levelManager.Resume();
		}

		private void Quit()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.ExitPlaymode();
#else
			Application.Quit();
#endif
		}

		private void OnDestroy()
		{
			if ( _levelManager != null )
			{
				_levelManager.Paused -= OnPaused;
				_levelManager.Resumed -= OnResumed;
			}
		}

		private void Awake()
		{
			_menuRoot = GetComponent<Canvas>();
		}
	}
}
