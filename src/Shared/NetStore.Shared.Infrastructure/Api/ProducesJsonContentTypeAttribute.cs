using Microsoft.AspNetCore.Mvc;

namespace NetStore.Shared.Infrastructure.Api;

public sealed class ProducesJsonContentTypeAttribute : ProducesAttribute
{
    public ProducesJsonContentTypeAttribute(Type type) : base(type)
    {
    }

    public ProducesJsonContentTypeAttribute(params string[] additionalContentTypes) : base("application/json", additionalContentTypes)
    {
    }
}