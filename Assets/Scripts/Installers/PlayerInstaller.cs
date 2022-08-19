using MeltingChamber.Gameplay;
using MeltingChamber.Gameplay.Movement;
using MeltingChamber.Gameplay.Player;

namespace MeltingChamber.Installers
{
    public class PlayerInstaller : ActorInstaller
    {
		public override void InstallBindings()
		{
			base.InstallBindings();

			Container.Bind<CharacterMotor>().FromMethod( GetComponentInChildren<CharacterMotor> ).AsSingle();
			Container.Bind<DashController>().FromMethod( GetComponentInChildren<DashController> ).AsSingle();
			Container.Bind<Reflector>().FromMethod( GetComponentInChildren<Reflector> ).AsSingle();
			Container.Bind<DamageHandler>().FromMethod( GetComponentInChildren<DamageHandler> ).AsSingle();
		}
	}
}
