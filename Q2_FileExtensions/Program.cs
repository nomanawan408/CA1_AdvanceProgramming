using System;
using System.Collections.Generic;

namespace Q2_FileExtensions;

class Program
{
    static void Main()
    {
        Dictionary<string, string> extensions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {".mp4", "MPEG-4 video file"},
            {".mov", "Apple QuickTime movie"},
            {".avi", "Audio Video Interleave file"},
            {".mkv", "Matroska video file"},
            {".webm", "WebM video file"},
            {".mp3", "MP3 audio file"},
            {".wav", "Waveform audio file"},
            {".flac", "Free Lossless Audio Codec file"},
            {".txt", "Plain text file"},
            {".pdf", "Portable Document Format"},
            {".docx", "Microsoft Word document"},
            {".xlsx", "Microsoft Excel spreadsheet"},
            {".pptx", "Microsoft PowerPoint presentation"},
            {".jpg", "JPEG image"},
            {".png", "Portable Network Graphics image"},
            {".gif", "Graphics Interchange Format image"},
            {".zip", "Compressed archive"},
            {".rar", "WinRAR archive"},
            {".html", "HyperText Markup Language file"},
            {".css", "Cascading Style Sheets file"}
        };

        while (true)
        {
            Console.Write("Enter a file extension (or 0 to exit): ");
            string? input = Console.ReadLine();

            if (input == "0")
            {
                break;
            }

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Please enter something.");
                continue;
            }

            if (!input.StartsWith("."))
            {
                input = "." + input;
            }

            if (extensions.TryGetValue(input, out string? description))
            {
                Console.WriteLine($"Extension {input}: {description}");
            }
            else
            {
                Console.WriteLine("I don't know this extension. Please try another one.");
            }
        }
    }
}
