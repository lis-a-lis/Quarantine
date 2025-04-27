namespace _Quarantine.Code.Infrastructure.PersistentProgress
{
    public interface ISavable<TData>
    {
        public TData Save();
    }
}