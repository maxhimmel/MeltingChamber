using UnityEngine;

namespace MeltingChamber.Gameplay
{
    public interface IBucketRenderer
    {
        void Show();
        void Hide();

        void Fill( float percentage, int max );
        void Deposit( Transform receptacle );
    }
}
