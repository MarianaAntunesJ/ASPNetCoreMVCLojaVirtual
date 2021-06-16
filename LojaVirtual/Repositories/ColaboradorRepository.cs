using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace LojaVirtual.Repositories
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private LojaVirtualContext _banco;

        public ColaboradorRepository(LojaVirtualContext banco)
        {
            _banco = banco;
        }

        public void Cadastrar(Colaborador colaborador)
        {
            _banco.Add(colaborador);
            _banco.SaveChanges();
        }

        public void Atualizar(Colaborador colaborador)
        {
            _banco.Update(colaborador);
            _banco.SaveChanges();
        }
                
        public void Excluir(int id)
        {
            _banco.Remove(ObterColaborador(id));
            _banco.SaveChanges();
        }

        public Colaborador Login(string email, string senha)
            => _banco.Colaboradores.Where(_ => _.Email == email && _.Senha == senha).FirstOrDefault();

        public Colaborador ObterColaborador(int id) => _banco.Colaboradores.Find(id);

        public IEnumerable<Colaborador> ObterTodosColaboradores() => _banco.Colaboradores.ToList();
    }
}
