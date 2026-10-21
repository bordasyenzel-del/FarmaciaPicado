using FarmaciaPicado.Entidades;
using System.Net.Http;
using System.Text.Json;

namespace FarmaciaPicado.Servicios
{
    public class ApiService
    {
        // servicio web REST que vamos a consumir
        private const string UrlTipoCambio = "https://open.er-api.com/v6/latest/USD";

        // cliente HTTP
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> ObtenerJsonAsync(string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> ConsultarServicioAsync()
        {
            return await ObtenerJsonAsync(UrlTipoCambio);
        }

        public async Task<TipoCambio> ObtenerTipoCambioAsync()
        {
            string json = await ConsultarServicioAsync();
            return JsonSerializer.Deserialize<TipoCambio>(json);
        }
    }
}