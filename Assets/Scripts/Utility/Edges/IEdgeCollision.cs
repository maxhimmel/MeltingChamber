using UnityEngine;

namespace MeltingChamber.Utility.Edge
{
    public interface IEdgeCollision 
    {
        EdgeCollider2D Build();
        void OnDrawGizmos( Transform owner );
    }
}
