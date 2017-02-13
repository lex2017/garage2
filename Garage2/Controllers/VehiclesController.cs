using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Garage2.DAL;
using Garage2.Models;

namespace Garage2.Controllers
{
    public class VehiclesController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: Vehicles
        public ActionResult Index(bool? sortvar, string orderby, string searchString)
        {
            IQueryable<Vehicle> ve = db.ve;

            if (!String.IsNullOrEmpty(searchString))
            {
                ve = ve.Where(s => s.RegNumber.Contains(searchString));
                ViewBag.searchString = searchString;
            }

            if (orderby != null)
            {
                ViewBag.OrderBy = orderby;


                if (sortvar == true)
                {
                    ve = ve.OrderByDescending(s => s.Type);
                    ViewBag.flag = false;
                }
                else
                {
                    ve = ve.OrderBy(s => s.Type);
                    ViewBag.flag = true;
                }
            }
            return View(ve.ToList());
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.ve.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type,RegNumber,Color,Manufacturer,Model,NumberOfWheels")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {

                var rgnr = db.ve.FirstOrDefault(x => x.RegNumber == vehicle.RegNumber);
                if (rgnr == null)
                {
                    vehicle.ParkAt = DateTime.Now;
                    db.ve.Add(vehicle);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["Message"] = "fail";
                }
            }

            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.ve.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,RegNumber,Color,Manufacturer,Model,NumberOfWheels")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                vehicle.ParkAt = db.ve.AsNoTracking().FirstOrDefault(z => z.Id == vehicle.Id).ParkAt;
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.ve.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, bool iskvitto)
        {
            Vehicle vehicle = db.ve.Find(id);
            TimeSpan pTime = DateTime.Now.Subtract(vehicle.ParkAt);
            TimeSpan pTime2 = new TimeSpan(pTime.Hours, pTime.Minutes, pTime.Seconds);
            ReceiptViewModel modelresult = new ReceiptViewModel()
            {
                Id = id,
                ParkAt = vehicle.ParkAt,
                ParkTime = pTime2,
                ParkOut = DateTime.Now,
                Cost = pTime.Hours * 60 + pTime.Minutes
            };
            db.ve.Remove(vehicle);
            db.SaveChanges();
            if (iskvitto == true)
            {
                return View("Receipt", modelresult);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Statistics()
        {
            int numWheels = db.ve.Sum(c => c.NumberOfWheels);
            var vehiclestypes = db.ve
               .GroupBy(v => v.Type)
               .Select(y => new
               {
                   Type = y.Key,
                   VehicleCount = y.Count(),
                   
               }).ToList();


            var vehiclestypesResult = vehiclestypes

               .Select(y => new StatisticsViewModel()
               {
                   Type = y.Type,
                   VehicleCount = y.VehicleCount,
                   WheelsTotal = numWheels

               }).ToList();
            return View("Statistics",vehiclestypesResult);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
