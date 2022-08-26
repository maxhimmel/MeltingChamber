using MeltingChamber.Framework;
using MeltingChamber.Gameplay;
using MeltingChamber.Gameplay.LevelPieces;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
		[SerializeField] private Cinemachine.CinemachineBrain _cineBrain;

		[BoxGroup("Prefabs")]
		[SerializeField] private PongBall _pongBallPrefab;
		[BoxGroup( "Prefabs" )]
		[SerializeField] private PongButton _pongButtonPrefab;

		public override void InstallBindings()
		{
			Container.Bind<LevelInitializer>()
				.FromNewComponentOnNewGameObject()
				.AsSingle()
				.NonLazy();

			Container.BindInstance( _cineBrain )
				.AsSingle();

			Container.Bind<ICameraResolver>()
				.To<CineBrainCameraResolver>()
				.AsSingle();

			Container.BindFactory<PongBall, PongBall.Factory>()
				.FromComponentInNewPrefab( _pongBallPrefab )
				.AsSingle();

			Container.BindFactory<PongButton, PongButton.Factory>()
				.FromComponentInNewPrefab( _pongButtonPrefab )
				.AsSingle();
		}
	}
}
