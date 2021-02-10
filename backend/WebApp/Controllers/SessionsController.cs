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
    builder.EntitySet<Session>("Sessions");
    builder.EntitySet<Hall>("Halls"); 
    builder.EntitySet<Movy>("Movies"); 
    builder.EntitySet<Ticket>("Tickets"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class SessionsController : ODataController
    {
        private Model1 db = new Model1();

        // GET: odata/Sessions
        [EnableQuery]
        public IQueryable<Session> GetSessions()
        {
            return db.Sessions;
        }

        // GET: odata/Sessions(5)
        [EnableQuery]
        public SingleResult<Session> GetSession([FromODataUri] int key)
        {
            return SingleResult.Create(db.Sessions.Where(session => session.id == key));
        }

        // PUT: odata/Sessions(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Session> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Session session = await db.Sessions.FindAsync(key);
            if (session == null)
            {
                return NotFound();
            }

            patch.Put(session);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(session);
        }

        // POST: odata/Sessions
        public async Task<IHttpActionResult> Post(Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sessions.Add(session);
            await db.SaveChangesAsync();

            return Created(session);
        }

        // PATCH: odata/Sessions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Session> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Session session = await db.Sessions.FindAsync(key);
            if (session == null)
            {
                return NotFound();
            }

            patch.Patch(session);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(session);
        }

        // DELETE: odata/Sessions(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Session session = await db.Sessions.FindAsync(key);
            if (session == null)
            {
                return NotFound();
            }

            db.Sessions.Remove(session);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Sessions(5)/Hall1
        [EnableQuery]
        public SingleResult<Hall> GetHall1([FromODataUri] int key)
        {
            return SingleResult.Create(db.Sessions.Where(m => m.id == key).Select(m => m.Hall1));
        }

        // GET: odata/Sessions(5)/Movy
        [EnableQuery]
        public SingleResult<Movy> GetMovy([FromODataUri] int key)
        {
            return SingleResult.Create(db.Sessions.Where(m => m.id == key).Select(m => m.Movy));
        }

        // GET: odata/Sessions(5)/Tickets
        [EnableQuery]
        public IQueryable<Ticket> GetTickets([FromODataUri] int key)
        {
            return db.Sessions.Where(m => m.id == key).SelectMany(m => m.Tickets);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SessionExists(int key)
        {
            return db.Sessions.Count(e => e.id == key) > 0;
        }
    }
}
