using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using WebApp;

namespace WebApp.Controllers
{
    /*
    Для класса WebApiConfig может понадобиться внесение дополнительных изменений, чтобы добавить маршрут в этот контроллер. Объедините эти инструкции в методе Register класса WebApiConfig соответствующим образом. Обратите внимание, что в URL-адресах OData учитывается регистр символов.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using WebApp;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Hall>("Halls");
    builder.EntitySet<Cinema>("Cinemas"); 
    builder.EntitySet<Seat>("Seats"); 
    builder.EntitySet<Session>("Sessions"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class HallsController : ODataController
    {
        private Model1 db = new Model1();

        // GET: odata/Halls
        [EnableQuery]
        public IQueryable<Hall> GetHalls()
        {
            return db.Halls;
        }

        // GET: odata/Halls(5)
        [EnableQuery]
        public SingleResult<Hall> GetHall([FromODataUri] int key)
        {
            return SingleResult.Create(db.Halls.Where(hall => hall.id == key));
        }

        // PUT: odata/Halls(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Hall> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Hall hall = await db.Halls.FindAsync(key);
            if (hall == null)
            {
                return NotFound();
            }

            patch.Put(hall);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HallExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(hall);
        }

        // POST: odata/Halls
        public async Task<IHttpActionResult> Post(Hall hall)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Halls.Add(hall);
            await db.SaveChangesAsync();

            return Created(hall);
        }

        // PATCH: odata/Halls(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Hall> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Hall hall = await db.Halls.FindAsync(key);
            if (hall == null)
            {
                return NotFound();
            }

            patch.Patch(hall);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HallExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(hall);
        }

        // DELETE: odata/Halls(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Hall hall = await db.Halls.FindAsync(key);
            if (hall == null)
            {
                return NotFound();
            }

            db.Halls.Remove(hall);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Halls(5)/Cinema1
        [EnableQuery]
        public SingleResult<Cinema> GetCinema1([FromODataUri] int key)
        {
            return SingleResult.Create(db.Halls.Where(m => m.id == key).Select(m => m.Cinema1));
        }

        // GET: odata/Halls(5)/Seats
        [EnableQuery]
        public IQueryable<Seat> GetSeats([FromODataUri] int key)
        {
            return db.Halls.Where(m => m.id == key).SelectMany(m => m.Seats);
        }

        // GET: odata/Halls(5)/Sessions
        [EnableQuery]
        public IQueryable<Session> GetSessions([FromODataUri] int key)
        {
            return db.Halls.Where(m => m.id == key).SelectMany(m => m.Sessions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HallExists(int key)
        {
            return db.Halls.Count(e => e.id == key) > 0;
        }
    }
}
