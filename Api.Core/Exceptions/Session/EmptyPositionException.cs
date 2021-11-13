namespace Api.Core.Exceptions.Session
{
    public class EmptyPositionException : DomainException
    {
        public override string Code => "empty_position";
        
        public EmptyPositionException(string positionName) : base($"{positionName} is empty. {positionName} must be set.")
        {
        }

    }
}