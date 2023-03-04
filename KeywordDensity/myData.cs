
namespace KeywordDensity
{
    class myData
    {
        public string Keyword { get; set; }
        public int Frequency { get; set; }
        public float Density { get; set; }

        public myData (string keyword, int frequency, float density)
        {
            Keyword = keyword;
            Frequency = frequency;
            Density = density;
        }
    }
}
