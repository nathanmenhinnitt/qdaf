namespace qdaf.Framework.UnitTests
{
    using System;
    using Core;
    using Moq;

    public static class LogAsserter
    {
        public static void VerifyNoExcptions(Mock<IQdafLogger> mock)
        {
            mock.Verify(x => x.Trace(It.IsAny<string>()), Times.Never);
            mock.Verify(x => x.Error(It.IsAny<string>()), Times.Never);
            mock.Verify(x => x.Error(It.IsAny<string>(), It.IsAny<Exception>()), Times.Never);
            mock.Verify(x => x.Error(It.IsAny<Exception>()), Times.Never);
        }
    }
}
