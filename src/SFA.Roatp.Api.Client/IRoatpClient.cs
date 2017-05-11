using System.Collections.Generic;
using System.Net.Http;
using SFA.Roatp.Api.Types;
using SFA.Roatp.Api.Types.Exceptions;

namespace SFA.Roatp.Api.Client
{
    public interface IRoatpClient
    {
        /// <summary>
        /// Get a provider details
        /// GET /providers/{ukprn}
        /// </summary>
        /// <param name="ukprn">the provider ukprn this should be 8 numbers long</param>
        /// <returns>a provider details based on ukprn</returns>
        /// <exception cref="EntityNotFoundException">when the resource you requested doesn't exist</exception>
        /// <exception cref="HttpRequestException">There was an unexpected error</exception>
        Provider Get(string ukprn);

        /// <summary>
        /// Get a provider details
        /// GET /providers/{ukprn}
        /// </summary>
        /// <param name="ukprn">the provider ukprn this should be 8 numbers long</param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException">when the resource you requested doesn't exist</exception>
        /// <exception cref="HttpRequestException">There was an unexpected error</exception>
        Provider Get(long ukprn);

        /// <summary>
        /// Get a provider details
        /// GET /providers/{ukprn}
        /// </summary>
        /// <param name="ukprn">the provider ukprn this should be 8 numbers long</param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException">when the resource you requested doesn't exist</exception>
        /// <exception cref="HttpRequestException">There was an unexpected error</exception>
        Provider Get(int ukprn);
        /// <summary>
        /// Check if a provider exists
        /// HEAD /providers/{ukprn}
        /// </summary>
        /// <param name="ukprn">the provider ukprn this should be 8 numbers long</param>
        /// <returns>bool</returns>
        /// <exception cref="EntityNotFoundException">when the resource you requested doesn't exist</exception>
        /// <exception cref="HttpRequestException">There was an unexpected error</exception>
        bool Exists(string ukprn);
        /// <summary>
        /// Check if a provider exists
        /// HEAD /providers/{ukprn}
        /// </summary>
        /// <param name="ukprn">the provider ukprn this should be 8 numbers long</param>
        /// <returns>bool</returns>
        /// <exception cref="EntityNotFoundException">when the resource you requested doesn't exist</exception>
        /// <exception cref="HttpRequestException">There was an unexpected error</exception>
        bool Exists(int ukprn);
        /// <summary>
        /// Check if a provider exists
        /// HEAD /providers/{ukprn}
        /// </summary>
        /// <param name="ukprn">the provider ukprn this should be 8 numbers long</param>
        /// <returns>bool</returns>
        /// <exception cref="EntityNotFoundException">when the resource you requested doesn't exist</exception>
        /// <exception cref="HttpRequestException">There was an unexpected error</exception>
        bool Exists(long ukprn);
        /// <summary>
        /// Get a list of active providers on ROATP
        /// GET /providers
        /// </summary>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException">when the resource you requested doesn't exist</exception>
        /// <exception cref="HttpRequestException">There was an unexpected error</exception>
        IEnumerable<Provider> FindAll();
    }
}