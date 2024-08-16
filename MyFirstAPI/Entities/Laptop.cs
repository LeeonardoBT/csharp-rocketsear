namespace MyFirstAPI.Entities;

public sealed class Laptop : Device
{
    public string GeModel()
    {
        var isConnected = IsConnected();

        if (isConnected)
            return "MacBook";

        return "Unknow";
    }

    public override string GetBrand()
    {
        return "Apple";
    }

    public override string Hello()
    {
        return "Hey Siri";
    }
}
