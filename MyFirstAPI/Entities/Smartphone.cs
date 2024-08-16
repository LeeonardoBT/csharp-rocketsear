namespace MyFirstAPI.Entities;

public sealed class Smartphone : Device
{
    public override string GetBrand()
    {
        return "Samsung";
    }
}
