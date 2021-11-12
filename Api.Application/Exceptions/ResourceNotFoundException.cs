using System;

namespace Api.Application.Exceptions
{
    public class ResourceNotFoundException : ApplicationException
    {
        public override string Code => "resource_not_found";
        
        public ResourceNotFoundException(Type resourceType) : base($"Resource of type: {resourceType} not found.")
        {
        }

    }
}