using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace MeltingChamber.Framework
{
    public class LevelInitializer : MonoBehaviour
    {
        public event System.Action InitializationCompleted;

        public bool IsInitialized { get; private set; }

        private ITransitionController _transitionController;

        [Inject]
		public void Construct( ITransitionController transitionController )
		{
            _transitionController = transitionController;
		}

        private async void Start()
		{
            await _transitionController.Open();
            
            IsInitialized = true;
            InitializationCompleted?.Invoke();
        }
    }
}
