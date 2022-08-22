using Zenject;
using UnityEngine;
using MeltingChamber.Gameplay.LevelPieces;

namespace MeltingChamber.Installers
{
    public class ArenaInstaller : MonoInstaller
    {
		public override void InstallBindings()
		{
			Container.BindFactory<Object, Tile, Tile.Factory>()
				.FromFactory<PrefabFactory<Tile>>();

			Container.Bind<ArenaBuilder>()
				.FromMethod( GetComponentInChildren<ArenaBuilder> )
				.AsSingle();
		}
	}
}
