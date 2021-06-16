using LojaVirtual.Models;
using System.Collections.Generic;

namespace LojaVirtual.Repositories.Contracts
{
    interface IColaboradorRepository
    {
        Colaborador Login(string email, string senha);

        void Cadastrar(Colaborador colaborador);
        void Atualizar(Colaborador colaborador);
        void Excluir(int id);

        Colaborador ObterColaborador(int id);
        IEnumerable<Colaborador> ObterTodosColaboradores();
    }
}
