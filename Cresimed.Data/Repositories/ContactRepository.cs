using System;
using System.Collections.Generic;
using System.Text;
using Cresimed.Core.Interfaces;
using Cresimed.Core.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq;
using Cresimed.Core.Entities.Database;
using Cresimed.Core.Entities.Base;
using Microsoft.Data.SqlClient;

namespace Cresimed.Data.Repositories
{
    public class ContactRepository : GenericRepository<Contact>,IContactRepository
    {
        private readonly CresimedDBContext _context;
        public ContactRepository(CresimedDBContext context) : base(context)
        {
            _context = context;
        }
        public void DeleteContact(int id)
        {
            Contact Contact = _context.Contacts.Where(x => x.ContactID == id).SingleOrDefault();
            _context.Remove(Contact);
            _context.SaveChanges();
        }
        public List<Contact> GetAll()
        {
            return _context.Contacts.ToList();

        }
        public Contact SaveContact(Contact Contact)
        {
            _context.Contacts.Add(Contact);
            _context.SaveChanges();
            return Contact;
        }
        public Contact GetById(int id)
        {
            var p = _context.Contacts.Where(x => x.ContactID == id).SingleOrDefault();

            return p;
        }
        public PaginatedList<Contact> GetAllFiltered(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        { 
            var Contacts = _context.Contacts.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                Contacts = Contacts.Where(s => s.Name.ToLower().Replace("à", "a")
                .Replace("â", "a")
                .Replace("ä", "a")
                .Replace("ç", "c")
                .Replace("é", "e")
                .Replace("è", "e")
                .Replace("ê", "e")
                .Replace("ë", "e")
                .Replace("î", "i")
                .Replace("ï", "i")
                .Replace("ô", "o")
                .Replace("ù", "u")
                .Replace("û", "u")
                .Replace("ü", "u").Contains(searchString));
            }
            switch (sortOrder)
            {

                case "ID":
                    Contacts = Contacts.OrderBy(s => s.ContactID);
                    break;
                case "ID_desc":
                    Contacts = Contacts.OrderByDescending(s => s.ContactID);
                    break;
                case "Contact":
                    Contacts = Contacts.OrderBy(s => s.Name);
                    break;
                case "Contact_desc":
                    Contacts = Contacts.OrderByDescending(s => s.Name);
                    break;
                default:
                    Contacts = Contacts.OrderByDescending(s => s.ContactID);
                    break;
            }

            int pageSize = 20;
            return PaginatedList<Contact>.Create(Contacts, pageNumber ?? 1, pageSize);

        }
        public int TotalContactsCount()
        {
            return _context.Contacts.Count();
        }
        public int TotalFilteredCount(string searchString)
        {
            var Contacts = _context.Contacts.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                Contacts = Contacts.Where(s => s.Name.Contains(searchString));
            }
            return Contacts.Count();

        }

        public List<Contact> GetLast5()
        {
            return _context.Contacts.OrderByDescending(x=>x.ContactID).Take(5).ToList();

        }
    }
}
