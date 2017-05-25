﻿using WordHunt.Data.Events;

namespace WordHunt.WebAPI.Hubs
{
    /// <summary>
    /// Actions that can be invoke on the client from server/.
    /// </summary>
    public interface IEventClient
    {
        void TeamChanged(TeamChanged args);
    }
}