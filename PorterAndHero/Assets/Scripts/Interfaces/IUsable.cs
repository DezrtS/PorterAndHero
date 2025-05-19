using System.Collections.Generic;

namespace Interfaces
{
    public interface IUsable
    {
        public delegate void UsedEventHandler(IUsable usable, bool isUsing, UseContext useContext);
        public event UsedEventHandler Used;
        public bool IsUsing { get; }
        public bool IsDisabled { get; }
        public abstract bool CanUse();
        public abstract bool CanStopUsing();
        public abstract void Use(UseContext useContext);
        public abstract void StopUsing(UseContext useContext);
    }

    public struct UseContext
    {

    }
}