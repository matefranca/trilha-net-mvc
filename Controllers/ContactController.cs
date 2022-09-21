using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using trilha_net_mvc.Context;
using trilha_net_mvc.Models;

namespace trilha_net_mvc.Controllers
{
    public class ContactController : Controller
    {
        private readonly AgendaContext _context;

        public ContactController(AgendaContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            var contacts = _context.Contacts.ToList();
            return View(contacts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Contacts.Add(contact);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(contact);
        }

        public IActionResult Edit(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null)
                return RedirectToAction(nameof(Index));

            return View(contact);
        }

        [HttpPost]
        public IActionResult Edit(Contact contact)
        {
            var contactTable  = _context.Contacts.Find(contact.Id);

            contactTable.Name = contact.Name;
            contactTable.Telefone = contact.Telefone;
            contactTable.Active = contact.Active;

            _context.Contacts.Update(contactTable);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));           
        }
    }
}