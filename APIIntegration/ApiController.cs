using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaApi.APIIntegration
{
    public class ApiController
    {
        IApiController apiController;
        public ApiController(IApiController apiController)
        {
            this.apiController = apiController;
        }

        public string ReadAllData()
        {
            return this.apiController.ReadAllData();
        }
    }
}
