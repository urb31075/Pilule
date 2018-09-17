// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateNUnit.cs" company="urb31075">
// All Righr Reserved  
// </copyright>
// <summary>
//   Defines the TemplateNUnit type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PiluleDataProviderUnitTest
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NUnit.Framework;
    using PiluleDataProvider;
    using Unity;
    using Assert = NUnit.Framework.Assert;

    /// <summary>
    /// The template n unit.
    /// </summary>
    [TestFixture]
    public class TemplateNUnit
    {
        /// <summary>
        /// The pilule data provider.
        /// </summary>
        private readonly IPiluleDataProvider piluleDataProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateNUnit"/> class.
        /// </summary>
        public TemplateNUnit()
        {
            var uc = new UnityContainer();
            this.piluleDataProvider = uc.Resolve<FakeDataProvider>();
        }

        /// <summary>
        /// The test method 1.
        /// </summary>
        [Test]
         public void TestMethod1()
         {
            Assert.IsNotNull(this.piluleDataProvider);
        }

        /// <summary>
        /// The test method 2.
        /// </summary>
        [Test]
        public void TestMethod2()
        {
            var data = this.piluleDataProvider.GetData(null);
            Assert.IsNotNull(data);
        }

        /// <summary>
        /// The test method 3.
        /// </summary>
        [Test]
        public void TestMethod3()
        {
            var data = this.piluleDataProvider.GetData(null);
            Assert.IsTrue(data.Any(c => c.Name.Contains("Fake")));
        }

        /// <summary>
        /// The test method 4.
        /// </summary>
        [Test]
        public void TestMethod4()
        {
            MyAssert.Throws<NotImplementedException>(
                () =>
                    {
                        this.piluleDataProvider.NopOperation();
                    },
                @"Nop operation not Implemented in FakeDataPrivider");
        }
    }

    /// <summary>
    /// The my assert.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1204:StaticElementsMustAppearBeforeInstanceElements", Justification = "Reviewed. Suppression is OK here.")]
    public static class MyAssert
    {
        /// <summary>
        /// The throws.
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <param name="expectedExceptionMessage">
        /// The expected exception message.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <exception cref="AssertFailedException">
        /// </exception>
        public static void Throws<T>(Action func, string expectedExceptionMessage) where T : Exception
        {
            var exceptionThrown = false;
            try
            {
                func.Invoke();
            }
            catch (T ex)
            {
                if (ex.Message == expectedExceptionMessage)
                {
                    exceptionThrown = true;
                }
            }

            if (!exceptionThrown)
            {
                throw new AssertFailedException($"An exception of type {typeof(T)} was expected, but not thrown");
            }
        }
    }
}
