using UnityEngine;
using Zenject;

namespace MeltingChamber.Gameplay.LevelPieces
{
    public class Tile : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<Object, Tile> { }
	}
}
