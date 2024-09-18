namespace TaskManagment.AppServices
{
    public class ResultDto
    {

        public bool Success { get; set; }

        public string[] Errors { get; set; } = [];



        public static ResultDto Ok { get { return new ResultDto() { Success = true }; } }
    }
}
