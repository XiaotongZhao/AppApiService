using AppApiService.Common;
using AppApiService.Domain.DynamicRequestDataService;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace AppApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicController : ControllerBase
    {
        private static LogService logService = new LogService();
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

        [HttpPost, Route("TestDynamicData")]
        public string TestDynamicData(dynamic data)
        {
            var jsonObject = JObject.Parse(data.ToString());
            var finalData = new JObject();
            var dataMap = new Dictionary<string, string>();
            dataMap.Add("Name", "MapName");
            dataMap.Add("Age", "MapAge");
            dataMap.Add("Detail", "MapDetail");
            dataMap.Add("Description", "MapDescription");
            dynamicDataService.MapDynamicData(jsonObject, dataMap, finalData);
            return finalData.ToString();
        }

        [HttpPost, Route("TestLog")]
        public void TestLog(LogInfor logInfor)
        {
            logService.EnqueueLog(logInfor.LogInformation);
        }

    }
}
