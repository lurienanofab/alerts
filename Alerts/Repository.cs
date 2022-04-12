using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;

namespace Alerts
{
    public class Repository
    {
        private readonly IMongoClient _client;
        
        public Repository()
        {
            _client = new MongoClient(ConfigurationManager.AppSettings["MongoConnectionString"]);
        }
        
        private IMongoCollection<AlertItem> GetCollection()
        {
            var db = _client.GetDatabase("alerts");
            var col = db.GetCollection<AlertItem>("items");
            return col;
        }
        
        public IList<AlertItem> GetAllAlerts()
        {
            return GetCollection().Find(Builders<AlertItem>.Filter.Empty).ToList();
        }
        
        public AlertItem GetAlert(object id)
        {
            var col = GetCollection();
            var filter = Builders<AlertItem>.Filter.Eq("_id", new ObjectId(id.ToString()));
            var result = col.Find(filter);
            var doc = result.First();
            return doc;
        }
        
        public void AddAlert(string alertType, string location, DateTime sd, DateTime ed, string text)
        {
            var alert = new AlertItem
            {
                Type = alertType,
                Location = location,
                StartDate = sd.ToUniversalTime(),
                EndDate = ed.ToUniversalTime(),
                Text = text
            };
            
            GetCollection().InsertOne(alert);
        }
        
        public void ModifyAlert(object id, string alertType, string location, DateTime sd, DateTime ed, string text)
        {
            var filter = Builders<AlertItem>.Filter.Eq("_id", new ObjectId(id.ToString()));
            var update = Builders<AlertItem>.Update
                .Set(x => x.Type, alertType)
                .Set(x => x.Location, location)
                .Set(x => x.StartDate , sd.ToUniversalTime())
                .Set(x => x.EndDate, ed.ToUniversalTime())
                .Set(x => x.Text, text);
                
            GetCollection().UpdateOne(filter, update);
        }
        
        public void DeleteAlert(object id)
        {
            var filter = Builders<AlertItem>.Filter.Eq("_id", new ObjectId(id.ToString()));
            GetCollection().DeleteOne(filter);
        }
        
        public void DeleteAllAlerts()
        {
            GetCollection().DeleteMany(Builders<AlertItem>.Filter.Empty);
        }
        
        public void AddAlerts(IEnumerable<AlertItem> items)
        {
            GetCollection().InsertMany(items);
        }
        
        public int ImportAlerts(string url, bool append)
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(url);
                
                var jarr = JArray.Parse(json);
                
                var list = jarr.Select(CreateAlert).ToList();
                
                if (!append)
                    DeleteAllAlerts();
                    
                AddAlerts(list);
                
                return list.Count;
            }
        }
        
        public string SerializeAlerts(IEnumerable<AlertItem> alerts)
        {
            var items = alerts.Select(x => new
            {
                type = x.Type,
                location = x.Location,
                startDate = x.StartDate.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"),
                endDate = x.EndDate.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"),
                text = x.Text
            }).ToList();
            
            var json = JsonConvert.SerializeObject(items, Formatting.Indented);
            
            return json;
        }
        
        private AlertItem CreateAlert(JToken jt)
        {
            var alertType = GetStringOrDefault(jt["type"], "info");
            var location = GetStringOrDefault(jt["location"], "menu");
            var startDate = jt["startDate"].Value<DateTime>();
            var endDate = jt["endDate"].Value<DateTime>();
            var text = jt["text"].Value<string>();
            
            var item = new AlertItem
            {
                Type = alertType,
                Location = location,
                StartDate = startDate.ToUniversalTime(),
                EndDate = endDate.ToUniversalTime(),
                Text = text
            };
            
            return item;
        }

        private string GetStringOrDefault(JToken jt, string defval)
        {
            if (jt == null)
                return defval;
            else
                return jt.Value<string>();
        }
    }
}