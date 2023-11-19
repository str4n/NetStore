using NetStore.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace NetStore.Modules.Products.Api.Controllers;

[Route(ProductsModule.BasePath + "/[controller]")]
internal sealed class HomeController : BaseController
{
    [HttpGet]
    public ActionResult<string> Get() => Ok("Products Api");
}