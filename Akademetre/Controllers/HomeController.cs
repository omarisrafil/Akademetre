using Akademetre.Models;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Akademetre.Controllers
{

    public class HomeController : Controller
    {
        private DataSet dataset;
        private DataTable table;
        private List<Person> people;

        public void InitializeData() {

            dataset = HttpContext.Application["DataSet"] as DataSet;
            table = HttpContext.Application["Table"] as DataTable;
            DataColumn column;

            column = new DataColumn("ID", Type.GetType("System.Int32"))
            {
                ReadOnly = true,
                Unique = true,
                AutoIncrement = true,
                AutoIncrementSeed = 1
            };
            table.Columns.Add(column);

            column = new DataColumn("Name", Type.GetType("System.String"))
            {
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            table.Columns.Add(column);

            column = new DataColumn("Surname", Type.GetType("System.String"))
            {
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            table.Columns.Add(column);

            column = new DataColumn("Address", Type.GetType("System.String"))
            {
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            table.Columns.Add(column);

            column = new DataColumn("Email", Type.GetType("System.String"))
            {
                AutoIncrement = false,
                ReadOnly = false,
                Unique = false
            };
            table.Columns.Add(column);

            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = table.Columns["ID"];
            table.PrimaryKey = PrimaryKeyColumns;

            dataset.Tables.Add(table);
        }

        public ActionResult Index(Person person)
        {
            dataset = HttpContext.Application["DataSet"] as DataSet;
            table = HttpContext.Application["Table"] as DataTable;
            DataRow row;

            if (dataset.Tables.Count == 0)
            {
                InitializeData();
            }

            if (!person.IsNull())
            {
                row = table.NewRow();
                row["Name"] = person.Name;
                row["Surname"] = person.Surname;
                row["Address"] = person.Address;
                row["Email"] = person.Email;

                table.Rows.Add(row);
            }

            people = (from rw in table.AsEnumerable()
                      select new Person()
                      {
                          ID = Convert.ToInt32(rw["ID"]),
                          Name = Convert.ToString(rw["Name"]),
                          Surname = Convert.ToString(rw["Surname"]),
                          Email = Convert.ToString(rw["Email"]),
                          Address = Convert.ToString(rw["Address"])
                      }).ToList();

            ModelState.Clear();
            HomeViewModel model = new HomeViewModel() { People = people };

            return View(model);
        }

        public FileResult ExportData()
        {
            dataset = HttpContext.Application["DataSet"] as DataSet;

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataset);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "worksheet.xlsx");
                }
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}