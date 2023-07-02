using System.Runtime.Versioning;

namespace Unittests
{
    [SupportedOSPlatform("windows")]
    public class STATestMethodAttribute : TestMethodAttribute
    {
        private readonly TestMethodAttribute? _testMethodAttribute;

        public STATestMethodAttribute()
        {
        }

        public STATestMethodAttribute(TestMethodAttribute testMethodAttribute)
        {
            _testMethodAttribute = testMethodAttribute;
        }

        public override TestResult[] Execute(ITestMethod testMethod)
        {
            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
            {
                return Invoke(testMethod);
            }

            TestResult[] result = Array.Empty<TestResult>();
            var thread = new Thread(() => result = Invoke(testMethod));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            return result;
        }

        private TestResult[] Invoke(ITestMethod testMethod)
        {
            if (_testMethodAttribute != null)
            {
                return _testMethodAttribute.Execute(testMethod);
            }

            return new[] { testMethod.Invoke(null) };
        }
    }
}
