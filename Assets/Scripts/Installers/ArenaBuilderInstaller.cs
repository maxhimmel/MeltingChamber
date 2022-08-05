using Zenject;
using UnityEngine;

namespace MeltingChamber
{
    public class ArenaBuilderInstaller : MonoInstaller
    {
        [SerializeField] private Transform _tilePrefab;

		public override void InstallBindings()
		{
			Container.BindFactory<Transform, PlaceholderFactory<Transform>>()
				.FromComponentInNewPrefab( _tilePrefab )
				.UnderTransform( transform )
				.AsSingle();
		}
	}
}
