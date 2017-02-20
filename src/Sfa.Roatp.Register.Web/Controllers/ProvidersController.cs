using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Controllers;
using Sfa.Roatp.Register.Web.Attributes;
using SFA.ROATP.Types;
using Swashbuckle.Swagger.Annotations;

namespace Sfa.Roatp.Register.Web.Controllers
{
    public class ProvidersController : ApiController
    {
        [SwaggerOperation("GetAll")]
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(IEnumerable<RoatpProvider>))]
        [Route("providers")]
        [ExceptionHandling]
        public IEnumerable<RoatpProvider> Get()
        {
            return new List<RoatpProvider>();
        }
    }
}
