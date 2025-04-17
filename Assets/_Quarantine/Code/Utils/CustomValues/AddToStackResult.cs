namespace _Quarantine.Code.Utils.CustomValues
{
    public readonly struct AddToStackResult
    {
        public readonly int addedAmount;
        public readonly int remainingAmount;

        public AddToStackResult(int addedAmount, int remainingAmount)
        {
            this.addedAmount = addedAmount;
            this.remainingAmount = remainingAmount;
        }
    }
}