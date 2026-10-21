using FarmaciaPicado.Servicios;

namespace FarmaciaPicado.Negocio
{
    public class TipoCambioNegocio
    {
        private readonly ApiService _api = new ApiService();

        public async Task<decimal> ObtenerValorDolarAsync(string moneda = "NIO")
        {
            var datos = await _api.ObtenerTipoCambioAsync();
            return datos.Rates[moneda];
        }
    }
}