using BusinessLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppLayer3.Controllers
{
    public class DisponibilidadDocenteController : Controller
    {
        // GET: DisponibilidadDocente
        public ActionResult Index()
        {
            List<Docente> listaDocentes = DataBridge.Instancia.obtenerListadoDocentes();
            ViewBag.listaDocentes = listaDocentes;
            return View("DisponibilidadDocente");
        }

        public ActionResult ObtenerDisponibilidadDocente(int idDocente, int idPeriodo)
        {
            DisponibilidadDocente disponibilidad = DataBridge.Instancia.obtenerDisponibilidadDocente(idDocente, idPeriodo);
            return Json(disponibilidad, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarDisponibilidadDocente(DisponibilidadDocente disponibilidad)
        {
            int resultado = DataBridge.Instancia.guardarDisponibilidadDocente(disponibilidad);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }
}