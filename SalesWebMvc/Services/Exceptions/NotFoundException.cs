using System;


namespace SalesWebMvc.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        // Exeção personalizada, execeções especificas da camada de serviço quando se tem uma exeção personalizada temos a possibilidade
        //de tratar exclusivamente essa exeção
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
