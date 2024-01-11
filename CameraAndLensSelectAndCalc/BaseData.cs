using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraAndLensSelectAndCalc
{
    public class BaseData
    {
        public string? Vendors { get; set; }
        public string? Model { get; set; }
        public string? Interface { get; set; }
    }

    public class CameraShowData : BaseData
    {
        public string? Shutter { get; set; }
        public string? ChipSize { get; set; }
        public string? PixelSize { get; set; }
        public string? Frame { get; set; }
        public string? Color { get; set; }
        public string? Pixels { get; set; }
    }
    public class CameraData : BaseData
    {
        public string? ChipVendors { get; set; }
        public string? Chip { get; set; }
        public string? Shutter { get; set; }
        public string? ChipSize { get; set; }
        public string? ChipType { get; set; }
        public string? ChipWidth { get; set; }
        public string? ChipHeight { get; set; }
        public string? PixelWidth { get; set; }
        public string? PixelHeight { get; set; }
        public string? PixelSize { get; set; }
        public string? Frame { get; set; }
        public string? Color { get; set; }
        public int Pixels { get; set; }
    }

    public class LensData : BaseData
    {
        public string? LensType { get; set; }
        public string? FocalLength { get; set; }
        public string? WorkingDistance { get; set; }
        public string? MatchingChip { get; set; }
        public string? Aperture { get; set; }
        public string? MaxViewAngle { get; set; }
    }
}
