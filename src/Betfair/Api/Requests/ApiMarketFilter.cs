﻿using Betfair.Core;

namespace Betfair.Api.Requests;

public sealed class ApiMarketFilter : MarketFilter<ApiMarketFilter>
{
    public DateRange? MarketStartTime { get; private set; }

    public ApiMarketFilter FromMarketStart(DateTimeOffset dateTime)
    {
        MarketStartTime ??= new DateRange();
        MarketStartTime.From = ToUtcString(dateTime);
        return this;
    }

    public ApiMarketFilter ToMarketStart(DateTimeOffset dateTime)
    {
        MarketStartTime ??= new DateRange();
        MarketStartTime.To = ToUtcString(dateTime);
        return this;
    }

    public ApiMarketFilter TodaysCard()
    {
        IncludeMarketTypes(MarketType.Win);
        IncludeEventTypes(EventType.HorseRacing);
        IncludeCountries(Country.UnitedKingdom);
        IncludeCountries(Country.Ireland);
        FromMarketStart(DateTime.Today);
        ToMarketStart(DateTime.Today.AddDays(1));

        return this;
    }

    private static string ToUtcString(DateTimeOffset dateTime) =>
        dateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", DateTimeFormatInfo.InvariantInfo);
}
