﻿using Domain.Guest.Exceptions;
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
            if (DocumentId == null ||
               string.IsNullOrEmpty(DocumentId.IdNumber) ||
               DocumentId.IdNumber.Length <= 3 ||
               DocumentId.DocumentType == 0)
            {
                throw new InvalidPersonDocumentIdException();
            }

            if (string.IsNullOrEmpty(Name) ||
               string.IsNullOrEmpty(Surname) ||
               string.IsNullOrEmpty(Email) ||
               string.IsNullOrWhiteSpace(Name) ||
               string.IsNullOrWhiteSpace(Surname) ||
               string.IsNullOrWhiteSpace(Email))
            {
                throw new MissingRequiredInformationException();
            }

            if (!Utils.ValidateEmail(Email))
                throw new InvalidEmailException();
        }

        public async Task Save(IGuestRepository guestRepository)
        {
            ValidState();
            if (Id == 0)
            {
                await guestRepository.Create(this);
            }
            else
            {
                await guestRepository.Create(this);
            }
        }
    }
}