using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
    public class ExceptionController : BaseApiController
    {
        [HttpGet]
        [Route("internal-server-Error")]
        public void ThrowInternalServerError()
        {
            try
            {
                throw new Exception();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
