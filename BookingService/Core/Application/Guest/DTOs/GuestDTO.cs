using Domain.Guest.Enums;
using Entities = Domain.Guest.Entities;
namespace Application.Guest.DTOs
{
    public class GuestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public int IdTypeCode { get; set; }

        internal static Domain.Guest.Entities.Guest MapToEntity(GuestDTO guestDTO)
        {
            return new Entities.Guest
            {
                Id = guestDTO.Id,
                Name = guestDTO.Name,
                Surname = guestDTO.Surname,
                Email = guestDTO.Email,
                DocumentId = new Domain.Guest.ValueObjects.PersonId
                {
                    IdNumber = guestDTO.IdNumber,
                    DocumentType = (DocumentType)guestDTO.IdTypeCode
                }
            };
        }

        internal static GuestDTO MapToDto(Domain.Guest.Entities.Guest guest)
        {
            return new GuestDTO
            {
                Id = (int)guest.Id,
                Email = guest.Email,
                Name = guest.Name,
                Surname = guest.Surname,
                IdNumber = guest.DocumentId.IdNumber,
                IdTypeCode = (int)guest.DocumentId.DocumentType,
            };
        }
    }
}
