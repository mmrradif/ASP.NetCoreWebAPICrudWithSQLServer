using CoreAPICrud.Context;
using CoreAPICrud.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreAPICrud.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {

        private readonly ContactAPIDbContext DbContext;
        public ContactsController(ContactAPIDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await DbContext.Contacts.ToListAsync());
        }


        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await DbContext.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact()
            {
                Id=Guid.NewGuid(),
                Address=addContactRequest.Address,
                Email=addContactRequest.Email,
                FullName=addContactRequest.FullName,
                Phone=addContactRequest.Phone
            };

            await DbContext.Contacts.AddAsync(contact);
            await DbContext.SaveChangesAsync();

            return Ok(contact);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact=await DbContext.Contacts.FindAsync(id);

            if (contact != null)
            {
                contact.Address = updateContactRequest.Address;
                contact.Email = updateContactRequest.Email;
                contact.FullName=updateContactRequest.FullName;
                contact.Phone=updateContactRequest.Phone;

                await DbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();       
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await DbContext.Contacts.FindAsync(id);

            if (contact != null)
            {
                DbContext.Remove(contact);
                await DbContext.SaveChangesAsync();

                return Ok(contact); 
            }
            return NotFound();
        }

    }
}
