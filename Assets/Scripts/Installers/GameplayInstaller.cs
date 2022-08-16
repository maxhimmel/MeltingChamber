using MeltingChamber.Framework;
using MeltingChamber.Gameplay;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
		[SerializeField] private Cinemachine.CinemachineBrain _cineBrain = default;

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
		}
	}
}
