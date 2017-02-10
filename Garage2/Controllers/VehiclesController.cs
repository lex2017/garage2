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
        public ActionResult Index(bool? sortvar,string orderby, string searchString)
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

                var rgnr = db.ve.FirstOrDefault(x=>x.RegNumber==vehicle.RegNumber);
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
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = db.ve.Find(id);
            var dt = DateTime.Now.Subtract(vehicle.ParkAt);
            var cost =dt.Hours*60+dt.Minutes;
            db.ve.Remove(vehicle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Receipt(int? id)
        {
            IQueryable<Vehicle> vehicle = db.ve;

            Vehicle ve = db.ve.Find(id);
            var dt = DateTime.Now.Subtract(ve.ParkAt);
            var cost = dt.Hours * 60 + dt.Minutes;
            var model = new ReceiptViewModel
            {
                Id = ve.Id,
                ParkAt = ve.ParkAt,
                ParkOut = dt,
                Cost = cost
            };

            return View(model);

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
