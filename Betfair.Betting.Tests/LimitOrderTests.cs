﻿namespace Betfair.Betting.Tests
{
    using System.Globalization;
    using Xunit;

    public class LimitOrderTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2147483648)]
        public void SelectionIdIsSetInConstructor(long selectionId)
        {
            var sut = new LimitOrder(selectionId, Side.Back, 1, 1.01);
            Assert.Equal(selectionId, sut.SelectionId);
        }

        [Theory]
        [InlineData(Side.Back)]
        [InlineData(Side.Lay)]
        public void SideCanBeSetInConstructor(Side side)
        {
            var sut = new LimitOrder(1, side, 1, 1.01);
            Assert.Equal(side, sut.Side);
        }

        [Theory]
        [InlineData(1.99)]
        [InlineData(10.77)]
        [InlineData(3.3333333)]
        public void SizeCanBeSetInConstructor(double size)
        {
            var sut = new LimitOrder(1, Side.Back, size, 1.01);
            Assert.Equal(size, sut.Size);
        }

        [Theory]
        [InlineData(1.01)]
        [InlineData(1000)]
        [InlineData(3.4567)]
        public void PriceCanBeSetInConstructor(double price)
        {
            var sut = new LimitOrder(1, Side.Back, 1, price);
            Assert.Equal(price, sut.Price);
        }

        [Theory]
        [InlineData(12345, Side.Back, 1.99, 1.01)]
        [InlineData(98765, Side.Lay, 10.99, 1000)]
        [InlineData(2147483648, Side.Back, 7.24, 3.5)]
        [InlineData(12345, Side.Lay, 1.99, 1.01)]
        public void ToInstructionReturnCorrectJsonString(long selectionId, Side side, double size, double price)
        {
            var sut = new LimitOrder(selectionId, side, size, price);
            var expected = $"{{\"selectionId\":\"{selectionId}\"," +
                           $"\"side\":\"{side.ToString().ToUpper(CultureInfo.CurrentCulture)}\"," +
                           $"\"orderType\":\"LIMIT\"," +
                           $"\"limitOrder\":{{" +
                           $"\"size\":\"{size}\"," +
                           $"\"price\":\"{price}\"," +
                           $"\"persistenceType\":\"LAPSE\"}}}}";
            Assert.Equal(expected, sut.ToInstruction());
        }

        [Theory]
        [InlineData(1.999, 2)]
        [InlineData(3.3333333, 3.33)]
        [InlineData(1.555, 1.56)]
        [InlineData(10.6666, 10.67)]
        public void ToInstructionRoundsSizeToTwoDecimalsPlaces(double size, double rounded)
        {
            var sut = new LimitOrder(1, Side.Back, size, 1.01);
            var expected = $"{{\"selectionId\":\"1\"," +
                           $"\"side\":\"BACK\"," +
                           $"\"orderType\":\"LIMIT\"," +
                           $"\"limitOrder\":{{" +
                           $"\"size\":\"{rounded}\"," +
                           $"\"price\":\"1.01\"," +
                           $"\"persistenceType\":\"LAPSE\"}}}}";
            Assert.Equal(expected, sut.ToInstruction());
        }

        [Fact]
        public void ToInstructionRoundsPriceToNearestValidPrice()
        {
            var sut = new LimitOrder(1, Side.Back, 1, 1);
            var nearest = 1.01;
            var expected = $"{{\"selectionId\":\"1\"," +
                           $"\"side\":\"BACK\"," +
                           $"\"orderType\":\"LIMIT\"," +
                           $"\"limitOrder\":{{" +
                           $"\"size\":\"1\"," +
                           $"\"price\":\"{nearest}\"," +
                           $"\"persistenceType\":\"LAPSE\"}}}}";
            Assert.Equal(expected, sut.ToInstruction());
        }
    }
}
