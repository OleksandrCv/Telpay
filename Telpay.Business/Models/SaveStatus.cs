namespace Telpay.Business.Models
{
    public class SaveStatus
    {
        public bool Successful { get; set; }
        public string Message { get; set; }

        public SaveStatus()
        {
            Successful = true;
        }
    }
}
