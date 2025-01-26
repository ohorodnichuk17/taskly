using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class ToDoTableItemsRepository(TasklyDbContext tasklyDbContext) : Repository<ToDoItemEntity>(tasklyDbContext), IToDoTableItemsRepository
{
    
}
