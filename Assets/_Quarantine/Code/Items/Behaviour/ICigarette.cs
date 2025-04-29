namespace _Quarantine.Code.Items.Behaviour
{
    public interface ICigarette : IUsableStuff
    {
        public bool IsGlowing { get; }
        public void LightItUp(IFireSource fireSource);
        public void PutOut();
    }
}