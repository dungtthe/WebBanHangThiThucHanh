using Newtonsoft.Json;
using System.Text;
using WebBanHang.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebBanHang.Helpers
{
    public static class Utils
    {
        public static string AddPhotoForProduct(string fileName, string imgPre)
        {
            string[] s = JsonConvert.DeserializeObject<string[]>(imgPre);
            List<string> images = new List<string>();

            if (!string.IsNullOrEmpty(fileName))
            {
                images.Add(fileName);
            }

            foreach (var item in s)
            {
                if (!item.Equals("no_img.png"))
                {
                    images.Add(item);
                }
            }

            images.Add("no_img.png");

            return JsonConvert.SerializeObject(images);
        }


        public static string RemovePhotoForProduct(string fileName, string imgPre)
        {
            string[] s = JsonConvert.DeserializeObject<string[]>(imgPre);
            List<string> images = new List<string>(s);

            images.Remove(fileName);

            if (images.Count == 0)
            {
                images.Add("no_img.png");
            }

            return JsonConvert.SerializeObject(images);
        }

        public static bool CheckTonTaiUserNameAndEmail(string username, string email, WebDbContext context)
        {
            return context.AppUsers.Any(user => user.UserName == username || user.Email == email);
        }

    }
}
