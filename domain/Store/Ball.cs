using Store.Data;
using System.Text.RegularExpressions;

namespace Store;
public class Ball
{
    private readonly BallDto dto;

    public int Id => dto.Id;

    public string Name
    {
        get => dto.Name;
        set => dto.Name = value?.Trim();
    }

    public string Brand
    {
        get => dto.Brand;
        set
        {
            if (TryFormatBrandOrModel(value, out string formattedBrandOrModel))
                dto.Brand = formattedBrandOrModel;
            throw new ArgumentException(nameof(Brand));
        }
    }

    public string Model
    {
        get => dto.Model;
        set
        {
            if (TryFormatBrandOrModel(value, out string formattedBrandOrModel))
                dto.Model = formattedBrandOrModel;
            throw new ArgumentException(nameof(Model));
        }
    }

    public string Description
    {
        get => dto.Description;
        set => dto.Description = value;
    }

    public decimal Price
    {
        get => dto.Price;
        set => dto.Price = value;
    }


    internal Ball(BallDto dto)
    {
        this.dto = dto;
    }

    public static bool TryFormatBrandOrModel(string brandOrModel, out string formattedBrandOrModel)
    {
        if (brandOrModel == null)
        {
            formattedBrandOrModel = null;
            return false;
        }
        formattedBrandOrModel = brandOrModel.Replace("-", "")
                            .Replace(" ", "")
                            .ToUpper();
        return true;
    }

    public static bool IsBrandOrModel(string brandOrModel)
            => TryFormatBrandOrModel(brandOrModel, out _);

    public static class DtoFactory
    {
        public static BallDto Create(string name,
                                     string brand,
                                     string model,
                                     string description,
                                     decimal price)
        {
            if (TryFormatBrandOrModel(brand, out string formattedBrandOrModel))
                brand = formattedBrandOrModel;
            else
                throw new ArgumentException(nameof(brand));

            if (TryFormatBrandOrModel(model, out formattedBrandOrModel))
                model = formattedBrandOrModel;
            else
                throw new ArgumentException(nameof(model));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));
            return new BallDto
            {
                Name = name.Trim(),
                Brand = brand.Trim(),
                Model = model.Trim(),
                Description = description?.Trim(),
                Price = price,
            };
        }
    }
    public static class Mapper
    {
        public static Ball Map(BallDto dto) => new Ball(dto);

            public static BallDto Map(Ball domain) => domain.dto;
    }
}
