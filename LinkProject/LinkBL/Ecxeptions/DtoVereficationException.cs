namespace LinkBL.Ecxeptions
{
    public class DtoVereficationException : Exception
    {
        public DtoVereficationException(string error) : base(error) { }
    }
}
