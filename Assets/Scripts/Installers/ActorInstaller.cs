using UnityEngine;
using Zenject;

namespace MeltingChamber.Installers
{
    public class ActorInstaller : MonoInstaller
    {
		public override void InstallBindings()
		{
			Container.Bind<Rigidbody2D>().FromMethod( GetComponentInChildren<Rigidbody2D> ).AsSingle();
		}
	}
}
