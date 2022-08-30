using Zenject;
using Rewired;
using MeltingChamber.Framework;
using MeltingChamber.Utility;

namespace MeltingChamber.Installers
{
    public class AppInstaller : MonoInstaller
    {
		public override void InstallBindings()
		{
			Container.Bind<Player>()
				.FromMethod( GetFirstPlayer )
				.AsSingle();


			Container.Bind<LevelLoader>()
				.AsSingle();

			Container.Bind<ITransitionController>()
				.FromMethod( GetComponentInChildren<ITransitionController> )
				.AsSingle();

			Container.Bind<LevelManager>()
				.AsSingle();

			Container.Bind<TimeController>()
				.AsSingle();
		}

		private Player GetFirstPlayer()
		{
			return ReInput.players.GetPlayer( 0 );
		}
	}
}
