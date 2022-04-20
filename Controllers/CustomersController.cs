using CsApiExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CsApiExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerContext _context;

        public CustomersController(CustomerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerSelectDTO>>> Get() =>
            await this._context.Customers
                .Select(x => CustomerToDTO(x))
                .ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerSelectDTO>> Get(long id)
        {
            Customer? customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            return CustomerToDTO(customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, CustomerDTO customerDTO)
        {
            Customer? customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            customer.Email = customerDTO.Email;
            customer.Password = customerDTO.Password;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CustomerExists(id))
            {              
                 return NotFound();            
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CustomerSelectDTO>> Create(CustomerDTO customerDTO)
        {
            bool exists = await _context.Customers.AnyAsync(c => c.Email == customerDTO.Email);

            if (exists)
            {
                return Conflict();
            }
            Customer customer = new Customer
            {
                Email = customerDTO.Email,
                Password = customerDTO.Password
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Get),
                new { id = customer.Id },
                CustomerToDTO(customer));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            Customer? customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(long id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

        private static CustomerSelectDTO CustomerToDTO(Customer customer) =>
            new CustomerSelectDTO()
            {
                Id = customer.Id,
                Email = customer.Email,
                Password = customer.Password
            };
    }
}