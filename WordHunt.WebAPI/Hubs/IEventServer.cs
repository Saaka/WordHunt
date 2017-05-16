namespace WordHunt.WebAPI.Hubs
{
    /// <summary>
    /// Action than client side can invoke.
    /// </summary>
    public interface IEventServer
    {
        string Subscribe(int gameId);
    }
}
