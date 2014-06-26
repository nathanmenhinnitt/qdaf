namespace qdaf.Framework.UnitTests
{
    using Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Threading.Tasks;

    [TestClass]
    public class PacketReceiverTests
    {
        public PacketReceiver Receiver { get; set; }

        public Mock<IPacketValidator> MockPacketValidator { get; set; }
        public Mock<IPacketProcessor> MockPacketProcessor { get; set; }
        public Mock<IQdafLogger> MockLogger { get; set; }

        [TestInitialize]
        public void Init()
        {
            MockPacketValidator = new Mock<IPacketValidator>();
            MockPacketProcessor = new Mock<IPacketProcessor>();
            MockLogger = new Mock<IQdafLogger>();
            Receiver = new PacketReceiver(MockPacketValidator.Object, MockPacketProcessor.Object, MockLogger.Object);
        }

        [TestMethod]
        public async Task should_receive_and_validate_empty_packet_and_process()
        {
            var data = new DataPacket
                {
                    Success = false,
                    Commands = new IPacketCommand[0]
                };

            await Receiver.ProcessPacketAsync(data);

            Assert.AreEqual(true, data.Success);

            MockPacketValidator.Verify(x => x.Validate(It.Is<DataPacket>(a => a == data)), Times.Once);
            MockPacketProcessor.Verify(x => x.ProcessAsync(It.Is<DataPacket>(a => a == data)), Times.Once);
            LogAsserter.VerifyNoExcptions(MockLogger);
        }
    }
}
