namespace ClubNet.Shared.Api.Contracts
{
    using ClubNet.Shared.Api.Dto;

    using System.Threading.Tasks;

    /// <summary>
    /// Define an Api able to managed the user informations
    /// </summary>
    public interface IUserApi
    {
        /// <summary>
        /// Create a new user account
        /// </summary>
        Task<IApiResponse> CreateUserAccountAsync(RegisterDto registerDto);
    }
}
