﻿using System.Runtime.Serialization;
using Betfair.Stream;
using Utf8Json;
using Utf8Json.Resolvers;

namespace Betfair.Tests.Stream.TestDoubles
{
    [DataContract]
    public class SubscriptionMessageStub
    {
        public SubscriptionMessageStub(string operation, int id)
        {
            Operation = operation;
            Id = id;
        }

        [DataMember(Name = "op", EmitDefaultValue = false)]
        public string Operation { get; private set; }

        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; private set; }

        [DataMember(Name = "marketFilter", EmitDefaultValue = false)]
        public StreamMarketFilter StreamMarketFilter { get; private set; }

        [DataMember(Name = "marketDataFilter", EmitDefaultValue = false)]
        public MarketDataFilter MarketDataFilter { get; private set; }

        [DataMember(Name = "initialClk", EmitDefaultValue = false)]
        public string InitialClock { get; private set; }

        [DataMember(Name = "clk", EmitDefaultValue = false)]
        public string Clock { get; private set; }

        public SubscriptionMessageStub WithMarketFilter(StreamMarketFilter streamMarketFilter)
        {
            StreamMarketFilter = streamMarketFilter;
            return this;
        }

        public SubscriptionMessageStub WithMarketDateFilter(MarketDataFilter marketDataFilter)
        {
            MarketDataFilter = marketDataFilter;
            return this;
        }

        public SubscriptionMessageStub WithInitialClock(string initialClock)
        {
            InitialClock = initialClock;
            return this;
        }

        public SubscriptionMessageStub WithClock(string clock)
        {
            Clock = clock;
            return this;
        }

        public string ToJson()
        {
            return JsonSerializer.ToJsonString(this, StandardResolver.ExcludeNull);
        }
    }
}
