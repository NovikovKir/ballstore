namespace BallStore.Contractors
{
    public interface IWebContractorsService
    {
        string Name { get; }

        Uri StartSession(IReadOnlyDictionary<string, string> parameters, Uri returnUri);
    }
}
