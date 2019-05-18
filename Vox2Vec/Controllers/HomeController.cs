using System;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Vox2Vec.Implementation;
using Vox2Vec.Implementation.Python;
using Vox2Vec.Models;
using Vox2Vec.Services;

namespace Vox2Vec.Controllers
{
    public class HomeController : Controller
    {
        private IVoicePipeline voicePipeline;
        private IFeatureRepository featureRepository;

        public HomeController()
        {
            var resoursePath = new ResourcePaths();
            var extractor = new EmbeddingExtractor(resoursePath);
            var voivePreprocessor = new VoicePreprocessor(resoursePath);
            var voicePathSource = new VoicePathSource(resoursePath);
            this.voicePipeline = new VoicePipeline(extractor, voivePreprocessor, voicePathSource);
            this.featureRepository = new InMemoryFeatureRepository();
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
            var s1 = Name; //
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

            }
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