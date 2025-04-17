namespace _Quarantine.Code.Utils.CustomValues
{
    public readonly struct RemoveFromStackResult
    {
        public readonly int removedAmount;
        public readonly int remainingAmount;

        public RemoveFromStackResult(int removedAmount, int remainingAmount)
        {
            this.removedAmount = removedAmount;
            this.remainingAmount = remainingAmount;
        }
    }
}