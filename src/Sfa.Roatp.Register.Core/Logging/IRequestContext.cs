namespace Sfa.Roatp.Register.Core.Logging
{
    public interface IRequestContext
    {
        string Url { get; }
        string IpAddress { get; }
    }
}