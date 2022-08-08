namespace DataLoad.Application.Transformer
{
    public class TransformException : Exception
    {
        internal TransformException(string businessMessage)
            : base(businessMessage)
        {
        }

        internal TransformException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
