using UnityEngine;

namespace MeltingChamber.Extensions
{
    public static class VectorExtensions
    {
        public static float Random( this Vector2 self )
		{
            return UnityEngine.Random.Range( self.x, self.y );
		}
    }
}
