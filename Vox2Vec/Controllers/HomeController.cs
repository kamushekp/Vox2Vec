using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Vox2Vec.DistanceProviders;
using Vox2Vec.Implementation;
using Vox2Vec.Implementation.Python;
using Vox2Vec.Models;
using Vox2Vec.Services;

namespace Vox2Vec.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVoicePipeline voicePipeline;
        private readonly IFeatureRepository featureRepository;

        public HomeController(IVoicePipeline voicePipeline, IFeatureRepository featureRepository)
        {
            this.voicePipeline = voicePipeline;
            this.featureRepository = featureRepository;
        }

        public ActionResult DragFilesInvitation(UserInfo userInfo)
        {
            return this.View();
        }

        public ActionResult AuthorizeInvitation()
        {
            return this.View();
        }

        public ActionResult Authorize(string Name)
        {
            this.KeepUserInfo(new UserInfo {UserName = Name});
            return this.RedirectToAction("DragFilesInvitation");
        }

        public ActionResult DragFilesProcessing()
        {
            foreach (string fileName in this.Request.Files)
            {
                var file = this.Request.Files[fileName];

                if (file == null || file.ContentLength <= 0) continue;

                var path = this.SaveFile(file);
                var embedding = this.voicePipeline.Extract(path);
                this.featureRepository.AddVoiceVecAsync(embedding, this.GetUserInfo());
                var nearestUsers =
                    this.featureRepository.GetNearestNeighbors(embedding, 5, new CosineDistanceProvider()).GetAwaiter().GetResult();
                this.KeepNearestUsers(nearestUsers);
            }

            return this.RedirectToAction("NearestUsersTable");
        }

        public ActionResult NearestUsersTable()
        {
            var nearestUsersList = this.GetNearestUsers();
            return this.View(nearestUsersList);
        }

        private void KeepNearestUsers(NearestUser[] nearestUsers)
        {
            this.TempData["nearestUsers"] = nearestUsers;
        }

        private NearestUser[] GetNearestUsers()
        {
            return (NearestUser[])this.TempData["nearestUsers"];
        }

        private void KeepUserInfo(UserInfo userInfo)
        {
            this.TempData["userInfo"] = userInfo;
        }

        private UserInfo GetUserInfo()
        {
            return (UserInfo)this.TempData["userInfo"];
        }

        private string SaveFile(HttpPostedFileBase file)
        {
            var filename = Guid.NewGuid().ToString();
            var originalDirectory =
                new DirectoryInfo($"{this.Server.MapPath(@"\")}Images\\uploaded");
            var pathString = Path.Combine(originalDirectory.ToString(), "imagepath");
            var isExists = Directory.Exists(pathString);
            if (!isExists)
            {
                Directory.CreateDirectory(pathString);
            }

            var path = $"{pathString}\\{filename}";
            file.SaveAs(path);

            return path;
        }
    }
}