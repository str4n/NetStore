using Microsoft.AspNetCore.Mvc;

namespace NetStore.Shared.Infrastructure.Api;

[ApiController]
[ProducesJsonContentType]
public abstract class BaseController : ControllerBase
{
    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is null)
        {
            return NotFound();
        }

        return Ok(model);
    }
}