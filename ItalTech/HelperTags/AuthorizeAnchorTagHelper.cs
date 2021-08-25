using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Linq;
using System.Security.Claims;

namespace WebApp.HelperTags
{
    /// <summary>
    /// Tag Anchor che viene visualizzato solo se l'utente corrente ha il ruolo richiesto, definito nella proprietà Role
    /// </summary>
    public class AuthorizeAnchorTagHelper : TagHelper
    {
        readonly ClaimsPrincipal currentUser;
        public AuthorizeAnchorTagHelper(IActionContextAccessor actionContext )
        {
            currentUser = actionContext.ActionContext.HttpContext.User;
        }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Roles { get; set; }
        /// <summary>
        /// Definisce la modalità di visualizzazione: Visibility non far appare il tag, Abilitation lo disabilita.
        /// </summary>
        public Mode Mode { get; set; } = Mode.Visibility;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";

            var link = String.IsNullOrEmpty(Area) ? "/" : "/" + Area + "/";
            link += Controller + "/" + Action;
            output.Attributes.SetAttribute("href", link);

            var existingClass = output.Attributes.FirstOrDefault(f => f.Name == "class");
            var cssClass = string.Empty;
            if (existingClass != null)
            {
                cssClass = existingClass.Value.ToString().Replace("disabledLink","");
            }
            cssClass += " disabledLink";

            if (string.IsNullOrEmpty(Roles))
            {
                return;
            }
            var roles = Roles.Split(',');
            foreach (var role in roles)
            {
                if (currentUser.IsInRole(role))
                {
                    return;
                }
            }

            if (Mode == Mode.Visibility)
            {
                output.SuppressOutput();
            }
            else
            {
                output.Attributes.SetAttribute("class", cssClass);
            }
        }
    }
}
