using UnityEngine;

namespace MeltingChamber.Gameplay.LevelPieces
{
    public class TileProvider : MonoBehaviour
    {
        public Transform Container => transform;

        [SerializeField] private Tile[] _tilePrefabs;

        public Tile GetRandomTile()
		{
            int randIdx = Random.Range( 0, _tilePrefabs.Length );
            return _tilePrefabs[randIdx];
		}
    }
}
