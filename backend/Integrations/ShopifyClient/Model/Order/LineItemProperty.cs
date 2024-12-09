using Newtonsoft.Json;

namespace POD.Integrations.ShopifyClient.Model.Order
{
    public class LineItemProperty
    {
        /// <summary>
        /// The name of the note attribute.
        /// </summary>
        [JsonProperty("name")] public object Name { get; set; }

        /// <summary>
        /// The value of the note attribute.
        /// </summary>
        [JsonProperty("value")] public object Value { get; set; }
    }
}