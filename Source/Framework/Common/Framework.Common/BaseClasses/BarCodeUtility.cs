using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;

namespace Cedar.Framework.Common.BaseClasses
{
    /// <summary>
    ///     条码类
    /// </summary>
    public class BarCodeUtility
    {
        /// <summary>
        ///     创建二维码(不带Logo)
        /// </summary>
        /// <param name="strBarcodeContent">要生成的二维码的内容(中文460，英文1003)</param>
        /// <param name="intBarCodeWidth">二维码图片的宽度</param>
        /// <param name="intBarCodeHeight">二维码图片的高度</param>
        /// <returns>二维码Bitmap</returns>
        public static Bitmap CreateBarcode(string strBarcodeContent, int intBarCodeWidth, int intBarCodeHeight)
        {
            return Encode(BarcodeFormat.QR_CODE, intBarCodeHeight, intBarCodeWidth, strBarcodeContent);
        }

        /// <summary>
        ///     创建二维码(带Logo)
        /// </summary>
        /// <param name="strBarcodeContent">要生成的二维码的内容(中文460，英文1003)</param>
        /// <param name="strLogoImgPath">二维码Logo图片的路径</param>
        /// <param name="intBarCodeWidth">二维码图片的宽度</param>
        /// <param name="intBarCodeHeight">二维码图片的高度</param>
        /// <param name="intLogoWidth">二维码图片的宽度</param>
        /// <param name="intLogoHeight">二维码图片的高度</param>
        /// <returns>Bitmap[0]:不带Logo的Bitmap流 Bitmap[1]:带Logo的Bitmap流</returns>
        public static Bitmap[] CreateBarcode(string strBarcodeContent, string strLogoImgPath, int intBarCodeWidth,
            int intBarCodeHeight, int intLogoWidth, int intLogoHeight)
        {
            var bitmap = Encode(BarcodeFormat.QR_CODE, intBarCodeHeight, intBarCodeWidth, strBarcodeContent);
            Image imgBarcode = bitmap;
            //Logo图片
            var imgLogo = Image.FromFile(strLogoImgPath);
            var newImg = CombinePic(imgBarcode, imgLogo, intBarCodeWidth, intBarCodeHeight, intLogoWidth, intLogoHeight);
            var newBitmap = new Bitmap(newImg);
            var arrayBitmap = new Bitmap[2];
            //不带Logo的Bitmap流
            arrayBitmap[0] = bitmap;
            //带Logo的Bitmap流
            arrayBitmap[1] = newBitmap;
            return arrayBitmap;
        }

        /// <summary>
        ///     创建二维码
        /// </summary>
        /// <param name="strBarcodeContent">要生成的二维码的内容(中文460，英文1003)</param>
        /// <param name="strBarcodeImgPath">存放二维码的路径</param>
        /// <param name="strLogoImgPath">存放二维码Logo的路径(logo的大小为24*24)</param>
        /// <param name="strBarcodeName">带logo的二维码的名称(以.jpg的格式存储)</param>
        /// <param name="strBarcodeNameNoLogo">不带logo的二维码的名称(以.jpg的格式存储)</param>
        /// <param name="intBarCodeWidth">二维码图片的宽度</param>
        /// <param name="intBarCodeHeight">二维码图片的高度</param>
        /// <param name="intLogoWidth">二维码Logo图片的宽度</param>
        /// <param name="intLogoHeight">二维码Logo图片的高度</param>
        /// <returns>true:创建成功 false:创建失败</returns>
        public static bool CreateBarcode(string strBarcodeContent, string strBarcodeImgPath, string strLogoImgPath,
            string strBarcodeName, string strBarcodeNameNoLogo, int intBarCodeWidth, int intBarCodeHeight,
            int intLogoWidth, int intLogoHeight)
        {
            try
            {
                var strSavePath = strBarcodeImgPath + "/" + strBarcodeName;
                var strSavePathNoLogo = strBarcodeImgPath + "/" + strBarcodeNameNoLogo;

                var bitmap = Encode(BarcodeFormat.QR_CODE, intBarCodeHeight, intBarCodeWidth, strBarcodeContent);
                Image imgBarcode = bitmap;
                var imgLogo = Image.FromFile(strLogoImgPath);
                var newImgBarcodeWithOutLogo = imgBarcode;

                //不带logo的二维码
                newImgBarcodeWithOutLogo.Save(strSavePathNoLogo);

                var newImgBarcode = CombinePic(imgBarcode, imgLogo, intBarCodeWidth, intBarCodeHeight, intLogoWidth,
                    intLogoHeight);

                //带logo的二维码
                newImgBarcode.Save(strSavePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     生成一维码
        /// </summary>
        /// <param name="strBarcodeContent">生成的内容</param>
        /// <param name="intBarCodeWidth">宽度</param>
        /// <param name="intBarCodeHeight">高度</param>
        /// <returns></returns>
        public static Bitmap CreateSingleBarcode(string strBarcodeContent, int intBarCodeWidth, int intBarCodeHeight)
        {
            return Encode(BarcodeFormat.CODE_128, intBarCodeHeight, intBarCodeWidth, strBarcodeContent);
        }

        /// <summary>
        ///     读取二维码信息
        /// </summary>
        /// <param name="strBarcodeImgPath">二维码的存放路径</param>
        /// <returns>二维码信息</returns>
        public static string ReadBarcode(string strBarcodeImgPath)
        {
            var img = Image.FromFile(strBarcodeImgPath);
            var bmap = new Bitmap(img);
            var ms = new MemoryStream();
            bmap.Save(ms, ImageFormat.Bmp);
            var bytes = ms.GetBuffer();
            LuminanceSource source = new RGBLuminanceSource(bytes, bmap.Width, bmap.Height);
            var bitmap = new BinaryBitmap(new HybridBinarizer(source));
            var result = new MultiFormatReader().decode(bitmap);
            return result.Text;
        }

        /// <summary>
        ///     合并二维码和二维码logo
        /// </summary>
        /// <param name="imgBarCode">二维码图片</param>
        /// <param name="imgLogo">二维码logo图片(logo的大小为24*24)</param>
        /// <param name="intBarWidth">二维码宽度</param>
        /// <param name="intBarHeight">二维码高度</param>
        /// <param name="intLogoWidth">intLogoWidth(Int32)</param>
        /// <param name="intLogoHeight">Height of the int logo.</param>
        /// <returns>
        ///     合并了二维码和二维码logo后的二维码
        /// </returns>
        private static Image CombinePic(Image imgBarCode, Image imgLogo, int intBarWidth, int intBarHeight,
            int intLogoWidth, int intLogoHeight)
        {
            //从指定的Image创建新的Graphics        
            var g = Graphics.FromImage(imgBarCode);

            g.DrawImage(imgLogo, (intBarWidth - intLogoWidth)/2, (intBarHeight - intLogoHeight)/2, intLogoWidth,
                intLogoHeight);
            GC.Collect();
            return imgBarCode;
        }

        /// <summary>
        ///     编码
        /// </summary>
        /// <param name="format">编码格式</param>
        /// <param name="height">高</param>
        /// <param name="width">宽</param>
        /// <param name="contents">内容</param>
        /// <returns>编码后图片</returns>
        private static Bitmap Encode(BarcodeFormat format, int height, int width, string contents)
        {
            var writer = new BarcodeWriter
            {
                Format = format,
                Options = new EncodingOptions
                {
                    Height = height,
                    Width = width
                },
                Renderer = (IBarcodeRenderer<Bitmap>) Activator.CreateInstance(typeof (BitmapRenderer))
            };
            return writer.Write(contents);
        }
    }
}