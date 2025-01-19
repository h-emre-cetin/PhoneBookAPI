using System;
using System.Collections.Generic;

namespace PhoneBookAPI.Application.DTOs
{
    public class PersonDto
    {
        public required Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Company { get; set; }
        public required List<ContactInfoDto> ContactInformation { get; set; }
    }

    public class CreatePersonDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Company { get; set; }
    public required List<AddContactInfoDto> ContactInformation { get; set; } = new List<AddContactInfoDto>();
}

    public class UpdatePersonDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Company { get; set; }
    public required List<AddContactInfoDto> ContactInformation { get; set; } = new List<AddContactInfoDto>();
}
}