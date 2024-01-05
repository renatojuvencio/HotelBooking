using Domain.Guest.Exceptions;
using Domain.Guest.Ports;
using Domain.Guest.ValueObjects;

namespace Domain.Guest.Entities
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public PersonId DocumentId { get; set; }

        private void ValidState()
        {
            if (this.DocumentId == null ||
               string.IsNullOrEmpty(this.DocumentId.IdNumber) ||
               this.DocumentId.IdNumber.Length <= 3 ||
               this.DocumentId.DocumentType == 0)
            {
                throw new InvalidPersonDocumentIdException();
            }

            if (string.IsNullOrEmpty(this.Name) ||
               string.IsNullOrEmpty(this.Surname) ||
               string.IsNullOrEmpty(this.Email) ||
               string.IsNullOrWhiteSpace(this.Name) ||
               string.IsNullOrWhiteSpace(this.Surname) ||
               string.IsNullOrWhiteSpace(this.Email))
            {
                throw new MissingRequiredInformationException();
            }

            if (!Utils.ValidateEmail(this.Email))
                throw new InvalidEmailException();
        }

        public bool ValidGuest()
        {
            this.ValidState();
            return true;
        }

        public async Task Save(IGuestRepository guestRepository)
        {
            ValidState();
            if (Id == 0)
            {
                this.Id = await guestRepository.Create(this);
            }
            else
            {
            }
        }
    }
}
