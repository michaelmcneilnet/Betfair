﻿namespace Betfair.Api.Responses.Markets;

public class RunnerResponse
{
    /// <summary>
    /// Gets the unique id of the runner (selection).
    /// Please note - the same selectionId and runnerName pairs are used across all Betfair markets which contain them.
    /// </summary>
    public long SelectionId { get; internal set; }

    /// <summary>
    /// Gets the handicap of the selection.
    /// </summary>
    public double Handicap { get; internal set; }

    /// <summary>
    /// Gets the status of the selection.
    /// For example ACTIVE, REMOVED, WINNER, PLACED, LOSER, HIDDEN.
    /// RunnerResponse status information is available for 90 days following market settlement.
    /// </summary>
    public string Status { get; internal set; } = string.Empty;

    /// <summary>
    /// Gets the adjustment factor applied if the runner is removed.
    /// </summary>
    public double? AdjustmentFactor { get; internal set; }

    /// <summary>
    /// Gets the price of the most recent bet matched on this selection.
    /// </summary>
    public double? LastPriceTraded { get; internal set; }

    /// <summary>
    /// Gets the total amount matched on this runner.
    /// </summary>
    public double? TotalMatched { get; internal set; }

    /// <summary>
    /// Gets the date and time the runner was removed.
    /// </summary>
    public DateTimeOffset? RemovalDate { get; internal set; }

    /// <summary>
    /// Gets the BSP related prices for this runner.
    /// </summary>
    [JsonPropertyName("sp")]
    public StartingPrices? StartingPrices { get; internal set; }

    /// <summary>
    /// Gets the exchange prices available for this runner.
    /// </summary>
    [JsonPropertyName("ex")]
    public ExchangePrices? ExchangePrices { get; internal set; }
}
