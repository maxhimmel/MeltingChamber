using MeltingChamber.Gameplay;
using MeltingChamber.Gameplay.Movement;
using MeltingChamber.Gameplay.Player;
using UnityEngine;

namespace MeltingChamber.Installers
{
    public class PlayerInstaller : ActorInstaller
    {
		[SerializeField] private FollowMotor _slimeBucketFollowerPrefab;

		public override void InstallBindings()
		{
			base.InstallBindings();

			Container.Bind<CharacterMotor>().FromMethod( GetComponentInChildren<CharacterMotor> ).AsSingle();
			Container.Bind<DashController>().FromMethod( GetComponentInChildren<DashController> ).AsSingle();
			Container.Bind<Reflector>().FromMethod( GetComponentInChildren<Reflector> ).AsSingle();
			Container.Bind<DamageHandler>().FromMethod( GetComponentInChildren<DamageHandler> ).AsSingle();
			Container.Bind<SludgeBucket>().FromMethod( GetComponentInChildren<SludgeBucket> ).AsSingle();
			Container.Bind<IBucketRenderer>().FromMethod( GetComponentInChildren<IBucketRenderer> ).AsSingle();

			Container.Bind<Animator>().FromMethod( GetComponentInChildren<Animator> ).AsSingle();
			Container.Bind<PlayerAnimController>().AsSingle();

			Container.BindFactory<FollowMotor, FollowMotor.Factory>()
				.FromComponentInNewPrefab( _slimeBucketFollowerPrefab )
				.UnderTransform( context => null )
				.AsSingle();
		}
	}
}
