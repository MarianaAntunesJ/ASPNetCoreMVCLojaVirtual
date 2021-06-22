using LojaVirtual.Models;
using System.Net;
using System.Net.Mail;

namespace LojaVirtual.Libraries.Email
{
    public class ContatoEmail
    {
        public static void EnviarContatoPorEmail(Contato contato)
        {
            // SMTP - Servidor que irá enviar a mensagem.
            // Servidor smtp do gmail + porta
            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("marianajantunes97@gmail.com", "627422sp"),
                EnableSsl = true //conexão segura
            };

            var corpoMsg = string.Format($"<h2>Contato - LojaVirtual</h2>" +
                                          $"<b>Nome: </b> {contato.Nome} <br />" +
                                          $"<b>Email: </b> {contato.Email} <br />" +
                                          $"<b>Texto: </b> {contato.Texto} <br />" +
                                       $"<br />E-mail enviado automaticamente do site LojaVirtual");

            var mensagem = new MailMessage();

            mensagem.From = new MailAddress("marianajantunes97@gmail.com");
            mensagem.To.Add("marianajantunes97@gmail.com");
            mensagem.Subject = $"Contato - LojaVirtual - E-mail: {contato.Email}";

            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            // ENviar msg via SMTP
            smtp.Send(mensagem);
        }
    }
}
