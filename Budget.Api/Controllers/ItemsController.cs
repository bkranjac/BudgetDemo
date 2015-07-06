using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Budget.Domain.Models;
using Budget.Data.Interfaces;
using Budget.Data.Concrete;
using System.Web.Http.Description;

namespace Budget.Api.Controllers
{
    public class ItemsController : ApiController
    {
        IItemRepository _itemRepository = new ItemRepository();

        public ItemsController(IItemRepository itemRepository)
         {
             this._itemRepository = itemRepository;          
         }

        // GET: api/Items
        public IHttpActionResult Get()
        {
            var result = _itemRepository.GetAll();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);

        }

        // GET: api/Items/5
        public IHttpActionResult Get(int id)
        {
            var item = _itemRepository.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);

        }

        // POST: api/Items
        [ResponseType(typeof(BudgetItem))]
        public IHttpActionResult Post(BudgetItem item)
        {
            if (ModelState.IsValid)
            {
                _itemRepository.CreateItem(item);
                return CreatedAtRoute("DefaultApi", new { id = item.Id }, item);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Items/5
        public IHttpActionResult Put(int id, BudgetItem item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BudgetItem oldItem = _itemRepository.GetItem(id);
            if (oldItem == null)
            {
                _itemRepository.CreateItem(item);
                return CreatedAtRoute("DefaultApi", new { id = item.Id }, item);
            }
            else
            {
                _itemRepository.UpdateItem(item);
               // return Content(HttpStatusCode.Accepted, item);
                return Ok(item);
            }

        }

        // DELETE: api/Items/5
        public IHttpActionResult Delete(int id)
        {
            BudgetItem item = _itemRepository.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }
            _itemRepository.DeleteItem(id);
            return Ok(item);


        }
    }
}
