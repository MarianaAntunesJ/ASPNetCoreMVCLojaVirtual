using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Session;
using LojaVirtual.Models;
using Newtonsoft.Json;

namespace LojaVirtual.Libraries.Login
{
    public class LoginCliente
    {
        private string Key = "Login.Cliente";
        private Sessao _sessao;

        public LoginCliente(Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Cliente cliente)
        {
            var clienteJSONString = JsonConvert.SerializeObject(cliente);
            _sessao.Cadastrar(Key, clienteJSONString);
        }

        public Cliente Get()
        {
            if (_sessao.Existe(Key))
            {
                var clienteJSONString = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<Cliente>(clienteJSONString);
            }
            return null;
        }

        public void Logout()
        {
            _sessao.RemoverTodos();
        }
    }
}
 