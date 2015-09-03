using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SerPro.API.Controllers
{
    [RoutePrefix("api/translations")]
    public class TranslationsController : ApiController
    {
        [Route("Get")]
        public IHttpActionResult Get(string lang)
        {
            var resourceObject = new JObject();

            var resourceSet = Resources.Resources.ResourceManager.GetResourceSet(new CultureInfo(lang), true, true);
            IDictionaryEnumerator enumerator = resourceSet.GetEnumerator();
            while (enumerator.MoveNext())
            {
                resourceObject.Add(enumerator.Key.ToString(), enumerator.Value.ToString());
            }

            return Ok(resourceObject);
        }
    }
}
