namespace _Quarantine.Code.Infrastructure.Services.SaveLoad
{
    public interface ISavableEntity
    {
        public void AcceptSave(ISavableEntitiesVisitor visitor);
    }
}