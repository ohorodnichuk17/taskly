using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taskly_Api.Response.Card;
using Taskly_Application.Requests.Card.GetCardsListsByBoardId;

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
            var cardList = await sender.Send(new GetCardListByBoardIdQuery(boardId));

            return cardList.Match(cardList => Ok(mapper.Map<CardListResponse[]>(cardList)),
                errors => Problem(errors));
        }
    }
}
