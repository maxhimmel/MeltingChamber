using Zenject;
using UnityEngine;
using MeltingChamber.Gameplay.LevelPieces;

namespace MeltingChamber.Installers
{
    public class ArenaInstaller : MonoInstaller
    {
		[SerializeField] private ArenaBuilder _builder;
		[SerializeField] private ArenaDissolver _dissolver;

		public override void InstallBindings()
		{
			Container.BindFactory<Object, Tile, Tile.Factory>()
				.FromFactory<PrefabFactory<Tile>>();

			Container.BindInstance( _builder )
				.AsSingle();

			Container.BindInstance( _dissolver )
				.AsSingle();
		}
	}
}
