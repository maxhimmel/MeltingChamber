using Zenject;
using Rewired;

namespace MeltingChamber.Installers
{
    public class AppInstaller : MonoInstaller
    {
		public override void InstallBindings()
		{
			Container.Bind<Player>().FromMethod( GetFirstPlayer ).AsSingle();
		}

		private Player GetFirstPlayer()
		{
			return ReInput.players.GetPlayer( 0 );
		}
	}
}
