using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace TestProject
{
    public class ChangePriceProperty
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PropertyNoExist()
        {
            API.Business.Propertys property = new();
            var result = property.ChangePriceProperty(new Entities.OChangePrice { 
                IdProperty = 9999, 
                Price = 450000000 
            });
            Assert.AreEqual(result, API.Enumn.EChangePriceProperty.Property_Not_Exists);
        }

        [Test]
        public void ChangePrice()
        {
            API.Business.Propertys property = new();
            var result = property.ChangePriceProperty(new Entities.OChangePrice
            {
                IdProperty = 1,
                Price = 500000000
            });
            Assert.AreEqual(result, API.Enumn.EChangePriceProperty.ChangePrice);
        }
    }
}
