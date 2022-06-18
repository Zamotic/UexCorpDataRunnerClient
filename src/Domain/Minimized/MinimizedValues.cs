using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.Minimized;
public static class MinimizedValues
{
    public const int MinimizedWidth = 38;
    public const int MinimizedHeight = 78;

    private const int DefaultMaximizedWidth = 425;
    private const int DefaultMaximizedHeight = 700;

    public static int _MaximizedWidth = 0;
    public static int MaximizedWidth 
    {
        get => _MaximizedWidth <= 0 ? DefaultMaximizedWidth : _MaximizedWidth; 
        set => _MaximizedWidth = value; 
    }

    public static int _MaximizedHeight = 0;
    public static int MaximizedHeight 
    { 
        get => _MaximizedHeight <= 0 ? DefaultMaximizedHeight : _MaximizedHeight; 
        set => _MaximizedHeight = value; 
    }
}
