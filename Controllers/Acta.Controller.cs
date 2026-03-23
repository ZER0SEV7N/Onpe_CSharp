using Microsoft.Extensions.Configuration;
using Onpe.Datos;
using Microsoft.AspNetCore.Mvc;

namespace Onpe.Controllers
{
    //Controlador para manejar las vistas relacionada con las actas de votacion.
    public class ActaController : Controller
    {
        private readonly daoGrupoVotacion _daoGrupoVotacion;

        public ActaController(daoGrupoVotacion daoGrupoVotacion)
        {
            _daoGrupoVotacion = daoGrupoVotacion;
        }

        //GET: Acta/Numero
        //Get: Acta/Numero/{id} -> id = idGrupoVotacion
        public IActionResult Numero(string id)
        {
            if (string.IsNullOrEmpty(id)) return View();

            var acta = _daoGrupoVotacion.getActa(id);
            if (acta == null || !acta.Valido)
            {
                ViewBag.Error = "El número de acta ingresado no existe, por favor vuelva a intentarlo";
                return View();
            }

            return View(acta);
        }

        //Get: Acta/Ubigeo 
        public IActionResult Ubigeo()
        { 
            return View(); 
        }

        //Endpoints para cargar los datos de ubigeo
        //JQuery y Ajax
        [HttpGet] public JsonResult CargarDepartamentos(string ambito)
        {
            if (ambito == "Extranjero")
                return Json(_daoGrupoVotacion.getDepartamentos(26, 30));
            else
                return Json(_daoGrupoVotacion.getDepartamentos(1, 25));
        }
        [HttpGet] public JsonResult CargarProvincias(int id) => Json(_daoGrupoVotacion.getProvincias(id));
        [HttpGet] public JsonResult CargarDistritos(int id) => Json(_daoGrupoVotacion.getDistritos(id));
        [HttpGet] public JsonResult CargarLocales(int id) => Json(_daoGrupoVotacion.getLocales(id));
        [HttpGet] public JsonResult CargarMesas(int id) => Json(_daoGrupoVotacion.getGrupoVotacion(id));

        //Cargar el Detalle para las actas.
        [HttpGet] public IActionResult ObtenerDetalle(string id)
        {
            var acta = _daoGrupoVotacion.getActa(id);
            if (acta == null || !acta.Valido) return NotFound();

            return PartialView("_ActaDetalle", acta);
        }
    }
}
