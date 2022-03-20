using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Video.Application.Interfaces;
using Video.Application.Profiles.Dtos;
using Video.Domain.Entities;

namespace Video.Web.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository _customer;
    private IMapper _mapper;
    public CustomersController(
        ICustomerRepository customer,
        IMapper mapper)
    {
        _customer = customer;
        _mapper = mapper;
    }

    // GET: api/customers
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var customers = await _customer.GetAllCustomersAsync();
        
        return Ok(customers.Select(_mapper.Map<Customer, CustomerDto>));
    }

    // GET api/customers/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var customerInDb = await _customer.GetCustomerByIdAsync(id);
        if (customerInDb == null)
            return NotFound();

        return Ok(_mapper.Map<Customer, CustomerDto>(customerInDb));
    }

    // POST api/customers
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CustomerDto customerDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var _mappedCustomer = _mapper.Map<CustomerDto, Customer>(customerDto);
        customerDto.Id = _mappedCustomer.Id;
        await _customer.AddAsync(_mappedCustomer);

        return CreatedAtAction(nameof(Get), new { id = _mappedCustomer.Id }, _mappedCustomer);
    }

    // PUT api/customers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] CustomerDto customerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var cutomerInDb = await _customer.GetCustomerByIdAsync(id);
        if (cutomerInDb == null)
            return NotFound();

        var _mappedCustomer = _mapper.Map(customerDto, cutomerInDb);
        customerDto.Id = _mappedCustomer.Id;
        await _customer.UpdateAsync(_mappedCustomer);

        return Ok(_mappedCustomer);
    }

    // DELETE api/customers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        Customer cutomerInDb = await _customer.GetCustomerByIdAsync(id);

        if (cutomerInDb == null)
            return NotFound();

        await _customer.DeleteCustomerByIdAsync(cutomerInDb.Id); 

        return NoContent();
    }
}
