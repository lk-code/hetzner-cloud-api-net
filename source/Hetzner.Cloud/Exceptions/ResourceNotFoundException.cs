﻿namespace Hetzner.Cloud.Exceptions;

public class ResourceNotFoundException : ApiException
{
    public ResourceNotFoundException(string message,
        HttpResponseMessage response)
        : base(response, message)
    {
    }

    public ResourceNotFoundException(string message,
        HttpResponseMessage response,
        Exception inner)
        : base(response, message, inner)
    {
    }
}