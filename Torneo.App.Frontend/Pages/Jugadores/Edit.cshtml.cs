using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Dominio;
using Torneo.App.Persistencia;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Torneo.App.Frontend.Pages.Jugadores
{
    public class EditModel : PageModel
    {
        private readonly IRepositorioJugador _repoJugador;
        private readonly IRepositorioEquipo _repoEquipo;
        private readonly IRepositorioPosicion _repoPosicion;

        public Jugador jugador { get; set; }
        public SelectList EquipoOptions { get; set; }
        public SelectList PosicionOptions { get; set; }
        public int EquipoSelected { get; set; }
        public int PosicionSelected { get; set; }


        public EditModel(IRepositorioJugador repoJugador, IRepositorioEquipo
        repoEquipo, IRepositorioPosicion repoPosicion)
        {
            _repoJugador = repoJugador;
            _repoEquipo = repoEquipo;
            _repoPosicion = repoPosicion;
        }

        public IActionResult OnGet(int id)
        {
            jugador = _repoJugador.GetJugador(id);
            EquipoOptions = new SelectList(_repoEquipo.GetAllEquipos(), "Id", "Nombre");
            EquipoSelected = jugador.Equipo.Id;
            PosicionOptions = new SelectList(_repoPosicion.GetAllPosiciones(), "Id", "Nombre");
            EquipoSelected = jugador.Equipo.Id;
            PosicionSelected = jugador.Posicion.Id;
            if (jugador == null)
            {
                return NotFound();
            }
            else
            {
                return Page();
            }
        }

        public IActionResult OnPost(Jugador jugador, int idEquipo, int idPosicion)
        {
            _repoJugador.UpdateJugador(jugador, idEquipo, idPosicion);
            return RedirectToPage("Index");
        }

    }
}
