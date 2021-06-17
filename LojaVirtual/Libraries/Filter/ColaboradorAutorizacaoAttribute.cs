using LojaVirtual.Libraries.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace LojaVirtual.Libraries.Filter
{
    public class ColaboradorAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {

        LoginColaborador _loginColaborador;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginColaborador = (LoginColaborador)context.HttpContext.RequestServices.GetService(typeof(LoginColaborador));
            var colaborador = _loginColaborador.Get();

            if (colaborador == null)
                context.Result = new ContentResult() { Content = "Acesso negado" };
        }
    }
}
