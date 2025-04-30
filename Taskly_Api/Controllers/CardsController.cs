using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taskly_Api.Request.Card;
using Taskly_Api.Response.Card;
using Taskly_Application.Requests.Card.Command.CreateCard;
using Taskly_Application.Requests.Card.Command.TransferCardToAnotherCardList;
using Taskly_Application.Requests.Card.Query.GetCardsListsByBoardId;


namespace Taskly_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController(ISender sender, IMapper mapper) : ApiController
    {
        [Authorize]
        [HttpGet("get-card-list-by-board-id")]
        public async Task<IActionResult> GetCardsListsByBoardId([FromQuery] Guid boardId)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")!.Value;
            var cardList = await sender.Send(new GetCardListByBoardIdQuery(boardId, Guid.Parse(userId)));

            return cardList.Match(cardList =>
                Ok(mapper.Map<CardListResponse[]>(cardList)),
                errors => Problem(errors));
        }

        [Authorize]
        [HttpPut("transfer-card-to-another-card-list")]
        public async Task<IActionResult> TransferCardToAnotherCardList([FromBody] TransferCardToAnotherCardListRequest transferCardToAnotherCardListRequest)
        {
            var transferedCard = await sender.Send(mapper.Map<TransferCardToAnotherCardListCommand>(transferCardToAnotherCardListRequest));

            return transferedCard.Match(transferedCard =>
                Ok(transferedCard),
                errors => Problem(errors));
        }

        [Authorize]
        [HttpPost("create-card")]
        public async Task<IActionResult> CreateCard([FromBody] CreateCardRequest createCustomCardRequest)
        {
            var createdCardId = await sender.Send(mapper.Map<CreateCardCommand>(createCustomCardRequest));

            return createdCardId.Match(createdCardId =>
                Ok(createdCardId),
                errors => Problem(errors));
        }
    }
}
