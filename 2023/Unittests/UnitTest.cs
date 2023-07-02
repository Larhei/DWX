namespace Unittests
{
    [TestClass]
    public class UnitTest
    {
        static UnitTest()
        {
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
            {
                throw new NotSupportedException();
            }
        }

        [TestInitialize]
        public void Setup()
        {
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
            {
                throw new NotSupportedException();
            }
        }

        [TestMethod]
        public void TestMethod()
        {
            Assert.AreEqual(ApartmentState.STA, Thread.CurrentThread.GetApartmentState());
        }
    }
}
