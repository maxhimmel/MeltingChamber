using UnityEngine;
using Zenject;

namespace MeltingChamber.Installers
{
    public class ActorInstaller : MonoInstaller
    {
		[SerializeField] private Collider2D _mainCollider = default;

		public override void InstallBindings()
		{
			Container.Bind<Rigidbody2D>().FromMethod( GetComponentInChildren<Rigidbody2D> ).AsSingle();
			Container.BindInstance( _mainCollider ).AsSingle();
		}
	}
}
