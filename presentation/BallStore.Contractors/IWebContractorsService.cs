namespace BallStore.Contractors
{
    public interface IWebContractorsService
    {
        string UniqueCode { get; }

        string GetUri { get; }
    }
}
