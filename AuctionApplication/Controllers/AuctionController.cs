using AuctionApplication.Core;
using AuctionApplication.Core.Interfaces;
using AuctionApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        // GET: AuctionController
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
        }

        // GET: AuctionController/Details/5
        public ActionResult Details(int id)
        {
            //TODO: make sure the details page shows the details of the right auction
           // programm will crash if the id given below isnt valid, no auction object will be found in that case
            Auction auction = _auctionService.GetAuctionById(-1);
            AuctionDetailsVM detailsVM = AuctionDetailsVM.FromAuction(auction);
            
            return View(detailsVM);
        }
        
        // GET: AuctionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuctionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAuctionVM vm)
        {
            //TODO: creation of auction isnt adding auction to database
            if (ModelState.IsValid)
            {
                Auction auction = new Auction()
                {
                    Name = vm.Name, 
                    Owner = User.Identity.Name,
                    StartingPrice = vm.StartingPrice,
                    Deadline = new DateTime(2023,11,15),
                    Description = vm.Description,
                    AuctionStatus = Status.ON_GOING
                   
                    
                };
                _auctionService.CreateAuction(auction);
                return RedirectToAction("Index");
            }
            return View(vm);
        }
     
        public ActionResult PlaceBid()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceBid(PlaceBidVm vm)
        {
            if (ModelState.IsValid)
            {
                Bid b = new Bid()
                {
                    Amount = vm.Amount

                };
                _auctionService.PlaceBid(b);
                return RedirectToAction("Index");
            }
            return View(vm);

        }

        // GET: AuctionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuctionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
    }
}
