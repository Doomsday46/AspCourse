using AspCourse.Models.security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HashingTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            HashingPassword.Init();
            HashingPassword.GetHashPassword("21673781264");
        }
    }
}
