namespace qdaf.Framework.UnitTests.TransactionTests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Fakes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TimeoutTests
    {
        [TestMethod]
        public async Task should_timeout()
        {
            var transaction = new Transaction(50, new DefaultTimer(), new LongDelayFakeTransactionProcessors(250));

            var packet = new DataPacket();

            var result = await transaction.BeginTransactionAsync(packet);

            Assert.AreEqual(TransactionStaus.Timeout, result.Status);
            Assert.IsNotNull(result.Exceptions);
            Assert.IsNotNull(result.TransactionId);
            Assert.IsTrue(result.Exceptions.Any());
        }
    }
}
