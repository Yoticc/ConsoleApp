﻿using static FuckSharp;
public class ConsoleApp
{
    public ConsoleApp()
    {
        Clear();
    }
    private List<Line> Lines = new List<Line>();
    public Line Now { get => this[CCPY]; set => this[CCPY] = value; }
    public ConsoleApp Clear()
    {
        CC();
        CCPY = 0;
        Lines = new List<Line>();
        return this;
    }
    public ConsoleApp Skip(int count = 1)
    {
        CCPY += count;
        return this;
    }
    public Line Next { get => this[++CCPY]; }
    public ConsoleApp WriteLine(params dynamic[] args)
    {
        this[++CCPY].Write(args);
        return this;
    }
    public Line Update(params dynamic[] args) => Now.Update(args);
    public Line this[int index]
    {
        get
        {
            int f = Lines.FindIndex(z => z.index == index);
            if (f == -1)
            {
                Lines.Add(new(index, this));
                f = Lines.FindIndex(z => z.index == index);
            }
            return Lines[f];
        }
        set
        {
            int f = Lines.FindIndex(z => z.index == index);
            if (f == -1)
            {
                Lines.Add(new(index, this));
                f = Lines.FindIndex(z => z.index == index);
            }
            Lines[f] = value;
        }
    }
    public enum Color
    {
        Green,
        White,
        Gray,
        Black,
        Cyan,
        Blue,
        Red,
        Yellow,
        Magenta,
        DarkBlue,
        DarkCyan,
        DarkGray,
        DarkGreen,
        DarkMagenta,
        DarkRed,
        DarkYellow,
    }
    public static Dictionary<Color, ConsoleColor> ConsoleColors = new Dictionary<Color, ConsoleColor>
    {
        { Color.Green, ConsoleColor.Green },
        { Color.White, ConsoleColor.White },
        { Color.Gray, ConsoleColor.Gray },
        { Color.Black, ConsoleColor.Black },
        { Color.Cyan, ConsoleColor.Cyan },
        { Color.Red, ConsoleColor.Red },
        { Color.Yellow, ConsoleColor.Yellow },
        { Color.Magenta, ConsoleColor.Magenta },
        { Color.DarkBlue, ConsoleColor.DarkBlue },
        { Color.DarkCyan, ConsoleColor.DarkCyan },
        { Color.DarkGray, ConsoleColor.DarkGray },
        { Color.DarkGreen, ConsoleColor.DarkGreen },
        { Color.DarkMagenta, ConsoleColor.DarkMagenta },
        { Color.DarkRed, ConsoleColor.DarkRed },
        { Color.DarkYellow, ConsoleColor.DarkYellow },
    };
    public class Line
    {
        public Line(int index, ConsoleApp parent)
        {
            this.index = index;
            Parent = parent;
        }
        public ConsoleApp Parent { get; private set; }
        public string Content { get; private set; } = "";
        public int index;
        public int xPos = 0;
        public Line Write(params dynamic[] args)
        {
            args.ToList().ForEach(z =>
            {
                if (z is Color)
                {
                    CFC = ConsoleColors[(Color)z];
                }
                else
                {
                    SetCursor();
                    string str = z.ToString();
                    W = str;
                    Content += str;
                    xPos += str.Length;
                }
            });
            return this;
        }
        public string Read(string str = "")
        {
            SetCursor();
            Write(str);
            return RL;
        }
        public ConsoleApp Skip(int count = 1) => Parent.Skip(count);
        public Line SetCursor()
        {
            CCP = (xPos, index);
            return this;
        }
        public string ReadLine(dynamic str)
        {
            Clear();
            CCP = (xPos, index);
            W = str;
            return RL;
        }
        public string ReadLine()
        {
            Clear();
            CCP = (xPos, index);
            return RL;
        }
        public Line Update(params dynamic[] args)
        {
            int oldContentLength = Content.Length;
            Content = "";
            xPos = 0;
            CCP = (0, index);
            args.ToList().ForEach(z =>
            {
                if (z is Color)
                {
                    CFC = ConsoleColors[(Color)z];
                }
                else
                {
                    string str = z.ToString();
                    W = str;
                    Content += str;
                    xPos += str.Length;
                }
            });
            if (oldContentLength - Content.Length > 0)
                W = "".PadRight(oldContentLength - Content.Length, ' ');
            return this;
        }
        public void WriteData()
        {
            W = $"[{DateTime.Now}]  ";
        }
        public Line Clear()
        {
            W = "".PadRight(Content.Length, ' ');
            Content = "";
            xPos = 0;
            return this;
        }
    }
}