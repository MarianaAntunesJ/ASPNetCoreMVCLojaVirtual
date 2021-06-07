﻿using LojaVirtual.Libraries.Email;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var news = new NewsletterEmail() { Email = "teste@gmail.com" };
            return View(news);
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
                    Nome = HttpContext.Request.Form["nome"],
                    Email = HttpContext.Request.Form["email"],
                    Texto = HttpContext.Request.Form["texto"]
                };

                var mensagens = new List<ValidationResult>();
                var contexto = new ValidationContext(contato);
                bool isValid = Validator.TryValidateObject(contato, contexto, mensagens, true);

                if (isValid)
                {
                    ContatoEmail.EnviarContatoPorEmail(contato);

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
            catch (Exception e)
            {
                ViewData["MSG_E"] = "Opps! Tivemos um erro. Tente novamente mais tarde!";

                //ToDo: implementar log
            }

            return View("Contato");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CadastroCliente()
        {
            return View();
        }

        public IActionResult CarrinhoCompras()
        {
            return View();
        }
    }
}
