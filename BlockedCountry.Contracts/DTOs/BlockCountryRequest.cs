using System.ComponentModel.DataAnnotations;

namespace BlockedCountry.Contracts.DTOs;

public class BlockCountryRequest
{
    [Required(ErrorMessage = "Country code is required.")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "Country code must be exactly 2 letters.")]
    public string CountryCode { get; set; } = string.Empty;
}
