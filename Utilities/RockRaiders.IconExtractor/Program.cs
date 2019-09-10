using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace RockRaiders.IconExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var path = @"C:\Users\Rune\Source\Repos\RockRaiders.Unity\RockRaiders\Assets\Resources\Textures\Interface\Menus";
            var outPath = @"C:\Users\Rune\Source\Repos\RockRaiders.Unity\RockRaiders\Assets\Resources\Textures\Interface\Menus\Clipped";
            var maskPath = @"C:\Users\Rune\Source\Repos\RockRaiders.Unity\RockRaiders\Assets\Resources\Textures\Interface\Menus\Shared\mask.png";
            
            var files = Directory.GetFiles(path).Where(file => file.EndsWith(".png", StringComparison.OrdinalIgnoreCase));

            var maskImage = Image.Load<Rgba32>(maskPath);

            foreach (var file in files)
            {
                if (Path.GetFileName(file).StartsWith("P")) continue;
                if (Path.GetFileName(file).StartsWith("N")) continue;
                if (Path.GetFileName(file).StartsWith("MSG")) continue;
                using (var srcImage = Image.Load<Rgba32>(file))
                {
                    if (srcImage.Size() != maskImage.Size()) continue;
                    using (var targetImage = new Image<Rgba32>(srcImage.Width, srcImage.Height))
                    {
                        for (var x = 0; x < srcImage.Width; x++)
                        {
                            for (var y = 0; y < srcImage.Height; y++)
                            {
                                if (maskImage[x, y].A != 0)
                                {
                                    targetImage[x, y] = srcImage[x, y];
                                }
                            }
                        }

                        targetImage.Save(Path.Combine(outPath, Path.GetFileName(file)));
                    }
                }
            }

            maskImage.Dispose();
        }
    }
}
