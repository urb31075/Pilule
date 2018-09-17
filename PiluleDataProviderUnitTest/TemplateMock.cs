// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateMock.cs" company="urb31075">
// All Right Reserved  
// </copyright>
// <summary>
//   Defines the TemplateMock type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PiluleDataProviderUnitTest
{
    using System.Collections.ObjectModel;
    using System.Linq;

    using Moq;

    using NUnit.Framework;

    using PiluleDataProvider;

    /// <summary>
    /// The template mock.
    /// </summary>
    [TestFixture]
    public class TemplateMock
    {
        /// <summary>
        /// The test method 1.
        /// </summary>
        [Test]
        public void TestMethod1()
        {
            var mock = new Mock<IPiluleDataProvider>();
            mock.Setup(a => a.GetGood(null)).Returns(new BaskeеtData(null) { Id = 0, Name = "Mock Good №5", Amount = 5, Price = 100 });

            var mo = mock.Object;

            // var result = Operation(...mock.Object...)
            // Assert (...result...)
            var data = mo.GetGood(null); 
            Assert.IsTrue(data.Name.Contains("Mock"));
        }

        /// <summary>
        /// The test method 2.
        /// </summary>
        [Test]
        public void TestMethod2()
        {
            var mock = new Mock<IPiluleDataProvider>();
            mock.Setup(a => a.GetData(null)).Returns(new ObservableCollection<BaskeеtData>
                                                         {
                                                             new BaskeеtData(null) { Id = 0, Name = "Mock Good №1", Amount = 1, Price = 2 },
                                                             new BaskeеtData(null) { Id = 1, Name = "Mock Good №2", Amount = 2, Price = 4 },
                                                             new BaskeеtData(null) { Id = 2, Name = "Mock Good №3", Amount = 3, Price = 8 },
                                                             new BaskeеtData(null) { Id = 3, Name = "Mock Good №4", Amount = 4, Price = 16 }
                                                         });

            var mo = mock.Object;

            // var result = Operation(...mock.Object...)
            // Assert (...result...)
            var data = mo.GetData(null);
            Assert.IsTrue(data.Any(c => c.Name.Contains("Mock")));
        }
    }
}
