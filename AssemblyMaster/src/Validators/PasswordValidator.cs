using AssemblyMaster.Exceptions;

namespace AssemblyMaster.Validators;

public static class PasswordValidator
{
    public static void Content(string password)
    {
        if (password.Length < 5)
        {
            throw new ContentSizeException("Senha não pode conter menos de 5 caracteres");
        }
    }
}
