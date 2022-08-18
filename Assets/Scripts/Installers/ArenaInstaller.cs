using Zenject;
using UnityEngine;
using MeltingChamber.Gameplay.LevelPieces;

namespace MeltingChamber.Installers
{
    public class ArenaInstaller : MonoInstaller
    {
        [SerializeField] private Transform _tilePrefab;
		[SerializeField] private Transform _tileContainer;

		public override void InstallBindings()
		{
			Container.BindFactory<Transform, PlaceholderFactory<Transform>>()
				.FromComponentInNewPrefab( _tilePrefab )
				.UnderTransform( _tileContainer )
				.AsSingle();

			Container.Bind<ArenaBuilder>()
				.FromMethod( GetComponentInChildren<ArenaBuilder> )
				.AsSingle();
		}
	}
}
