using MvcEventosAWS.Models;
using System.Net.Http.Headers;

namespace MvcEventosAWS.Services
{
    public class EventosService
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public EventosService(KeysModel model)
        {
            this.UrlApi = model.ApiUrl;
            this.header = new MediaTypeWithQualityHeaderValue
                ("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(this.UrlApi + request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Evento>> GetEventosAsync()
        {
            string request = "api/eventos";
            List<Evento> data =
                await this.CallApiAsync<List<Evento>>(request);
            return data;
        }

        public async Task<List<Evento>> GetEventosCategoriaAsync(int idcategoria)
        {
            string request = "api/eventos/"+idcategoria;
            List<Evento> data =
                await this.CallApiAsync<List<Evento>>(request);
            return data;
        }

        public async Task<List<CategoriaEvento>> GetCategoriaAsync()
        {
            string request = "api/eventos/getcategorias";
            List<CategoriaEvento> data =
                await this.CallApiAsync<List<CategoriaEvento>>(request);
            return data;
        }

    }
}
