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
        public void TestDynamicData(dynamic data)
        {
            var jsonObject = JObject.Parse(data.ToString());
            foreach (var property in jsonObject.Properties())
            {
                var propertyName = property.Name;
                JToken propertyValue = property.Value;
                var propertyType = propertyValue.Type;
                if (propertyType == JTokenType.String)
                {
                    Console.WriteLine($"{propertyName} is a string");
                }
                else if (propertyType == JTokenType.Integer)
                {
                    Console.WriteLine($"{propertyName} is an integer");
                }
                else if (propertyType == JTokenType.Boolean)
                {
                    Console.WriteLine($"{propertyName} is a boolean");
                }
                else if (propertyType == JTokenType.Object)
                {
                    Console.WriteLine($"{propertyName} is a Object");
                }
                Console.WriteLine($"{property.Name}: {property.Value}");
            }
        }

    }
}
