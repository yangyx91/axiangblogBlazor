using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class FaceImg_Response
    {
        public string faceId { get; set; }

        public FaceRectangle faceRectangle { get; set; }

        public FaceAttributes faceAttributes { get; set; }
    }


    public class FaceAttributes
    {
        public decimal smile { get; set; }

        public string gender { get; set; }

        public int age { get; set; }

        public FaceEmotion emotion { get; set; }

        public FaceHair hair { get; set; }
    }

    public class FaceRectangle
    {
        public int top { get; set; }

        public int left { get; set; }

        public int width { get; set; }

        public int height { get; set; }

    }

    public class FaceEmotion
    {
        public decimal anger { get; set; }

        public decimal contempt { get; set; }

        public decimal disgust { get; set; }

        public decimal fear { get; set; }

        public decimal happiness { get; set; }

        public decimal neutral { get; set; }

        public decimal sadness { get; set; }

        public decimal surprise { get; set; }
    }

    public class FaceHair
    {
        public decimal bald { get; set; }

        public bool invisible { get; set; }

        public List<FaceHairColor> hairColor { get; set; }
    }

    public class FaceHairColor
    {
        public string color { get; set; }

        public decimal confidence { get; set; }
    }
}
