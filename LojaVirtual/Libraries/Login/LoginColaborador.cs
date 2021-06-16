using LojaVirtual.Libraries.Session;
using LojaVirtual.Models;
using Newtonsoft.Json;

namespace LojaVirtual.Libraries.Login
{
    public class LoginColaborador
    {
        private string Key = "Login.Colaborador";
        private Sessao _sessao;

        public LoginColaborador(Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Colaborador colaborador)
        {
            var colaboradorJSONString = JsonConvert.SerializeObject(colaborador);
            _sessao.Cadastrar(Key, colaboradorJSONString);
        }

        public Colaborador Get()
        {
            if (_sessao.Existe(Key))
            {
                var colaboradorJSONString = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<Colaborador>(colaboradorJSONString);
            }
            return null;
        }

        public void Logout()
        {
            _sessao.RemoverTodos();
        }
    }
}
