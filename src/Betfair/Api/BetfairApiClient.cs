﻿using Betfair.Api.Requests;
using Betfair.Api.Responses;
using Betfair.Core.Client;

namespace Betfair.Api;

public class BetfairApiClient
{
    private const string _betting = "https://api.betfair.com/exchange/betting/rest/v1.0";
    private readonly BetfairHttpClient _client;

    public BetfairApiClient(BetfairHttpClient client) =>
        _client = client;

    public async Task<IReadOnlyList<MarketCatalogue>> MarketCatalogue(
        MarketCatalogueQuery? query = null,
        CancellationToken cancellationToken = default)
    {
        query ??= new MarketCatalogueQuery();
        return await _client.Post<IReadOnlyList<MarketCatalogue>>(
            new Uri($"{_betting}/listMarketCatalogue/"),
            query,
            cancellationToken);
    }

    public async Task<string> MarketStatus(
        string marketId,
        CancellationToken cancellationToken)
    {
        var response = await _client.Post<List<MarketStatus>>(
            new Uri($"{_betting}/listMarketBook/"),
            new { MarketIds = new List<string> { marketId } },
            cancellationToken);

        return response?.FirstOrDefault()?.Status ?? "NONE";
    }
}
