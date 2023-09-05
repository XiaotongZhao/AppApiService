using AppApiService.Domain.DynamicRequestDataService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicController : ControllerBase
    {
        private readonly IDynamicDataService dynamicDataService;
        public DynamicController(IDynamicDataService dynamicDataService)
        {
            this.dynamicDataService = dynamicDataService;
        }

        [HttpPost, Route("TestPostDynamicJsonMapJson")]
        public async Task<string> TestPostDynamicJsonMapJson(dynamic data)
        {
            JObject json = JObject.Parse(data.ToString());
            var jsonObject = await dynamicDataService.TestMapJsonObject(json);
            var jsonToString = jsonObject.ToString();
            XmlDocument doc = JsonConvert.DeserializeXmlNode(jsonToString, "PushTravelApplyOrder_1_1") ?? new XmlDocument();
            return doc.InnerXml.ToString();
        }
        
        [HttpPost, Route("AddDataMaps")]
        public async Task<bool> AddDataMaps(List<DataMap> dataMaps)
        {
            var res = await dynamicDataService.AddDataMaps(dataMaps);
            return res;
        }

        [HttpGet, Route("GetDataMapList")]
        public async Task<List<DataMap>> GetDataMapList()
        {
            var res = await dynamicDataService.GetDataMapList();
            return res;
        }
    }
}
