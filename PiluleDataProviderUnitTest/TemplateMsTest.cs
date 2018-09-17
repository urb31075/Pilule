// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateMsTest.cs" company="urb31075">
//  All Right Reserved 
// </copyright>
// <summary>
//   Defines the TemplateMsTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PiluleDataProviderUnitTest
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using PiluleDataProvider;

    using Unity;

    /// <summary>
    /// The template ms test.
    /// </summary>

    [TestClass]
    public class TemplateMsTest
    {
        /// <summary>
        /// The pilule data provider.
        /// </summary>
        private readonly IPiluleDataProvider piluleDataProvider;

        public TemplateMsTest()
        {
            var uc = new UnityContainer();
            this.piluleDataProvider = uc.Resolve<FakeDataProvider>();
        }

        /// <summary>
        /// The test method 1.
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(true);
        }

        /// <summary>
        /// The test method 2.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestMethod2()
        {
            try
            {
                this.piluleDataProvider.NopOperation();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Nop operation not Implemented in FakeDataPrivider"), ex.Message);
                throw;
            }
        }
    }
}
