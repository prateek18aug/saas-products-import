namespace SaasProductImporter.Models
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using YamlDotNet.Serialization;

    public class Product
    {
        [JsonIgnore]
        [YamlMember(Alias = "tags")]
        public string Tags { get; set; }
        [YamlIgnore]
        public List<string> Categories { get; set; }
        [YamlMember(Alias = "twitter")]
        public string Twitter { get; set; }
        [YamlMember(Alias = "name")]
        public string Title { get; set; }
    }
}
