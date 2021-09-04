
namespace ItalTech.Models.TableToExport
{
    public class Title
    {
        public string Caption { get; set; }
        public Align Align { get; set; } = Align.Center;
        public bool IsBold { get; set; } = true;
    }
}
