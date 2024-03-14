﻿using Betfair.Api.Requests;
using Betfair.Api.Responses;
using Betfair.Core.Client;
using Betfair.Core.Login;

namespace Betfair.Api;

public class BetfairApiClient : IDisposable
{
    private const string _betting = "https://api.betfair.com/exchange/betting/rest/v1.0";
    private readonly BetfairHttpClient _httpClient;
    private readonly HttpAdapter _client;
    private readonly bool _disposeHttpClient = true;
    private bool _disposedValue;

    [ExcludeFromCodeCoverage]
    public BetfairApiClient(Credentials credentials)
    {
        ArgumentNullException.ThrowIfNull(credentials);
        _httpClient = new BetfairHttpClient(credentials.Certificate);
        var tokenProvider = new TokenProvider(_httpClient, credentials);
        _client = BetfairHttpFactory.Create(credentials, tokenProvider, _httpClient);
    }

    internal BetfairApiClient(HttpAdapter adapter)
    {
        _disposeHttpClient = false;
        _httpClient = null!;
        _client = adapter;
    }

    public async Task<IReadOnlyList<MarketCatalogue>> MarketCatalogue(
        ApiMarketFilter? filter = null,
        MarketCatalogueQuery? query = null,
        CancellationToken cancellationToken = default)
    {
        filter ??= new ApiMarketFilter();
        query ??= new MarketCatalogueQuery();
        return await _client.PostAsync<IReadOnlyList<MarketCatalogue>>(
            new Uri($"{_betting}/listMarketCatalogue/"),
            new
            {
                filter,
                query.MarketProjection,
                query.Sort,
                query.MaxResults,
            },
            cancellationToken);
    }

    public async Task<string> MarketStatus(
        string marketId,
        CancellationToken cancellationToken)
    {
        var response = await _client.PostAsync<List<MarketStatus>>(
            new Uri($"{_betting}/listMarketBook/"),
            new { MarketIds = new List<string> { marketId } },
            cancellationToken);

        return response?.FirstOrDefault()?.Status ?? "NONE";
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    [ExcludeFromCodeCoverage]
    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue) return;
        if (disposing && _disposeHttpClient) _httpClient.Dispose();

        _disposedValue = true;
    }
}
