using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Alerts
{
    public class AlertsHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var alerts = GetAllAlerts();
            var json = JsonConvert.SerializeObject(alerts, Formatting.Indented);
            context.Response.Write(json);
        }

        public IEnumerable<object> GetAllAlerts()
        {
            var client = new MongoClient(ConfigurationManager.AppSettings["MongoConnectionString"]);
            var db = client.GetDatabase("alerts");
            var col = db.GetCollection<BsonDocument>("items");
            var list = col.Find(Builders<BsonDocument>.Filter.Empty).ToList();

            var result = list.Select(x => new
            {
                type = x["type"].AsString,
                location = x["location"].AsString,
                startDate = x["startDate"].ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"),
                endDate = x["endDate"].ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"),
                text = x["text"].AsString
            });

            return result;
        }

        public bool IsReusable { get { return false; } }
    }
}