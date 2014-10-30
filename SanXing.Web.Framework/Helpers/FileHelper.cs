using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace SanXing.Web.Framework.Helpers
{

    public class UploadResult
    {
        public string err { get; set; }
        public string url { get; set; }
        public string status { get; set; }
        public string url_120 { get; set; }
        public string url_800 { get; set; }
    }


    public class FileHelper
    {
        public static UploadResult UpLoadSave(IEnumerable<HttpPostedFileBase> attachments, string status = "upload")
        {
            int uploadmaxsize = 10240000;
            var res = new UploadResult();
            foreach (var file in attachments)
            {
                string folder = System.Configuration.ConfigurationManager.AppSettings["FileIntMarketPath"].ToString();
                folder += DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";
                int uploadMaxLength = 10240000;
                if (uploadmaxsize != 10240000)
                {
                    uploadMaxLength = uploadmaxsize;
                }
                string fileName = Guid.NewGuid().ToString();
                string directory = HttpContext.Current.Server.MapPath(string.Format("~{0}",
                    folder));
                string extension = ".JPG";
                //System.IO.Path.GetExtension(file.FileName).ToLower();
                string filePath = GetImgSaveUrl(directory, fileName, extension);
                string saveUrl = GetImgSaveUrl(folder, fileName, extension);

                if (file.ContentLength < uploadMaxLength)
                {
                    if (!System.IO.Directory.Exists(directory))
                    {
                        System.IO.Directory.CreateDirectory(directory);
                    }

                    file.SaveAs(filePath);
                    Crop(filePath, 120);
                    //Crop(filePath, 430);
                    Crop(filePath, 800);
                    //Crop(filePath, 960);

                    res.err = string.Empty;
                    res.url = saveUrl;
                    res.status = status;
                    res.url_120 = GetImgSaveUrl(folder, fileName, extension, 120);
                    res.url_800 = GetImgSaveUrl(folder, fileName, extension, 800);

                }
                else
                {
                    res.err = string.Format("{{\"err\":\"上传文件大小不能超过{0}K\"}}", uploadMaxLength / 1000);
                }
            }
            return res;
        }


        public static UploadResult UpLoadFile(IEnumerable<HttpPostedFileBase> attachments, string status = "upload")
        {
            int uploadmaxsize = 10240000;
            var res = new UploadResult();
            foreach (var file in attachments)
            {
                string folder = System.Configuration.ConfigurationManager.AppSettings["FileIntMarketPath"].ToString();
                folder += DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";
                int uploadMaxLength = 10240000;
                if (uploadmaxsize != 10240000)
                {
                    uploadMaxLength = uploadmaxsize;
                }
                string fileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                string directory = HttpContext.Current.Server.MapPath(string.Format("~{0}", folder));
                string extension = System.IO.Path.GetExtension(file.FileName).ToLower();

                string filePath = GetImgSaveUrl(directory, fileName, extension);
                string saveUrl = GetImgSaveUrl(folder, fileName, extension);

                if (file.ContentLength < uploadMaxLength)
                {
                    if (!System.IO.Directory.Exists(directory))
                    {
                        System.IO.Directory.CreateDirectory(directory);
                    }
                    file.SaveAs(filePath);

                    res.url = saveUrl;
                    res.status = status;

                }
                else
                {
                    res.err = string.Format("{{\"err\":\"上传文件大小不能超过{0}K\"}}", uploadMaxLength / 1000);
                }
            }
            return res;
        }

        public static string GetImgSaveUrl(string folder, string filename, string extension)
        {
            return string.Format("{0}{1}{2}",
                folder,
                filename,
                extension
                );
        }

        public static string GetImgSaveUrl(string folder, string filename, string extension, int targerwidth)
        {
            var url = GetImgSaveUrl(folder, filename, extension);
            return url.Substring(0, url.LastIndexOf('.')) + "_" + targerwidth + extension;
        }


        public static string GetImgCutpath(string imgPath)
        {
            return imgPath.Substring(0, imgPath.LastIndexOf('.')) + "_small.jpg";
        }

        public static string GetImgCutpath(string imgPath, int tarwidth)
        {
            return imgPath.Substring(0, imgPath.LastIndexOf('.')) + "_" + tarwidth + ".jpg";
        }

        public static string GetGUIDFolder(string guid)
        {
            string folder = string.Format("{0}/{1}/{2}/",
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day);

            return folder;
        }

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public static void Resize(String source, String destination, Int32 targetWidth)
        {
            byte[] resizedImage = ResizeSourceImage(source, targetWidth);

            SaveToDestination(destination, resizedImage);
        }

        public static byte[] ResizeSourceImage(String path, Int32 targetWidth)
        {
            byte[] resized = null;


            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {

                byte[] buffer = new byte[fs.Length];

                fs.Read(buffer, 0, Convert.ToInt32(fs.Length));

                resized = ResizeBinary(buffer, targetWidth);
            }

            return resized;
        }

        public static void SaveToDestination(String path, byte[] resized)
        {
            using (MemoryStream ms = new MemoryStream(resized))
            {
                using (Image resizedImage = Image.FromStream(ms))
                {
                    resizedImage.Save(path);
                }
            }
        }


        public static byte[] ResizeBinary(byte[] imageFile, int targetSize)
        {
            using (MemoryStream tempStream = new MemoryStream(imageFile))
            {
                using (Image oldImage = Image.FromStream(tempStream))
                {
                    Size newSize = CalculateNewDimensions(oldImage.Size, targetSize);
                    using (Bitmap newImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format32bppArgb))
                    {
                        using (Graphics canvas = Graphics.FromImage(newImage))
                        {
                            canvas.SmoothingMode = SmoothingMode.AntiAlias;
                            canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            canvas.DrawImage(oldImage, new Rectangle(new Point(0, 0), newSize));
                            using (MemoryStream m = new MemoryStream())
                            {
                                newImage.Save(m, ImageFormat.Jpeg);
                                return m.GetBuffer();
                            }
                        }
                    }
                }
            }
        }

        public static Size CalculateNewDimensions(Size oldSize, int targetSize)
        {
            Size newSize = new Size();
            if (oldSize.Height > oldSize.Width)
            {
                //Image is taller than it is wide. Determine new width as a function of the 
                //old size's width combined with the target size's ratio to the old size's height.
                newSize.Width = (int)(oldSize.Width * ((float)targetSize / (float)oldSize.Height));
                newSize.Height = targetSize;
            }
            else
            {
                //Image is wider than it is tall. Determine new height as a function of the 
                //old size's height combined with the target size's ratio to the old size's width.
                newSize.Width = targetSize;
                newSize.Height = (int)(oldSize.Height * ((float)targetSize / (float)oldSize.Width));
            }
            return newSize;
        }

        public static bool Crop(string imgPath, int width, int height, int x, int y, int targetwidth)
        {

            bool success = false;
            try
            {
                Crop(imgPath, 300);
                string imgfilePath = GetImgCutpath(imgPath, 300);
                Image image = Image.FromFile(imgfilePath);
                Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                bmp.SetResolution(80, 60);
                Graphics gfx = Graphics.FromImage(bmp);
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gfx.DrawImage(image, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
                // Dispose to free up resources
                ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
                Encoder myEncoder = Encoder.Quality;
                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 32L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp.Save(GetImgCutpath(imgPath), jgpEncoder,
                    myEncoderParameters);
                //bmp.Save(GetImgCutpath(imgPath), ImageFormat.Jpeg);
                image.Dispose();
                bmp.Dispose();
                gfx.Dispose();
                if (true)
                {
                    Resize(GetImgCutpath(imgPath), GetImgCutpath(imgPath, targetwidth), targetwidth);
                    deleteImg(imgPath, 300);
                    deleteImg(imgPath, 120);
                    deleteImg(imgPath, 430);
                    deleteImg(imgPath, 800);
                    deleteImg(imgPath, 960);
                    deleteImg(GetImgCutpath(imgPath));
                }
                success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;
        }
        public static bool Crop(string imgPath, int targetwidth)
        {

            bool success = false;
            try
            {

                Image image = Image.FromFile(imgPath);
                int width = image.Width;
                int height = image.Height;
                int x = 0, y = 0;
                Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                bmp.SetResolution(80, 60);
                Graphics gfx = Graphics.FromImage(bmp);
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gfx.DrawImage(image, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
                // Dispose to free up resources
                ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
                Encoder myEncoder = Encoder.Quality;
                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 32L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp.Save(GetImgCutpath(imgPath), jgpEncoder,
                    myEncoderParameters);
                //bmp.Save(GetImgCutpath(imgPath), ImageFormat.Jpeg);
                image.Dispose();
                bmp.Dispose();
                gfx.Dispose();
                if (true)
                {
                    Resize(imgPath, GetImgCutpath(imgPath, targetwidth), targetwidth);
                }
                success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;
        }

        public static void deleteImg(string imgPath, int width)
        {
            string imgfilePath = GetImgCutpath(imgPath, width);
            deleteImg(imgfilePath);
        }

        public static void deleteImg(string imgfilePath)
        {
            File.Delete(imgfilePath);
        }
    }
}
