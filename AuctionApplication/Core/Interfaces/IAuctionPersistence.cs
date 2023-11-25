namespace AuctionApplication.Core.Interfaces
{
    public interface IAuctionPersistence {
        bool CreateAuction(Auction auction);
        bool UpdateDescription(Auction auction);
        
        List<Auction> GetOnGoingAuctions(List<Auction> auctions);
        Auction GetAuctionById(int id);

        bool PlaceBid(Bid bid);
        List<Auction> GetAllActiveBiddenAuctionsByUser(string userName);
        List<Auction> GetClosedAuctionsWonByUser(string userName);
        List<Auction> GetAllTheUsersAuctions(string userName);

    }
}
