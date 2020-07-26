namespace Betfair.Betting.Tests.TestDoubles.Responses
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Betfair.Betting.Tests.TestDoubles.Requests;
    using Utf8Json;
    using Utf8Json.Resolvers;

    public class PlaceExecutionReportStub
    {
        public PlaceExecutionReportStub(string marketId, string status)
        {
            this.MarketId = marketId;
            this.Status = status;
        }

        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; }

        [DataMember(Name = "errorCode", EmitDefaultValue = false)]
        public string ErrorCode { get; set; }

        [DataMember(Name = "marketId", EmitDefaultValue = false)]
        public string MarketId { get; }

        [DataMember(Name = "instructionReports", EmitDefaultValue = false)]
        public IList<PlaceInstructionReportStub> InstructionReports { get; private set; }

        [DataMember(Name = "customerRef", EmitDefaultValue = false)]
        public string CustomerRef { get; set; }

        internal List<LimitOrder> LimitOrders { get; } = new List<LimitOrder>();

        internal void AddReport(LimitOrderBuilder order, string betId, string status, string orderStatus)
        {
            this.InstructionReports ??= new List<PlaceInstructionReportStub>();
            this.InstructionReports.Add(order.PlaceInstructionReportObject(betId, status, orderStatus));
            this.LimitOrders.Add(order.Object);
        }

        internal void AddNullReport(LimitOrderBuilder order)
        {
            this.LimitOrders.Add(order.Object);
        }

        internal void SetReturnContent(ExchangeServiceSpy exchange)
        {
            var content = JsonSerializer.ToJsonString(this, StandardResolver.ExcludeNull);
            exchange.WithReturnContent("placeOrders", content);
        }
    }
}
