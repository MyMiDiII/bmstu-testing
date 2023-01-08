using Xunit.Sdk;
using System.Transactions;
using System.Reflection;


namespace Integrate.Utils {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ResetDatabase: BeforeAfterTestAttribute {
        private TransactionScope transactionScope;

        public ResetDatabase() {
            transactionScope = new TransactionScope();
        }

        public override void Before(MethodInfo methodUnderTest) { }

        public override void After(MethodInfo methodUnderTest) {
            transactionScope.Dispose();
        }
    }
}
