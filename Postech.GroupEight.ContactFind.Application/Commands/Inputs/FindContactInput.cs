using MediatR;
using Postech.GroupEight.ContactFind.Application.Commands.Outputs;
using Postech.GroupEight.ContactFind.Application.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Postech.GroupEight.ContactFind.Application.Commands.Inputs
{
    /// <summary>
    /// Object that stores contact search data.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FindContactInput : IRequest<DefaultOutput<IEnumerable<FindContactByAreaCodeViewModel>>>
    {
        /// <summary>
        /// The area code of the contact's phone number that will be used as a filter for the search.
        /// </summary>
        [Required(ErrorMessage = "Contact area code is required.")]
        [RegularExpression("^\\d{2}$", ErrorMessage = "The area code must contain 2 numeric characters.")]
        public string AreaCodeValue { get; init; }
    }
}
