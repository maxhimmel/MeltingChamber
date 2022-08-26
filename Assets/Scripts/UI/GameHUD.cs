using MeltingChamber.Gameplay.LevelPieces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MeltingChamber.UI
{
    public class GameHUD : MonoBehaviour
    {
        [SerializeField] private Image _countdownFill;

        private ArenaDissolver _dissolver;

        [Inject]
		public void Construct( ArenaDissolver dissolver )
		{
            _dissolver = dissolver;
		}

		private void Update()
		{
			_countdownFill.fillAmount = _dissolver.CountdownRatio;
		}
	}
}
