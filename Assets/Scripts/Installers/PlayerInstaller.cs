using MeltingChamber.Gameplay.Movement;

namespace MeltingChamber.Installers
{
    public class PlayerInstaller : ActorInstaller
    {
		public override void InstallBindings()
		{
			base.InstallBindings();

			Container.Bind<CharacterMotor>().FromMethod( GetComponentInChildren<CharacterMotor> ).AsSingle();
			Container.Bind<DashController>().FromMethod( GetComponentInChildren<DashController> ).AsSingle();
		}
	}
}
