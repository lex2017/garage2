using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Garage2.DAL;
using Garage2.Models;

namespace Garage2.Controllers
{
    public class VehiclesController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: Vehicles
        public ActionResult Index(bool? sortvar, string orderby, string searchString, string VehicleTypeId, bool? parking)
        {
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "VehicleTypeId", "Type");
            IQueryable<Vehicle> ve = db.Vehicles;

            if (!String.IsNullOrEmpty(searchString))
            {
                ve = ve.Where(s => s.RegNumber.Contains(searchString) || s.Manufacturer.Contains(searchString));
                ViewBag.searchString = searchString;
            }
            if (!String.IsNullOrEmpty(VehicleTypeId))
            {
                ve = ve.Where(s => s.VehicleTypeId.ToString().Equals(VehicleTypeId));
            }
            if (parking==true)
            {
                ViewBag.Parking = "Yes";
            }
            if (orderby != null)
            {
                ViewBag.OrderBy = orderby;


                if (sortvar == true)
                {
                    ve = ve.OrderByDescending(s => s.VehicleType.Type);
                    ViewBag.flag = false;
                }
                else
                {
                    ve = ve.OrderBy(s => s.VehicleType.Type);
                    ViewBag.flag = true;
                }
            }
            return View(ve.ToList());
        }
        public ActionResult Details2(string searchString, string VehicleTypeId)
        {
            IQueryable<Vehicle> ve = db.Vehicles;
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "VehicleTypeId", "Type");
            if (!String.IsNullOrEmpty(searchString))
            {
                ve = ve.Where(s => s.RegNumber.Contains(searchString) || s.Manufacturer.Contains(searchString));
                ViewBag.searchString = searchString;
            }
            if (!String.IsNullOrEmpty(VehicleTypeId))
            {
                ve = ve.Where(s => s.VehicleTypeId.ToString().Equals(VehicleTypeId));
            }
            return View("DetailsView",ve.ToList());
        }


        // GET: Vehicles/Details/5
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

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "VehicleTypeId", "Type");
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Name");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type,RegNumber,Color,Manufacturer,Model,NumberOfWheels,VehicleTypeId,MemberId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {

                var rgnr = db.Vehicles.FirstOrDefault(x => x.RegNumber == vehicle.RegNumber);
                if (rgnr == null)
                {
                    vehicle.ParkAt = DateTime.Now;
                    db.Vehicles.Add(vehicle);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { parking = true });
                }
                else
                {
                    ViewData["Message"] = "fail";
                    ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "VehicleTypeId", "Type");
                    ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Name");
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
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Name", vehicle.MemberId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "VehicleTypeId", "Type", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,RegNumber,Color,Manufacturer,Model,NumberOfWheels,VehicleTypeId,MemberId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                var duplicate_regnumber = db.Vehicles.Any(x => x.RegNumber == vehicle.RegNumber);
                if (duplicate_regnumber)
                {
                    // En användare finns redan
                    ViewData["Message"] = "fail";
                }
                else
                {
                    vehicle.ParkAt = db.Vehicles.AsNoTracking().FirstOrDefault(z => z.Id == vehicle.Id).ParkAt;
                    db.Entry(vehicle).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Name", vehicle.MemberId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "VehicleTypeId", "Type", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
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

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            TimeSpan pTime = DateTime.Now.Subtract(vehicle.ParkAt);
            TimeSpan pTime2 = new TimeSpan(pTime.Hours, pTime.Minutes, pTime.Seconds);
            ReceiptViewModel modelresult = new ReceiptViewModel()
            {
                Id = id,
                RegNumber = vehicle.RegNumber,
                ParkAt = vehicle.ParkAt,
                ParkTime = pTime2,
                ParkOut = DateTime.Now,
                Cost = pTime.Hours * 60 + pTime.Minutes
            };
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
               return View("Receipt", modelresult);
        }

        public ActionResult Statistics()
        {
            var vehiclestypes = db.Vehicles
               .GroupBy(v => v.VehicleType)
               .Select(y => new
               {
                   Type = y.Key,
                   VehicleCount = y.Count()                   
               }).ToList();

            int numWheels = db.Vehicles.Sum(c => c.NumberOfWheels);

            double totalMinutes = 0;
            foreach (var vehicle in db.Vehicles)
                totalMinutes += Math.Round((DateTime.Now - vehicle.ParkAt).TotalMinutes);

            var vehiclestypesResult = vehiclestypes
               .Select(y => new StatisticsViewModel()
               {
                   Type = y.Type,
                   VehicleCount = y.VehicleCount,
                   WheelsTotal = numWheels,
                   CostTotal = (int)(totalMinutes * 60) / 60
               }).ToList();
            return View("Statistics", vehiclestypesResult);
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
