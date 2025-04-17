using _Quarantine.Code.Infrastructure.Services.ItemDatabase;

namespace _Quarantine.Code.Items.Behaviour
{
    public interface IItem
    {
        
    }
    
    public interface IVisitableItem : IItem
    {
        public void Accept(ISetupItemVisitor visitor);
    }
}