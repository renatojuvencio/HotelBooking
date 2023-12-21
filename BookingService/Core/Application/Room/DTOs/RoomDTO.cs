using Domain.Guest.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Room.DTOs
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenace { get; set; }
        public Price Price { get; set; }
        public bool IsAvailable { get; set; }
        public bool HasGuest { get; set; }

        internal static Domain.Room.Entities.Room MapToEntity(RoomDTO roomDTO)
        {
            return new Domain.Room.Entities.Room
            {
                Id = roomDTO.Id,
                Name = roomDTO.Name,
                InMaintenace = roomDTO.InMaintenace,
                Level = roomDTO.Level,
                Price = roomDTO.Price,
            };
        }

        internal static RoomDTO MapToDto(Domain.Room.Entities.Room room)
        {
            return new RoomDTO
            {
                Id = room.Id,
                Name = room.Name,
                Level = room.Level,
                Price=room.Price,
                HasGuest = room.HasGuest,
                InMaintenace = room.InMaintenace,
                IsAvailable = room.IsAvailable,
            };
        }
    }
}
