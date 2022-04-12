using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Alerts
{
    public class AlertItem
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("location")]
        public string Location { get; set; }

        [BsonElement("startDate")]
        public DateTime StartDate { get; set; }

        [BsonElement("endDate")]
        public DateTime EndDate { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }
    }
}