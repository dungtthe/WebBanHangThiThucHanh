using Newtonsoft.Json;
using System.Text;
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

    }
}
