using MeltingChamber.Gameplay.Movement;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Installers
{
    public class FollowerInstaller : MonoInstaller
    {
		public override void InstallBindings()
		{
			Container.Bind<Rigidbody2D>()
				.FromMethod( GetComponentInChildren<Rigidbody2D> )
				.AsSingle();

			Container.Bind<CharacterMotor>()
				.FromMethod( GetComponentInChildren<CharacterMotor> )
				.AsSingle();
		}
	}
}
