namespace DataLoad.Application.Transformer
{
    public class TransformException : Exception
    {
        public TransformException(string businessMessage)
            : base(businessMessage)
        {
        }

        public TransformException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
