using System;

namespace Shared.Domain.Entities
{
    public interface IEntity<TId>
    {
         TId Id { get; set; }
    }
}
