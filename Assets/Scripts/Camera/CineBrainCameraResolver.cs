using Cinemachine;
using MeltingChamber.Gameplay;
using UnityEngine;

namespace MeltingChamber
{
	public class CineBrainCameraResolver : ICameraResolver
	{
		public Camera Camera => _brain.OutputCamera;

		private readonly CinemachineBrain _brain;

		public CineBrainCameraResolver( CinemachineBrain brain )
		{
			_brain = brain;
		}
	}
}
