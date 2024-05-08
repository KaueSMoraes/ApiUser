namespace AssemblyMaster.Exceptions
{
    public class ContentSizeException : ApplicationException
    {
        public ContentSizeException(string message) :base(message){}
    }

    public class ContentVoidException : ApplicationException
    {
        public ContentVoidException(string message, string c) :base(message){}
    }

    public class ContentIndexOfException : ApplicationException
    {
        public ContentIndexOfException (string message) :base(message){}
    }
}
