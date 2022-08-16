using Zenject;
using Rewired;
using MeltingChamber.Framework;
using UnityEngine;

namespace MeltingChamber.Installers
{
    public class AppInstaller : MonoInstaller
    {
		[SerializeField] private TransitionData _transitionData = new TransitionData();

		public override void InstallBindings()
		{
			Container.Bind<Player>()
				.FromMethod( GetFirstPlayer )
				.AsSingle();


			Container.Bind<LevelLoader>()
				.AsSingle();

			Container.Bind<TransitionController>()
				.AsSingle()
				.WithArguments( _transitionData );

			Container.Bind<LevelManager>()
				.AsSingle();
		}

		private Player GetFirstPlayer()
		{
			return ReInput.players.GetPlayer( 0 );
		}
	}
}
