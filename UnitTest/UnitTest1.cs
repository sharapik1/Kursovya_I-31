using FakeDataProviderLib;
using KatyaRyrs.Class;
using KatyaRyrs.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KatyaRyrs.Test
{
    [TestClass]
    public class UnitTest1
    {
        [ClassInitialize]
        static public void Init(TestContext tc)
        {
            Globals.dataProvider = new FakeDataProvider();
        }

        [TestMethod]
        public void Save_AddProductWithoutName_Error()
        {
            Product ProductTest = new Product()
            {
                Name = ""
            };
            try
            {
                ProductTest.Save();
                Assert.Fail();
            }
            catch 
            { 

            }
            
        }
        [TestMethod]
        public void Save_AddCurrentProductTypeNotFilled_Error()
        {
            Product CurrentProductTypeTest = new Product()
            {
                CurrentProductType = null
            };
            try
            {
                CurrentProductTypeTest.Save();
                Assert.Fail();
            }
            catch
            {

            }
        }
        [TestMethod]
        public void Save_AddPriceLassAndNotNull_Error()
        {
            Product PriceTest = new Product()
            {
                Price = 0
            };
            try
            {
                PriceTest.Save();
                Assert.Fail();
            }
            catch
            {

            }

        }
        [TestMethod]
        public void Save_AddWeightProductNotFilled_Error()
        {
            Product WeightTest = new Product()
            {
                Weight = ""
            };
            try
            {
                WeightTest.Save();
                Assert.Fail();
            }
            catch
            {

            }
        }
        [TestMethod]
        public void Save_AddNumberProductLassAndNotNull_Error()
        {
            Product NumberTest = new Product()
            {
                Number = 0
            };
            try
            {
                NumberTest.Save();
                Assert.Fail();
            }
            catch
            {

            }
        }
        [TestMethod]
        public void Delete_DeleteNotProduct_Error()
        {
            Product IDTest = new Product()
            {
                ID = 0
            };
            try
            {
                IDTest.Delete();
                Assert.Fail();
            }
            catch
            {

            }
        }



    }
}
