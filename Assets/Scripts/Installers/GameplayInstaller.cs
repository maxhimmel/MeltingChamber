using MeltingChamber.Framework;
using MeltingChamber.Gameplay;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
		[SerializeField] private Cinemachine.CinemachineBrain _cineBrain;
		[SerializeField] private PongBall _pongBallPrefab;

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
		}
	}
}
