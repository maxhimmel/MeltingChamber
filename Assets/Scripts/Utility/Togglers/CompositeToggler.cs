using System.Collections.Generic;
using UnityEngine;

namespace MeltingChamber.Utility
{
    public class CompositeToggler : MonoBehaviour,
        IToggler
    {
		private List<IToggler> _children;

		public void Disable()
		{
			foreach ( var child in _children )
			{
				child.Disable();
			}
		}

		public void Enable()
		{
			foreach ( var child in _children )
			{
				child.Enable();
			}
		}

		public void Init()
		{
			_children = new List<IToggler>();
			GetComponentsInChildren( _children );
			_children.Remove( this );

			foreach ( var child in _children )
			{
				child.Init();
			}
		}
    }
}
