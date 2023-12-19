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
            //if (auction == null || auction.Id != 0 ) throw new InvalidDataException();
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
        public List<Auction> GetActiveOngoingAuctions()
        {
            return _auctionPersistence.GetActiveOngoingAuctions();
        }

        public Auction GetAuctionById(int id)
        {
            return _auctionPersistence.GetAuctionById(id);
        }

        public List<Auction> GetClosedAuctionsWonByUser(string userName)
        {
            return _auctionPersistence.GetClosedAuctionsWonByUser(userName);
         
        }

      

      

        public bool UpdateDescription(int id,string descr,string owner)
        {
            return _auctionPersistence.UpdateDescription(id,descr,owner);
        }

  


        public bool PlaceBid(Bid bid, ref string msg)
        {
            
            if(_auctionPersistence.IsBidPlaceAble(bid, ref msg)) { 
            _auctionPersistence.PlaceBid(bid);
                return true;

            }
            else
            {
                return false;
            }
                
            
            
        }
    }
}
