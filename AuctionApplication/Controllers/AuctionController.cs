using AuctionApplication.Core;
using AuctionApplication.Core.Interfaces;
using AuctionApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace AuctionApplication.Controllers
{
    //methods we want to open up for non authenticated users, do it by using [AllowAnonymous]
    [Authorize]
    public class AuctionController : Controller
    {
        private  readonly IAuctionService _auctionService;

        public AuctionController(IAuctionService auctionService)
        {
            this._auctionService = auctionService;
        }

        /*   // GET: AuctionController
          public ActionResult Index()
          {

              string userName = User.Identity.Name; 

              List<Auction> auctions = _auctionService.GetAllTheUsersAuctions(userName);
              List<AuctionVM> auctionVMs = new();
              foreach(var auction in auctions)
              {
                  auctionVMs.Add(AuctionVM.FromAuction(auction));
              }
              return View(auctionVMs);

          }*/
          
       // GET: AuctionController
        public ActionResult Index()
        {

            List<Auction> auctions = _auctionService.GetActiveOngoingAuctions();
            List<AuctionVM> auctionVMs = new();

            foreach (var auction in auctions)
            {
                auctionVMs.Add(AuctionVM.FromAuction(auction));
            }

            return View(auctionVMs);
        }
        public ActionResult ViewYourAuctions()
        {
            List<Auction> auctions = _auctionService.GetAllTheUsersAuctions(User.Identity.Name);
            List<AuctionVM> auctionVMs = new();

            foreach (var auction in auctions)
            {
                auctionVMs.Add(AuctionVM.FromAuction(auction));
            }

            return View(auctionVMs);
        }

        // GET: AuctionController/Details/5
        public ActionResult Details(int id)
        {
            //TODO: make sure the details page shows the details of the right auction
           // programm will crash if the id given below isnt valid, no auction object will be found in that case

            Auction auction = _auctionService.GetAuctionById(id);
            AuctionVM detailsVM = AuctionVM.FromAuction(auction);
            
            return View(detailsVM);
        }
        
        // GET: AuctionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuctionController/Create
        [HttpPost]
        public ActionResult Create(CreateAuctionVM vm)
        {
            Console.WriteLine("helLO");
            Console.WriteLine("\n\n\n\n\n");
            string userName = User.Identity.Name;
            //TODO: creation of auction isnt adding auction to database
            
                Auction auction = new Auction()
                {
                    Name = vm.Name, 
                    Owner = userName,
                    StartingPrice = vm.StartingPrice,
                    Deadline = vm.DeadLine,
                    Description = vm.Description,
                    //AuctionStatus = Status.ON_GOING
                   
                    
                };
               
                _auctionService.CreateAuction(auction);
                return RedirectToAction("Index");
        }
     
        public ActionResult PlaceBid()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceBid(BidVM bVm)
        {
            //placebidvm as parameter, then make new bid object, fill it with values from placebidvm and add it to placebid
            Console.WriteLine("placebid from controller" );
            if (bVm == null)
            {
                Console.WriteLine("BidVM is null.");
                ModelState.AddModelError("", "Bid placement failed. Invalid data.");
                return View(bVm);
            }

            // Assuming BidVM contains the necessary properties to create a Bid object
            var bid = new Bid
            {
                AuctionId = bVm.Id, // Replace with the actual property name in BidVM
                Bidder = User.Identity.Name,       // Replace with the actual bidder information from your application
                Amount = bVm.Amount,      // Replace with the actual property name in BidVM
                BidTime = DateTime.Now    // Use the current time or the time provided by BidVM
            };

            string message = "";
            if (_auctionService.PlaceBid(bid,ref message)){

                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("the placebid in console aint working");
                // Bid placement failed, handle errors or return appropriate response
                ModelState.AddModelError("", "Bid placement failed. " + message);
                return View(bVm); // Return to the bid placement view with error messages
            }
        }

        // GET: AuctionController/Edit/5
        public ActionResult EditDescr()
        {

            return View();
        }

        // POST: AuctionController/Edit/5
        [HttpPost]
        public ActionResult EditDescr(EditDescrVm vm)
        {
            _auctionService.UpdateDescription(vm.Id, vm.NewDescr, User.Identity.Name);
            return RedirectToAction("Details", "Auction", new { id = vm.Id });
        }


    // GET: AuctionController/Delete/5
    public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuctionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult YourBiddenAuctions()
        {
            List<Auction> auctions = _auctionService.GetAllActiveBiddenAuctionsByUser(User.Identity.Name);
            List<AuctionVM> auctionVMs = new();

            foreach (var auction in auctions)
            {
                auctionVMs.Add(AuctionVM.FromAuction(auction));
            }

            return View(auctionVMs);
        }

        public ActionResult YourWonAuctions()
        {
            List<Auction> auctions = _auctionService.GetClosedAuctionsWonByUser(User.Identity.Name);
            List<AuctionVM> auctionVMs = new();

            foreach (var auction in auctions)
            {
                auctionVMs.Add(AuctionVM.FromAuction(auction));
            }

            return View(auctionVMs);
        }
    }


}
