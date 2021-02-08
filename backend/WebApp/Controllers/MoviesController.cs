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
using WebApp;

namespace WebApp.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    /*
    Для класса WebApiConfig может понадобиться внесение дополнительных изменений, чтобы добавить маршрут в этот контроллер. Объедините эти инструкции в методе Register класса WebApiConfig соответствующим образом. Обратите внимание, что в URL-адресах OData учитывается регистр символов.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using WebApp;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Movy>("Movies");
    builder.EntitySet<Session>("Sessions"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class MoviesController : ODataController
    {
        private Model1 db = new Model1();

        // GET: odata/Movies
        [EnableQuery]
        public IQueryable<Movy> GetMovies()
        {
            return db.Movies;
        }

        // GET: odata/Movies(5)
        [EnableQuery]
        public SingleResult<Movy> GetMovy([FromODataUri] int key)
        {
            return SingleResult.Create(db.Movies.Where(movy => movy.id == key));
        }

        // PUT: odata/Movies(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Movy> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Movy movy = await db.Movies.FindAsync(key);
            if (movy == null)
            {
                return NotFound();
            }

            patch.Put(movy);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(movy);
        }

        // POST: odata/Movies
        public async Task<IHttpActionResult> Post(Movy movy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Movies.Add(movy);
            await db.SaveChangesAsync();

            return Created(movy);
        }

        // PATCH: odata/Movies(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Movy> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Movy movy = await db.Movies.FindAsync(key);
            if (movy == null)
            {
                return NotFound();
            }

            patch.Patch(movy);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(movy);
        }

        // DELETE: odata/Movies(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Movy movy = await db.Movies.FindAsync(key);
            if (movy == null)
            {
                return NotFound();
            }

            db.Movies.Remove(movy);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Movies(5)/Sessions
        [EnableQuery]
        public IQueryable<Session> GetSessions([FromODataUri] int key)
        {
            return db.Movies.Where(m => m.id == key).SelectMany(m => m.Sessions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovyExists(int key)
        {
            return db.Movies.Count(e => e.id == key) > 0;
        }
    }
}
