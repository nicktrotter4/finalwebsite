using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Web; 
using System.Web.Mvc; 
using System.IO; 
using CsvHelper; 
using WebApplication4.Models; 
 
namespace WebApplication4.Controllers 
{ 
public class CustomController : Controller 
{ 
public ActionResult ContractorWebmasters() 
 { 
ViewBag.CompanyName = "Contractor Webmasters";
ViewBag.Address = "3309 Winthrop Ave. suite 77";
return View(); 
 } 
public ActionResult EastfieldCollege() 
 { 
ViewBag.CompanyName = "Eastfield College";
ViewBag.Address = "3737 Motley Dr";
return View(); 
 } 
public ActionResult AirTexHVAC() 
 { 
ViewBag.CompanyName = "AirTex HVAC";
ViewBag.Address = "";
return View(); 
 } 
public ActionResult ExtremeComfortAirConditioningandHeating() 
 { 
ViewBag.CompanyName = "Extreme Comfort Air Conditioning and Heating";
ViewBag.Address = "3320 Wiley Post Rd";
return View(); 
 } 
}}