using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.Core.Const;
using Api.Core.Domain;

namespace Api.Core.Repositories
{
    public interface IOfferRepository
    {
        Task<Offer> GetOffer(Guid id);

        /// <summary>
        /// Fetch offers. Lot of parameters are nullable, that means they are not needed at all.
        /// </summary>
        Task<ICollection<Offer>> GetOffers(Guid? authorId, string city, double? longitude, double? latitude,
            int? maxDistance, IEnumerable<OfferType> types, int page, int resultsPerPage);

        Task Insert(Offer offer);
        
        Task<bool> OfferExistsForUser(Guid offerId, Guid authorId);
        
        /// <summary>
        /// Remove offer by offer Id. IMPORTANT: First check if offer belongs to user, by calling <see cref="OfferExistsForUser"/>
        /// </summary>
        /// <param name="offerId">Id of the offer</param>
        Task RemoveOffer(Guid offerId);

        Task Update(Offer offer);
    }
}