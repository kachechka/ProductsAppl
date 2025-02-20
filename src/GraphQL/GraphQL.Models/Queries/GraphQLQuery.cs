﻿using Newtonsoft.Json.Linq;

namespace GraphQL.Models.Queries
{
    public class GraphQLQuery
    {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string Query { get; set; }
        public JObject Variables { get; set; }

        public GraphQLQuery()
        { }
    }
}
