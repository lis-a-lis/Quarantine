using _Quarantine.Code.Infrastructure.Services.ItemDatabase;

namespace _Quarantine.Code.Items.Behaviour
{
    public interface IVisitableItem
    {
        public string Id { get; }
        public void Accept(ISetupItemVisitor visitor);
    }
}