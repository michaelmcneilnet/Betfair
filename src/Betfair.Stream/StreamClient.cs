﻿using Betfair.Stream.Messages;

namespace Betfair.Stream;

public class StreamClient : IDisposable
{
    private readonly Pipeline _client;
    private int _requestId;
    private bool _disposedValue;

    public StreamClient() =>
        _client = new Pipeline();

    public StreamClient(System.IO.Stream stream) =>
        _client = new Pipeline(stream);

    public Task Authenticate(string appKey, string sessionToken)
    {
        _requestId++;
        var authMessage = new Authentication
        {
            Id = _requestId,
            AppKey = appKey,
            Session = sessionToken,
        };

        return _client.Write(authMessage);
    }

    public Task Subscribe(MarketFilter marketFilter, DataFilter dataFilter)
    {
        _requestId++;
        var subscriptionMessage = new SubscriptionMessage
        {
            Op = "marketSubscription",
            Id = _requestId,
            MarketFilter = marketFilter,
            MarketDataFilter = dataFilter,
        };

        return _client.Write(subscriptionMessage);
    }

    public Task SubscribeToOrders()
    {
        _requestId++;
        var subscriptionMessage = new SubscriptionMessage
        {
            Op = "orderSubscription",
            Id = _requestId,
        };

        return _client.Write(subscriptionMessage);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue) return;
        if (disposing) _client.Dispose();

        _disposedValue = true;
    }
}
