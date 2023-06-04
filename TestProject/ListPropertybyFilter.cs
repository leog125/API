using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class ListPropertybyFilter
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ListProperty()
        {
            API.Business.Propertys property = new();
            var result = property.ListPropertybyFilter(new Entities.OPropertyFilter
            {
                IdProperty = 1,
                Name = "test",
                Year = 2000,
                IdOwner = 1
            });
            Assert.IsTrue(result.Count>0);
        }
    }
}
