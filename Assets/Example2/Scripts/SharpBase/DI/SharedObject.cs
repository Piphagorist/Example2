namespace SharpBase.DI
{
    public abstract class SharedObject : IShared
    {
        protected GlobalContainer Container;
        
        public virtual void Init() {}

        internal void SetContainer(GlobalContainer container)
        {
            Container = container;
        }
    }
}