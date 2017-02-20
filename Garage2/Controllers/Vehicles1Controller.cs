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
    public class Vehicles1Controller : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: Vehicles1
        public ActionResult Index()
        {
            var vehicles = db.Vehicles.Include(v => v.Member).Include(v => v.VehicleType);
            return View(vehicles.ToList());
        }

        // GET: Vehicles1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles1/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Name");
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "VehicleTypeId", "Type");
            return View();
        }

        // POST: Vehicles1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RegNumber,Color,Manufacturer,Model,NumberOfWheels,ParkAt,VehicleTypeId,MemberId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Name", vehicle.MemberId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "VehicleTypeId", "Type", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Name", vehicle.MemberId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "VehicleTypeId", "Type", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // POST: Vehicles1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RegNumber,Color,Manufacturer,Model,NumberOfWheels,ParkAt,VehicleTypeId,MemberId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Name", vehicle.MemberId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "VehicleTypeId", "Type", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
            return RedirectToAction("Index");
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
