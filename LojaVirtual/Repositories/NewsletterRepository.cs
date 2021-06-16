using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace LojaVirtual.Repositories
{
    public class NewsletterRepository : INewsletterRepository
    {
        private LojaVirtualContext _banco;

        public NewsletterRepository(LojaVirtualContext banco) => _banco = banco;

        public void Cadastrar(NewsletterEmail newsletter)
        {
            _banco.NewsletterEmails.Add(newsletter);
            _banco.SaveChanges();
        }

        public IEnumerable<NewsletterEmail> ObterTodasNewsletter() => _banco.NewsletterEmails.ToList();
    }
}
