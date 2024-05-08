using AssemblyMaster.Exceptions;

namespace AssemblyMaster.Validators;

public static class EmailValidator
{
   public static void Content(string email)
   {
        if (string.IsNullOrEmpty(email))
        throw new ContentVoidException("Email não pode ser vazio", nameof(email));

        if ((email.IndexOf(".com") == -1) && (email.IndexOf("@") == -1))
        {
            throw new ContentIndexOfException("Formato de email inválido");
        }
   }
}
