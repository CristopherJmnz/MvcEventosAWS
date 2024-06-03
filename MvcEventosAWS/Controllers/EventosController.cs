using Microsoft.AspNetCore.Mvc;
using MvcEventosAWS.Models;
using MvcEventosAWS.Services;

namespace MvcEventosAWS.Controllers
{
    public class EventosController : Controller
    {
        private EventosService service;
        public EventosController(EventosService service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<Evento> eventos = await this.service.GetEventosAsync();
            return View(eventos);
        }

        public async Task<IActionResult> Categorias()
        {
            List<CategoriaEvento> categorias = await this.service.GetCategoriaAsync();
            return View(categorias);
        }

        public async Task<IActionResult> EventosCategoria(int idcategoria)
        {
            List<Evento> eventos = await this.service.GetEventosCategoriaAsync(idcategoria);
            return View(eventos);
        }
    }
}
