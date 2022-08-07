using UnityEngine;
using System.Collections.Generic;

namespace MeltingChamber.Extensions
{
    public static class AlgorithmExtensions
    {
		public static void Shuffle<T>( this IList<T> array )
		{
			for ( int idx = array.Count - 1; idx > 0; --idx )
			{
				int randIdx = Random.Range( 0, idx + 1 );

				// Swap ...
				T temp = array[idx];
				array[idx] = array[randIdx];
				array[randIdx] = temp;
			}
		}
	}
}
