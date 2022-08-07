using UnityEngine;

namespace MeltingChamber.Extensions
{
    public static class VectorExtensions
    {
        public static float Random( this Vector2 self )
		{
            return UnityEngine.Random.Range( self.x, self.y );
		}

        public static Vector2 XY( this Vector3 self )
		{
            return new Vector2( self.x, self.y );
		}
    }
}
