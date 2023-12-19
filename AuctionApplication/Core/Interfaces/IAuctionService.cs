namespace AuctionApplication.Core.Interfaces
{
    public interface IAuctionService {
        //används i controller 
        bool CreateAuction(Auction auction);
        bool UpdateDescription(int id, string newDescr, string owner);

        Auction GetAuctionById(int id);
        // bool PlaceBid(Bid bid);

        bool PlaceBid(Bid bid, ref string msg);


        List<Auction> GetActiveOngoingAuctions();

        List<Auction> GetAllActiveBiddenAuctionsByUser(string userName);
        List<Auction> GetClosedAuctionsWonByUser(string userName);
        List<Auction> GetAllTheUsersAuctions(string userName);

    }
}
