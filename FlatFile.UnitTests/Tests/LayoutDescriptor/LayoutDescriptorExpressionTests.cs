using FlatFile.FixedWidth.Implementation;
using FlatFile.FixedWidth.Interfaces;
using FlatFileParserUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.LayoutDescriptor
{
    [TestClass]
    public class LayoutDescriptorExpressionTests
    {
        private readonly DummyStringPairModel examplePairModel = new DummyStringPairModel();
        private readonly int field1Length = 42;
        private readonly int idLength = 10;
        private readonly IFlatFileLayoutDescriptor<DummyStringPairModel> settings;

        public LayoutDescriptorExpressionTests()
        {
            settings = GetTestLayoutDescriptor();
        }

        [TestMethod]
        public void Should_InitializeFirstFieldLength_When_AppendFieldIsInvoked()
        {
            Assert.AreEqual(settings.GetField(0).Length, idLength);
        }

        [TestMethod]
        public void Should_InitializeFirstFieldPropertyInfo_When_AppendFieldIsInvoked()
        {
            Assert.AreEqual(settings.GetField(0).PropertyInfo, examplePairModel.GetType().GetProperty(nameof(examplePairModel.id)));
        }

        [TestMethod]
        public void Should_InitializeFirstFieldStartPosition_When_AppendFieldIsInvoked()
        {
            Assert.AreEqual(settings.GetField(0).StartPosition, 0);
        }

        [TestMethod]
        public void Should_InitializeSecondFieldLength_When_AppendFieldIsInvoked()
        {
            Assert.AreEqual(settings.GetField(1).Length, field1Length);
        }

        [TestMethod]
        public void Should_InitializeSecondFieldPropertyInfo_When_AppendFieldIsInvoked()
        {
            Assert.AreEqual(settings.GetField(1).PropertyInfo, examplePairModel.GetType().GetProperty(nameof(examplePairModel.x)));
        }

        [TestMethod]
        public void Should_InitializeSecondFieldStartPosition_When_AppendFieldIsInvoked()
        {
            // Should start at 0 + (length of first column)
            Assert.AreEqual(settings.GetField(1).StartPosition, idLength);
        }

        private IFlatFileLayoutDescriptor<DummyStringPairModel> GetTestLayoutDescriptor()
        {
            var s = new LayoutDescriptor<DummyStringPairModel>()
                .AppendField(x => x.id, idLength)
                .AppendField(x => x.x, field1Length);
            return s;
        }
    }
}