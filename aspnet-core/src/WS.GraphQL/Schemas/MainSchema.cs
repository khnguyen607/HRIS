using Abp.Dependency;
using GraphQL.Types;
using GraphQL.Utilities;
using WS.Queries.Container;
using System;

namespace WS.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IServiceProvider provider) :
            base(provider)
        {
            Query = provider.GetRequiredService<QueryContainer>();
        }
    }
}