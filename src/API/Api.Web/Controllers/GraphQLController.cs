using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Models.Queries;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Web.Controllers
{
#if DEBUG
    [Route("/graphql")]
#else
    [Route("api/[controller]")]
#endif
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ExecutionOptions _options;

        public GraphQLController(
            ISchema schema,
            IDocumentExecuter documentExecuter)
        {
            _documentExecuter = documentExecuter;
            _schema = schema;
            _options = new ExecutionOptions
            {
                Schema = _schema
            };
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            _options.Query = query.Query;
            _options.OperationName = query.OperationName;
            _options.Inputs = query.Variables.ToInputs();

            var result = await _documentExecuter.ExecuteAsync(_options);

            if ((result.Errors?.Count ?? 0) > 0)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result);
        }
    }
}
