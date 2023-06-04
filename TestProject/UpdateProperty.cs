using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class UpdateProperty
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void OwnerNotExists()
        {
            API.Business.Propertys property = new();
            var result = property.UpdateProperty(new Entities.OProperty
            {
                IdProperty = 1,
                Name = "test",
                Address = "test",
                Price = 0,
                CodeInternal = "ds",
                Year = 2000,
                IdOwner = 999999,
            });
            Assert.AreEqual(result, API.Enumn.EUpdateProperty.Owner_Not_Exists);
        }

        [Test]
        public void PropertyNotExists()
        {
            API.Business.Propertys property = new();
            var result = property.UpdateProperty(new Entities.OProperty
            {
                IdProperty = 99999,
                Name = "test",
                Address = "test",
                Price = 0,
                CodeInternal = "ds",
                Year = 2000,
                IdOwner = 1,
            });
            Assert.AreEqual(result, API.Enumn.EUpdateProperty.Property_Not_Exists);
        }

        [Test]
        public void Update_Property()
        {
            API.Business.Propertys property = new();
            var result = property.UpdateProperty(new Entities.OProperty
            {
                IdProperty = 1,
                Name = "test",
                Address = "test",
                Price = 0,
                CodeInternal = "ds",
                Year = 2000,
                IdOwner = 1,
            });
            Assert.AreEqual(result, API.Enumn.EUpdateProperty.Update_Property);
        }
    }
}
