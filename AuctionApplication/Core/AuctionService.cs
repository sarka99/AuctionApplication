using AuctionApplication.Core.Interfaces;

namespace AuctionApplication.Core
{
    public class AuctionService : IAuctionService
    {
        private IAuctionPersistence _auctionPersistence;

        public AuctionService(IAuctionPersistence auctionPersistence)
        {
            _auctionPersistence = auctionPersistence;
        }

        public bool CreateAuction(Auction auction)
        {
            if (auction == null || auction.Id != 0 ) throw new InvalidDataException();
            return _auctionPersistence.CreateAuction(auction);          
        }

        public List<Auction> GetAllActiveBiddenAuctionsByUser(string userName)
        {
            return _auctionPersistence.GetAllActiveBiddenAuctionsByUser(userName);
        }

        public List<Auction> GetAllTheUsersAuctions(string userName)
        {
           return _auctionPersistence.GetAllTheUsersAuctions(userName);
        }

        public Auction GetAuctionById(int id)
        {
            return _auctionPersistence.GetAuctionById(id);
        }

        public List<Auction> GetClosedAuctionsWonByUser(string userName)
        {
            return _auctionPersistence.GetClosedAuctionsWonByUser(userName);
         
        }

        public List<Auction> GetOnGoingAuctions(List<Auction> auctions)
        {
            return _auctionPersistence.GetOnGoingAuctions(auctions);
        }

        public bool PlaceBid(Bid bid)
        {
           
            return _auctionPersistence.PlaceBid(bid);
           
        }

        public bool UpdateDescription(Auction auction)
        {
            return _auctionPersistence.UpdateDescription(auction);
        }
    }
}
