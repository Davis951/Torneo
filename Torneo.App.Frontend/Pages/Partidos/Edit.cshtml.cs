using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Dominio;
using Torneo.App.Persistencia;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Torneo.App.Frontend.Pages.Partidos
{
    public class EditModel : PageModel
    {
        private readonly IRepositorioPartido _repoPartido;
        private readonly IRepositorioEquipo _repoEquipo;
        public Partido partido { get; set; }
        public SelectList EquipoOptions { get; set; }
        public int LocalSelected { get; set; }
        public int VisitanteSelected { get; set; }

        public EditModel(IRepositorioPartido repoPartido, IRepositorioEquipo repoEquipo)
        {
            _repoPartido = repoPartido;
            _repoEquipo = repoEquipo;
        }


        public IActionResult OnGet(int id)
        {
            partido = _repoPartido.GetPartido(id);
            EquipoOptions = new SelectList(_repoEquipo.GetAllEquipos(), "Id", "Nombre");
            LocalSelected = partido.Local.Id;
            VisitanteSelected = partido.Visitante.Id;

            if (partido == null)
            {
                return NotFound();
            }
            else
            {
                return Page();
            }
        }

        public IActionResult OnPost(Partido partido, int idEquipoLocal, int idEquipoVisitante)
        {
            _repoPartido.UpdatePartido(partido, idEquipoLocal, idEquipoVisitante);
            return RedirectToPage("Index");
        }

    }
}
