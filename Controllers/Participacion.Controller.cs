using Microsoft.AspNetCore.Mvc;
using Onpe.Datos;
using Microsoft.Extensions.Configuration;
namespace Onpe.Controllers
{
    //Controlador para manejar las vistas relacionada con la participacion electoral.
    public class ParticipacionController : Controller
    {
        private readonly daoParticipacion _daoParticipacion;

        private readonly string[] continentes = { "AFRICA", "AMERICA", "ASIA", "EUROPA", "OCEANIA" };
        //Constructor para inicializar la conexion a la base de datos
        public ParticipacionController(daoParticipacion daoParticipacion)
        {
            _daoParticipacion = daoParticipacion;
        }

        public IActionResult Inicial()
        {
            var listaCompleta = _daoParticipacion.getNacional(1, 30);
            return View(listaCompleta);
        }

        //Get: Participacion/Ambito/{id ? Nacional : Extranjero}
        public IActionResult Ambito(string id = "Nacional")
        {
            ViewBag.Titulo = "NACIONAL";
            ViewBag.Columna = id == "Extranjero" ? "CONTINENTE" : "DEPARTAMENTO";
            ViewBag.SiguienteNivel = "Departamentos";

            var lista = id == "Extranjero" ? _daoParticipacion.getNacional(26, 30) : _daoParticipacion.getNacional(1, 25);
            return View("Resumen", lista);
        }

        //Get: Participacion/Departamentos/{Departamento}
        //Cuenta tanto nacional como extranjero
        public IActionResult Departamentos(string id)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Nacional");

            ViewBag.Titulo = id;
            ViewBag.Columna = continentes.Contains(id.ToUpper()) ? "PAÍS" : "PROVINCIA";
            ViewBag.SiguienteNivel = "Provincia";

            return View("Resumen", _daoParticipacion.getPorDepartamento(id));
        }

        //Get: Participacion/Provincia/{Provincia}
        public IActionResult Provincia(string id)
        {
            ViewBag.Titulo = id;
            ViewBag.Columna = "DISTRITO / CIUDAD";
            ViewBag.SiguienteNivel = "Distrito"; 

            return View("Resumen", _daoParticipacion.getPorProvincia(id));
        }

        //Get: Participacion/Distrito/{Distrito}
        public IActionResult Distrito(string id)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Nacional");

            ViewBag.Titulo = id;
            ViewBag.Columna = "LOCAL DE VOTACIÓN";
            ViewBag.SiguienteNivel = ""; 

            return View("Resumen", _daoParticipacion.getPorDistrito(id));
        }
    }
}
