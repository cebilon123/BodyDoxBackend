using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Api.Application.Common;
using Api.Application.Handlers;
using Api.Application.Handlers.CommandHandlers;
using Api.Application.Handlers.Commands;
using Api.Application.Handlers.Queries;
using Api.Application.ResultModels;
using Api.Attributes;
using Api.Core.Const;
using Api.Core.Repositories;
using Api.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OffersController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IImageRepository _repository;

        public OffersController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher,
            IImageRepository repository)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
            _repository = repository;
        }

        [HttpGet]
        [SwaggerDescription("Fetch offers with possible query parameters")]
        public async Task<ICollection<Offer>> GetOffers(
            [FromQuery] string city,
            [FromQuery] double? latitude,
            [FromQuery] double? longitude,
            [FromQuery] IEnumerable<OfferType> types,
            [FromQuery] Pagination page,
            [FromQuery] int? distance)
        {
            return await _queryDispatcher.QueryAsync(new GetOffersQuery(city, latitude, longitude, types,
                page ?? Pagination.Default, distance));
        }

        [HttpGet("/{id}")]
        [SwaggerDescription("Fetch offer by offer guid")]
        public async Task<GetOfferByIdQueryResult> GetOffer([FromRoute] Guid id)
        {
            return await _queryDispatcher.QueryAsync(new GetOfferByIdQuery(id));
        }

        [HttpPost]
        [CookiesAuthorize]
        [SwaggerDescription("Create offer")]
        public async Task<ActionResult> CreateOffer([FromForm] IEnumerable<IFormFile> images,
            [FromForm] CreateOfferCommand command)
        {
            await using var stream = new MemoryStream();
            foreach (var image in images)
            {
                if (!ImageHelper.IsImage(image))
                    return new UnsupportedMediaTypeResult();

                await image.CopyToAsync(stream);

                command.AddImage(new CreateOfferImage(stream.ToArray()));

                stream.SetLength(0);
            }

            await _commandDispatcher.SendAsync(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        [CookiesAuthorize]
        [SwaggerDescription(
            "Remove offer by offer id. Action can be performed only by creator of offer or if we impersonate user")]
        public async Task<ActionResult> DeleteOffer([FromRoute] Guid id)
        {
            await _commandDispatcher.SendAsync(new RemoveOfferCommand(id));

            return Ok();
        }

        [HttpPut("{id}")]
        [CookiesAuthorize]
        [SwaggerDescription("Update offer by offer id")]
        public async Task<ActionResult> UpdateOffer([FromRoute] Guid id,
            [FromBody] UpdateOfferCommand.UpdateOfferData updateOfferDataData)
        {
            await _commandDispatcher.SendAsync(new UpdateOfferCommand(id, updateOfferDataData));

            return Ok();
        }
    }
}