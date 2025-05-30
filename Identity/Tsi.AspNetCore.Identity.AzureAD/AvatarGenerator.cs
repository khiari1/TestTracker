
namespace Tsi.AspNetCore.Identity.AzureAD;

using System;
using System.Collections.Generic;
using System.Drawing;

public static class AvatarGenerator
{
    private static List<Brush> _BackgroundColours = new List<Brush>
    {
        Brushes.BlueViolet, Brushes.Green, Brushes.DarkRed,Brushes.Purple,Brushes.Turquoise,Brushes.Violet,Brushes.Magenta,
    };
    private static List<string> _BackgroundColoursHex = new List<string>
    {
        "#4547a9","#0b655b","#465161","#8f48d2","#b32b23","#822854","#816204"
    };

    public static Bitmap GenerateAvatar(string avatarString)
    {
        var randomIndex = new Random().Next(0, _BackgroundColoursHex.Count - 1);
        var hexColor = _BackgroundColoursHex[randomIndex];

        // Create a bitmap and draw the text
        var bmp = new Bitmap(32, 32);
        using (var graphics = Graphics.FromImage(bmp))
        {
            graphics.Clear(ColorTranslator.FromHtml(hexColor));

            var font = new Font("Segoe UI VSS (Regular)", 8f, FontStyle.Regular);
            var sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            graphics.DrawString(avatarString, font, Brushes.White, new RectangleF(0, 0, bmp.Width, bmp.Height), sf);

        }
        bmp.SetResolution(298, 298);


        return bmp;
    }
}


