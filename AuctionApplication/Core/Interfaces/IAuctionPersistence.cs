namespace AuctionApplication.Core.Interfaces
{
    public interface IAuctionPersistence {
        bool CreateAuction(Auction auction);
        bool UpdateDescription(int id,string newDescr, string owner);
        
       
        Auction GetAuctionById(int id);


        bool PlaceBid(Bid bid);
        bool IsBidPlaceAble(Bid bid, ref string msg);
      
        List<Auction> GetAllActiveBiddenAuctionsByUser(string userName);
        List<Auction> GetActiveOngoingAuctions();

        List<Auction> GetClosedAuctionsWonByUser(string userName);
        List<Auction> GetAllTheUsersAuctions(string userName);
        

    }
}
