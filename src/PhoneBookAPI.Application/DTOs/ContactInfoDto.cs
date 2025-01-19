using System;
using PhoneBookAPI.Core.Entities.Enums;

namespace PhoneBookAPI.Application.DTOs
{
    public class ContactInfoDto
    {
        public Guid Id { get; set; }
        public required ContactType Type { get; set; }
        public required string Value { get; set; }
    }

    public class AddContactInfoDto
    {
        public required ContactType Type { get; set; }
        public required string Value { get; set; }
    }
}