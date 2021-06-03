using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Email
{
    public class ContatoEmail
    {
        public static void EnviarContatoPorEmail(Contato contato)
        {
            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("marianajantunes97@gmail.com", "senha"),
                EnableSsl = true
            };

            string corpoMsg = string.Format("<h2>Contato - LojaVirtual</h2>" +
                                            "<b>Nome: </b> {0} <br />" +
                                            "<b>Email: </b> {1} <br />" +
                                            "<b>Texto: </b> {2} <br />" +
                                         "<br />E-mail enviado automaticamente do site LojaVirtual",
                                            contato.Nome,
                                            contato.Email,
                                            contato.Texto);

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress("marianajantunes97@gmail.com");
            mensagem.To.Add("marianajantunes97@gmail.com");
            mensagem.Subject = $"Contato - LojaVirtual - E-mail: {contato.Email}";
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            smtp.Send(mensagem);
        }
    }
}
