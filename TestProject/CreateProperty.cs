using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class CreateProperty
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void OwnreNoExist()
        {
            API.Business.Propertys property = new();
            var result = property.CreateProperty(new Entities.OCreateProperty { 
                Name = "test",
                Address = "test",
                Price = 0,
                CodeInternal = "ds",
                Year = 2000,
                IdOwner = 999,
            });
            Assert.AreEqual(result, API.Enumn.ECreateProperty.Owner_Not_Exists);
        }
    }
}
