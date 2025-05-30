
namespace Tsi.Erp.TestTracker.Abstractions.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public static class  AvatarGenerator
    {
        private static List<string> _BackgroundColours = new List<string> { "#FF5733", "#3498db", /* ... */ };

        public static Bitmap  GenerateAvatar(string firstName, string lastName)
        {
            var avatarString = string.Format("{0} {1}", firstName, lastName).ToUpper();
            var randomIndex = new Random().Next(0, _BackgroundColours.Count - 1);
            var bgColour = _BackgroundColours[randomIndex];

            // Create a bitmap and draw the text
            var bmp = new Bitmap(48, 48);
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.Clear(ColorTranslator.FromHtml(bgColour));
                var font = new Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                var sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                graphics.DrawString(avatarString, font, Brushes.White, new RectangleF(0, 0, bmp.Width, bmp.Height), sf);
            }

            return bmp;
        }
    }

}
