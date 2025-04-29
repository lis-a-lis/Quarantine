using _Quarantine.Code.Stats;

namespace _Quarantine.Code.Items.Behaviour
{
    public interface IUsableStuff
    {
        public bool TryUse(PlayerStats stats);
    }
}