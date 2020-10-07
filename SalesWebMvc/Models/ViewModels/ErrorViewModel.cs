using System;

namespace SalesWebMvc.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; }
        
        //Essa requisição vai retornar se ele não é nulo ou vazio
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}