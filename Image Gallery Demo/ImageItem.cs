using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Gallery_Demo
{
    public class ImageItem
    {
        public string Id;
        public string Name;
        public byte[] Base64;
        public string Format;

        public string Id1 { get => Id; set => Id = value; }
        public string Name1 { get => Name; set => Name = value; }
        public byte[] Base641 { get => Base64; set => Base64 = value; }
        public string Format1 { get => Format; set => Format = value; }
    }
}
