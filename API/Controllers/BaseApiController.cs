using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   [ApiController] // bind DTOs automatically and do the validations automatically
    [Route("api/[controller]")] 
    
    public class BaseApiController : ControllerBase
    {
        
    }
}