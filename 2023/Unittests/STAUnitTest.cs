namespace Unittests
{
    //[TestClass]

    [STATestClass]
    public class STAUnitTest
    {
        static STAUnitTest()
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
        public void TestTestmethodWithTestAttribute()
        {
            // To make it work use STATestClassAttriute and not TestClassAttriute.
            Assert.AreEqual(ApartmentState.STA, Thread.CurrentThread.GetApartmentState());
        }

        [STATestMethod]
        public void TestTestmethodWithSTATestAttribute()
        {
            Assert.AreEqual(ApartmentState.STA, Thread.CurrentThread.GetApartmentState());
        }
    }
}
