using Microsoft.AspNetCore.Mvc;
using MyFirstAPI.Entities;

namespace MyFirstAPI.Controllers;

public class DeviceController : MyFirstApiBaseController
{
    [HttpGet]
    public IActionResult Get()
    {
        var laptop = new Laptop();

        var model = laptop.GeModel();
        var brand = laptop.GetBrand();

        var key = GetCustomKey();

        return Ok(key);
    }
}
