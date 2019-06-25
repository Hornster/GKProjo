namespace Assets.Scripts.PlayerRemade.Contracts
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    /// <summary>
    /// Describes class that can be observed by another class. 
    /// </summary>
    /// <typeparam name="TObservedType">Type which will be passed to observer upon notification.</typeparam>
    public interface IObservable<out TObservedType>
    {
        /// <summary>
        /// Adds observer to the class.
        /// </summary>
        /// <param name="observer">Observer object that requires data from TObservedType
        /// upon notification.</param>
        void AddObserver(IObserver<TObservedType> observer);
    }
}
