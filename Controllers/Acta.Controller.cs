using Microsoft.Extensions.Configuration;
using Onpe.Datos;
using Microsoft.AspNetCore.Mvc;

namespace Onpe.Controllers
{
    //Controlador para manejar las vistas relacionada con las actas de votacion.
    public class ActaController : Controller
    {
        private readonly daoGrupoVotacion daoGV;

        public ActaController(IConfiguration configuration)
        {
            daoGV = new daoGrupoVotacion(configuration);
        }

        //GET: Acta/Numero
        //Get: Acta/Numero/{id} -> id = idGrupoVotacion
        public IActionResult Numero(string id)
        {
            if (string.IsNullOrEmpty(id)) return View();

            var acta = daoGV.getActa(id);
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
                return Json(daoGV.getDepartamentos(26, 30));
            else
                return Json(daoGV.getDepartamentos(1, 25));
        }
        [HttpGet] public JsonResult CargarProvincias(int id) => Json(daoGV.getProvincias(id));
        [HttpGet] public JsonResult CargarDistritos(int id) => Json(daoGV.getDistritos(id));
        [HttpGet] public JsonResult CargarLocales(int id) => Json(daoGV.getLocales(id));
        [HttpGet] public JsonResult CargarMesas(int id) => Json(daoGV.getGrupoVotacion(id));

        //Cargar el Detalle para las actas.
        [HttpGet] public IActionResult ObtenerDetalle(string id)
        {
            var acta = daoGV.getActa(id);
            if (acta == null || !acta.Valido) return NotFound();

            return PartialView("_ActaDetalle", acta);
        }
    }
}
