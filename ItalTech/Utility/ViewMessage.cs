using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using ItalTech.ExtensionMethods;
using ItalTech.Models;

namespace ItalTech.Utility
{
    public class ViewMessage
    {
        /// <summary>
        /// Visualizza il messaggio, contenuto nell'oggetto response, nella vista parziale  _MessageResponse
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="response">Risposta da visualizzare</param>
        public static void Show(Controller controller, Response response)
        {
            controller.TempData.Set<Response>("response", response);
        }

        /// <summary>
        /// Visualizza il messaggio, contenuto nell'oggetto response, nella vista parziale  _MessageResponse
        /// </summary>
        /// <typeparam name="T">Tipo</typeparam>
        /// <param name="controller">Controller</param>
        /// <param name="response">Risposta da visualizzare</param>
        public static void Show<T>(Controller controller, Response<T> response)
        {
            controller.TempData.Set<Response<T>>("response", response);
        }

        /// <summary>
        /// Visualizza il messaggio, contenuto nell'oggetto response, nella vista parziale  _MessageResponse
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="response">Risposta da visualizzare</param>
        public static void Show(Controller controller, Infrastruttura.Models.Response response)
        {
            controller.TempData.Set<Response>("response", new Response { IsSucces = response.IsSucces, Message=response.Message});
        }

        /// <summary>
        /// Visualizza il messaggio, contenuto nell'oggetto response, nella vista parziale  _LocalMessageResponse
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="response">Risposta da visualizzare</param>
        public static void ShowLocal(Controller controller, Response response)
        {
            controller.ViewBag.Response = response;
        }

        /// <summary>
        /// Visualizza il messaggio, contenuto nell'oggetto response, nella vista parziale  _LocalMessageResponse
        /// </summary>
        /// <typeparam name="T">Tipo</typeparam>
        /// <param name="controller">Controller</param>
        /// <param name="response">Risposta da visualizzare</param>
        public static void ShowLocal<T>(Controller controller, Response<T> response)
        {
            controller.ViewBag.Response = response;
        }

        /// <summary>
        /// Visualizza il messaggio, contenuto nell'oggetto response, nella vista parziale  _LocalMessageResponse
        /// </summary>
        /// <typeparam name="T">Tipo</typeparam>
        /// <param name="controller">Controller</param>
        /// <param name="response">Risposta da visualizzare</param>
        public static void ShowLocal<T>(Controller controller, Infrastruttura.Models.Response<T> response)
        {
            controller.ViewBag.Response = new Response { IsSucces = response.IsSucces, Message=response.Message};
        }

        /// <summary>
        /// Restituisce la response precedentemente settata tramite il metodo "Show"
        /// </summary>
        /// <param name="razor"></param>
        /// <returns></returns>
        public static Response GetResponse(RazorPageBase razor)
        {
            return razor.TempData.Get<Response>("response");
        }

        public static Response GetLocalResponse(RazorPage razor)
        {
            var response = razor.ViewBag.Response;
            if (response == null)
                return null;
            return response is Response ? response : new Response { IsSucces = response.IsSucces , Message = response.Message }; 
        }
    }
}
