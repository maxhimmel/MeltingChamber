using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Framework
{
    public class LevelInitializer : MonoBehaviour
    {
        public bool IsInitialized { get; private set; }

        private TransitionController _transitionController;

        [Inject]
		public void Construct( TransitionController transitionController )
		{
            _transitionController = transitionController;
		}

        private async void Start()
		{
            await _transitionController.Open();
            IsInitialized = true;
        }
    }
}
