namespace Assets.Scripts.PlayerRemade.Contracts
{
    /// <summary>
    /// Describes object that observes another one.
    /// </summary>
    /// <typeparam name="TObservedType">Type that is being observed by the object.</typeparam>
    public interface IObserver <in TObservedType>
    {
        /// <summary>
        /// Called by observed object. 
        /// </summary>
        /// <param name="observedObject"></param>
        void Notify(TObservedType observedObject);
    }
}
