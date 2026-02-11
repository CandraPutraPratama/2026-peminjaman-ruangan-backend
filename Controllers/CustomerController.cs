using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2026_peminjaman_ruangan_backend.Data;
using _2026_peminjaman_ruangan_backend.Models;
using _2026_peminjaman_ruangan_backend.DTOs;

namespace _2026_peminjaman_ruangan_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/customers (daftar semua customer)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers(string? search)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Name.Contains(search) || c.Email.Contains(search));
            }

            return await query.Select(c => new CustomerDTO { 
                Id = c.Id, 
                Name = c.Name, 
                    Email = c.Email, 
                    PhoneNumber = c.PhoneNumber 
                }).ToListAsync();
        }

        // 2. POST: api/customers (untuk menambah customer baru)
        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> PostCustomer(CreateCustomerDTO dto)
        {
            var customer = new Customer { 
                Name = dto.Name, 
                Email = dto.Email, 
                PhoneNumber = dto.PhoneNumber 
            };
            
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomers), new { id = customer.Id }, 
                new CustomerDTO { Id = customer.Id, Name = customer.Name, Email = customer.Email, PhoneNumber = customer.PhoneNumber });
        }

        // 3. DELETE: api/customers/{id} (untuk menghapus customer)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound("Customernya tidak ditemukan!");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}