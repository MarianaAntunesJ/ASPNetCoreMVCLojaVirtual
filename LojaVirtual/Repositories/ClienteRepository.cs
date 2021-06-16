using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private LojaVirtualContext _banco;

        public ClienteRepository(LojaVirtualContext banco)
        {
            _banco = banco;
        }

        public void Cadastrar(Cliente cliente)
        {
            _banco.Add(cliente);
            _banco.SaveChanges();
        }

        public void Atualizar(Cliente cliente)
        {
            _banco.Update(cliente);
            _banco.SaveChanges();
        }             

        public void Excluir(int id)
        {
            _banco.Remove(ObterCliente(id));
            _banco.SaveChanges();
        }

        public Cliente Login(string email, string senha)
            => _banco.Clientes.Where(_ => _.Email == email && _.Senha == senha).FirstOrDefault();

        public Cliente ObterCliente(int id) => _banco.Clientes.Find(id);

        public IEnumerable<Cliente> ObterTodosClientes() => _banco.Clientes.ToList();
    }
}
