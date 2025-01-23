// using Mapster;
// using Taskly_Application.DTO;
// using Taskly_Domain.Entities;
//
// namespace Taskly_Api.MapsterConfigs;
//
// public static class BoardMapsterConfig
// {
//     public static void Config()
//     {
//         TypeAdapterConfig<BoardEntity, TemplateBoardDto>
//             .NewConfig()
//             .Map(dest => dest.Members, src => src.Members.Select(m => new MemberDto
//             {
//                 Name = m.NormalizedUserName,
//                 AvatarId = m.AvatarId
//             }))
//             .Map(dest => dest.BoardTemplates, src => src.BoardTemplates.Select(bt => new BoardTemplateDto
//             {
//                 Name = bt.Name,
//                 ImagePath = bt.ImagePath
//             }))
//             .Map(dest => dest.CardLists, src => src.CardLists.Select(cl => new CardListDto
//             {
//                 Title = cl.Title,
//                 Cards = cl.Cards.Select(c => new CardDto
//                 {
//                     Description = c.Description,
//                     AttachmentUrl = c.AttachmentUrl,
//                     Status = c.Status,
//                     TimeRange = c.TimeRangeEntity != null ? new TimeRangeDto
//                     {
//                         StartTime = c.TimeRangeEntity.StartTime,
//                         EndTime = c.TimeRangeEntity.EndTime
//                     } : null,
//                     Comments = c.Comments.Select(com => new CommentDto
//                     {
//                         Text = com.Text,
//                         CreatedAt = com.CreatedAt
//                     }).ToList()
//                 }).ToList()
//             }).ToList());
//     }
// }