using API.Enumn;
using API.Models;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;

namespace API.Business
{
    public class Propertys
    {
        public Test_MAUContext db = new();
        public ECreateProperty CreateProperty(OCreateProperty property)
        {
            if (db.Owners.Find(property.IdOwner) == null)
                return ECreateProperty.Owner_Not_Exists;
            else
            {
                Property property_ = new()
                {
                    IdOwner = property.IdOwner,
                    Name = property.Name,
                    Address = property.Address,
                    Price = property.Price,
                    CodeInternal = property.CodeInternal,
                    Year = property.Year
                };
                db.Properties.Add(property_);
                db.SaveChanges();
                return ECreateProperty.Created;
            }
        }

        public EAddImageProperty AddImageProperty(OAddImage image)
        {
            if (db.Properties.Find(image.IdProperty) == null)
                return EAddImageProperty.Property_Not_Exists;
            else if (db.PropertyImages.Find(image.IdProperty) != null)
                return EAddImageProperty.Property_With_Image;
            else if (string.IsNullOrEmpty(image.Image_Base64))
                return EAddImageProperty.Not_Image;
            else
            {
                PropertyImage image_ = new()
                {
                    IdProperty = image.IdProperty,
                    File = image.Image_Base64,
                    Enable = image.Enable
                };
                db.PropertyImages.Add(image_);
                db.SaveChanges();
                return EAddImageProperty.Add_Image;
            }
        }

        public EChangePriceProperty ChangePriceProperty(OChangePrice changePrice)
        {
            Property? property = db.Properties.Find(changePrice.IdProperty);
            if (property == null)
                return EChangePriceProperty.Property_Not_Exists;
            else
            {
                property.Price = changePrice.Price;
                db.Properties.Update(property);
                db.SaveChanges();
                return EChangePriceProperty.ChangePrice;
            }
        }

        public EUpdateProperty UpdateProperty(OProperty property)
        {
            if (db.Properties.Find(property.IdProperty) == null)
                return EUpdateProperty.Property_Not_Exists;
            else if (db.Owners.Find(property.IdOwner) == null)
                return EUpdateProperty.Owner_Not_Exists;
            else
            {
                Property? Uproperty = db.Properties.Where(x => x.IdProperty == property.IdProperty).FirstOrDefault();
                Uproperty.Name = property.Name;
                Uproperty.Address = property.Address;
                Uproperty.Price = property.Price;
                Uproperty.CodeInternal = property.CodeInternal;
                Uproperty.Year = property.Year;
                db.Properties.Update(Uproperty);
                return EUpdateProperty.Update_Property;
            }
        }

        public List<OProperty> ListPropertybyFilter(OPropertyFilter filter)
        {
            List<OProperty> result = new List<OProperty>();
            List<Property> list = db.Properties.Where(x => ((filter.IdProperty > 0) && (x.IdProperty == filter.IdProperty)) ||
                                                           ((filter.IdOwner > 0) && (x.IdOwner == filter.IdProperty)) ||
                                                           ((filter.Year > 0) && (x.Year == filter.Year)) ||
                                                           (x.Name.Contains(filter.Name))).ToList();
            foreach (var item in list)
            {
                OProperty property = new()
                {
                    IdProperty = item.IdProperty,
                    IdOwner = item.IdOwner,
                    Name = item.Name,
                    Address = item.Address,
                    Price = item.Price,
                    CodeInternal = item.CodeInternal,
                    Year = item.Year
                };
                result.Add(property);
            }
            return result;
        }

    }
}
