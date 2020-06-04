using EventsCore.Application.Common.Models;
using System.Threading.Tasks;

namespace EventsCore.Application.Common.Interfaces
{
    /// <summary>
    /// Defines a User Manager Interface
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Adds a User
        /// </summary>
        /// <param name="LDAPName">A string containing the User's LDAP name.</param>
        /// <param name="blueDeckId">A unsigned integer containing the User's BlueDeck Id.</param>
        /// <param name="firstName">A string containing the User's first name.</param>
        /// <param name="lastName">A string containing the User's last name.</param>
        /// <param name="idNumber">A string containing the User's Id Number.</param>
        /// <param name="email">A string containing the User's email address.</param>
        /// <param name="contactNumber">A string containing the User's contact number.</param>
        /// <param name="rankId">An integer containing the Id of the User's <see cref="Domain.Entities.Rank"></see> Rank.</param>
        /// <returns></returns>
        Task<(Result Result, int UserId)> CreateUserAsync(string LDAPName, uint blueDeckId, string firstName, string lastName, string idNumber, string email, string contactNumber, int rankId);
        /// <summary>
        /// Deletes a User
        /// </summary>
        /// <param name="UserId">The Id of the User to delete.</param>
        /// <returns></returns>
        Task<Result> DeleteUserAsync(int UserId);
    }
}
