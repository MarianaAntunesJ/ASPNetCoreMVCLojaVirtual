using LojaVirtual.Libraries.Email;
using LojaVirtual.Libraries.Filter;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        private IClienteRepository _repositoryCliente;
        private INewsletterRepository _repositoryNewsletter;
        private LoginCliente _loginCliente;

        public HomeController(IClienteRepository repositoryCliente, INewsletterRepository repositoryNewsletter, LoginCliente loginCliente)
        {
            _repositoryCliente = repositoryCliente;
            _repositoryNewsletter = repositoryNewsletter;
            _loginCliente = loginCliente;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm]NewsletterEmail newsletter) //sobrecarga
        {
            if(ModelState.IsValid)
            {
                _repositoryNewsletter.Cadastrar(newsletter);

                TempData["MSG_S"] = "Email cadastrado. Agora você passará a receber nossas ofertas especiais em seu email!";
                
                return RedirectToAction(nameof(Index)); // Repassa para uma ação
            }
            else
                return View(); // Mostra a tela
        }

        public IActionResult Contato()
        {
            return View();
        }

        public IActionResult ContatoAcao()
        {
            try
            {
                Contato contato = new Contato
                {
                   //propriedade do controller + a requisição + formulário enviado pelo usuário
                    Nome = HttpContext.Request.Form["nome"],
                    Email = HttpContext.Request.Form["email"],
                    Texto = HttpContext.Request.Form["texto"]
                };

                var mensagens = new List<ValidationResult>(); // Lista de msg caso tenha erro
                var contexto = new ValidationContext(contato); // Contexto de validação do objeto
                bool isValid = Validator.TryValidateObject(contato, contexto, mensagens, true);

                if (isValid)
                {
                    ContatoEmail.EnviarContatoPorEmail(contato);

                    //ViewData e ViewBag - enviam dados pra view
                    // ViewData - acessa por dicionario (ex = chave e valor)
                    // ViewBag - acessa por propriedade (ex = ViewBag.Nome)
                    ViewData["MSG_S"] = "Mensagem de contato enviado com sucesso";
                }
                else
                {
                    var sb = new StringBuilder();
                    foreach (var texto in mensagens)
                    {
                        sb.Append($"{texto.ErrorMessage}<br />");
                    }

                    ViewData["MSG_E"] = sb.ToString();
                    ViewData["CONTATO"] = contato;
                }
                
            }
            catch (Exception)
            {
                ViewData["MSG_E"] = "Opps! Tivemos um erro. Tente novamente mais tarde!";

                //ToDo: implementar log
            }

            return View("Contato");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] Cliente cliente)
        {
            var clienteDB = _repositoryCliente.Login(cliente.Email, cliente.Senha);

            if (clienteDB != null)
            {
                _loginCliente.Login(clienteDB);

                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                ViewData["MSG_E"] = "Usuário não encontrado, verifique o email e senha digitados!";
                return View();
            }
        }

        [HttpGet]
        public IActionResult CadastroCliente()
        {
            return View();
        }

        [HttpGet]
        [ClienteAutorizacao]
        public IActionResult Painel()
        {
            return new ContentResult() { Content = "Este é o painel do cliente!" };
        }

        [HttpPost]
        public IActionResult CadastroCliente([FromForm]Cliente cliente)
        {
            if (ModelState.IsValid)
            {

                _repositoryCliente.Cadastrar(cliente);

                // TempData - armazena dados por um tempo
                /* - Garante que mesmo que tenham sido feitas outras requisições,
                   vc ainda possa acessar esses mesmos dados
                   - Depois da leitura destes dados, eles são deletados.
                  (pode-se usar o método .Keep para mantê-los)*/
                TempData["MSG_S"] = "Cadastro realizado com sucesso!";

                //Todo: Implementar redirecionamentosa diferentes (painel, carrinho etc)
                return RedirectToAction(nameof(CadastroCliente));
            }
            return View();
        }

        public IActionResult CarrinhoCompras()
        {
            return View();
        }
    }
}
