namespace Telpay.Business
{
    public interface IDbEntity<TKey>
    {
        TKey Id { get; }
    }
}
