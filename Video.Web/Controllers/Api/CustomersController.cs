using Microsoft.AspNetCore.Mvc;
using Video.Application.Interfaces;
using Video.Domain.Entities;

namespace Video.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customer;
        public CustomersController(ICustomerRepository customer)
        {
            _customer = customer;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await _customer.GetAllCustomersAsync(includeProperties: c => c.MembershipType);
            return Ok(customers);
        }

        // GET api/customers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var customer = await _customer.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // POST api/customers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await _customer.AddAsync(customer);
            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cutomerInDb = await _customer.GetCustomerByIdAsync(id);
            if (cutomerInDb == null)
                return NotFound();

            cutomerInDb.FirstName = customer.FirstName;
            cutomerInDb.LastName = customer.LastName;
            cutomerInDb.Birthdate = customer.Birthdate;
            cutomerInDb.IsSubscribed = customer.IsSubscribed;
            cutomerInDb.MembershipTypeId = customer.MembershipTypeId;

            await _customer.UpdateAsync(cutomerInDb);

            return Ok(cutomerInDb);
        }

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Customer customerIdb = await _customer.GetCustomerByIdAsync(id);

            if (customerIdb == null)
                return NotFound();

            await _customer.DeleteCustomerByIdAsync(customerIdb.Id); 

            return NoContent();
        }
    }
}
