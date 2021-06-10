using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Session
{
    public class Sessao
    {
        IHttpContextAccessor _context;
        public Sessao(IHttpContextAccessor context)
        {
            _context = context;
        }

        public void Cadastrar(string key, string value) 
            => _context.HttpContext.Session.SetString(key, value);

        public void Atualizar(string key, string value)
        {
            if (Existe(key))
                _context.HttpContext.Session.Remove(key);

            _context.HttpContext.Session.SetString(key, value);
        }

        public void Remover(string key)
            => _context.HttpContext.Session.Remove(key);

        public string Consultar(string key)
            => _context.HttpContext.Session.GetString(key);

        public bool Existe(string key)
        {
            if (_context.HttpContext.Session.GetString(key) == null)
                return false;
            return true;
        }

        public void RemoverTodos()
            => _context.HttpContext.Session.Clear();
    }
}
