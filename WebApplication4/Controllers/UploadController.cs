using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using WebApplication4.Models;
using CsvHelper;
using System.Text.RegularExpressions;
using System.Net;


namespace WebApplication4.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpGet]
        public ActionResult UploadFile()
        {
           
            return View();
        }
        

        [HttpPost]
       

        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);
                    string website = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/website.txt"));

                    List<ClientstoDisplay> clientstoDisplays = new List<ClientstoDisplay>();
                    Clients client = new Clients();
                    WebClient newclient = new WebClient();
                    Stream stream = newclient.OpenRead("http://localhost:63392/UploadedFiles/website.csv");
                    var csv = new CsvReader(new StreamReader(stream));
                    var Clientlist = csv.GetRecords<Clients>();

                    foreach (var c in Clientlist)
                    {
                        ClientstoDisplay clientstoDisplay = new ClientstoDisplay();
                        clientstoDisplay.Name = c.Name;
                        clientstoDisplay.Phone = c.Phone;
                        clientstoDisplay.City = c.City;
                        clientstoDisplay.Address = c.Address;
                        clientstoDisplay.Email = c.Email;
                        clientstoDisplay.Category = c.Category;
                        clientstoDisplay.Zip = c.Zip;

                        clientstoDisplays.Add(clientstoDisplay);

                    }

                    var lines = System.IO.File.ReadAllLines((Server.MapPath("~/Controllers/CustomController.cs")));
                    if (lines.Length > 0)
                    {
                        System.IO.File.Delete((Server.MapPath("~/Controllers/CustomController.cs")));
                    }
                    string appendData1 = "using System; \n";
                    appendData1 += "using System.Collections.Generic; \n";
                    appendData1 += "using System.Linq; \n"; 
                    appendData1 += "using System.Web; \n";
                    appendData1 += "using System.Web.Mvc; \n";
                    appendData1 += "using System.IO; \n";
                    appendData1 += "using CsvHelper; \n";
                    appendData1 += "using WebApplication4.Models; \n \n";
                    appendData1 += "namespace WebApplication4.Controllers \n";
                    appendData1 += "{ \n";
                    appendData1 += "public class CustomController : Controller \n";
                    appendData1 += "{ \n";



                    System.IO.File.AppendAllText((Server.MapPath("~/Controllers/CustomController.cs")), appendData1);


                    foreach (var c in clientstoDisplays)
                    {
                        if (!System.IO.File.Exists(Regex.Replace(c.Name, @"\s+", "")))
                        {
                            System.IO.File.Create(Server.MapPath("~/Views/Custom/" + Regex.Replace(c.Name, @"\s+", "") + ".cshtml")).Close();
                            
                            
                        }
                        if (c.Name != "")
                        {
                            string appendData = "public ActionResult " + Regex.Replace(c.Name, @"\s+", "") + "() \n { \n";
                            appendData += "ViewBag.CompanyName = " + "\"" + c.Name + "\"" + ";\n";
                            appendData += "ViewBag.Address = " + "\"" + c.Address + "\"" + ";\n";
                            appendData += "return View();";
                            appendData += " \n } \n";
                            System.IO.File.AppendAllText((Server.MapPath("~/Controllers/CustomController.cs")), appendData);

                        }
                    }

                    foreach (var c in clientstoDisplays)
                    {
                        
                       System.IO.File.AppendAllText((Server.MapPath("~/Views/Custom/" + Regex.Replace(c.Name, @"\s+", "") + ".cshtml")), website);
                    }
                    System.IO.File.AppendAllText((Server.MapPath("~/Controllers/CustomController.cs")), "}}"); 



                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
        }
        

    }
}