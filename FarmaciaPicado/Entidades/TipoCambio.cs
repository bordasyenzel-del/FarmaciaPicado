using System.Text.Json.Serialization;

namespace FarmaciaPicado.Entidades
{
    public class TipoCambio
    {
        [JsonPropertyName("base_code")]
        public string Base { get; set; }

        [JsonPropertyName("rates")]
        public Dictionary<string, decimal> Rates { get; set; }
    }
}