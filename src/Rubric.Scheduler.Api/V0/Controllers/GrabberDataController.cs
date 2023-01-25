using Microsoft.AspNetCore.Mvc;

namespace Rubric.Scheduler.Api.V0.Controllers;

[ApiExplorerSettings(IgnoreApi = false)]
[Produces("application/json")]
[ApiController]
[Route("api/v{version:apiVersion}/grabber-data")]
public class GrabberDataController : ControllerBase
{
    public GrabberDataController ()
    {
        
    }
}