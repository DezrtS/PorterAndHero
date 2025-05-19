using Interfaces;
using UnityEngine;

namespace Systems.Items
{
    public abstract class Item : MonoBehaviour, IUsable
    {
        public event IUsable.UsedEventHandler Used;
        public bool IsUsing { get; }
        public bool IsDisabled { get; }
        
        public bool CanUse()
        {
            throw new System.NotImplementedException();
        }

        public bool CanStopUsing()
        {
            throw new System.NotImplementedException();
        }

        public void Use(UseContext useContext)
        {
            throw new System.NotImplementedException();
        }

        public void StopUsing(UseContext useContext)
        {
            throw new System.NotImplementedException();
        }
    }
}
