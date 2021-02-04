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
using System.Web.Http.Cors;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    /*
    Для класса WebApiConfig может понадобиться внесение дополнительных изменений, чтобы добавить маршрут в этот контроллер. Объедините эти инструкции в методе Register класса WebApiConfig соответствующим образом. Обратите внимание, что в URL-адресах OData учитывается регистр символов.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using WebApplication4.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Cinema>("Cinemas");
    builder.EntitySet<Hall>("Halls"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CinemasController : ODataController
    {
        private Model1 db = new Model1();

        // GET: odata/Cinemas
        [EnableQuery]
        public IQueryable<Cinema> GetCinemas()
        {
            return db.Cinemas;
        }

        // GET: odata/Cinemas(5)
        [EnableQuery]
        public SingleResult<Cinema> GetCinema([FromODataUri] string key)
        {
            return SingleResult.Create(db.Cinemas.Where(cinema => cinema.name == key));
        }

        // PUT: odata/Cinemas(5)
        public async Task<IHttpActionResult> Put([FromODataUri] string key, Delta<Cinema> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cinema cinema = await db.Cinemas.FindAsync(key);
            if (cinema == null)
            {
                return NotFound();
            }

            patch.Put(cinema);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CinemaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(cinema);
        }

        // POST: odata/Cinemas
        public async Task<IHttpActionResult> Post(Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cinemas.Add(cinema);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CinemaExists(cinema.name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(cinema);
        }

        // PATCH: odata/Cinemas(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<Cinema> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cinema cinema = await db.Cinemas.FindAsync(key);
            if (cinema == null)
            {
                return NotFound();
            }

            patch.Patch(cinema);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CinemaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(cinema);
        }

        // DELETE: odata/Cinemas(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            Cinema cinema = await db.Cinemas.FindAsync(key);
            if (cinema == null)
            {
                return NotFound();
            }

            db.Cinemas.Remove(cinema);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Cinemas(5)/Halls
        [EnableQuery]
        public IQueryable<Hall> GetHalls([FromODataUri] string key)
        {
            return db.Cinemas.Where(m => m.name == key).SelectMany(m => m.Halls);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CinemaExists(string key)
        {
            return db.Cinemas.Count(e => e.name == key) > 0;
        }
    }
}
