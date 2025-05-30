namespace Tsi.Erp.TestTracker.Api.Controllers.Dto.Request
{
    public class RecurringJobModel
    {
        public RecurringJobModel(int hour, int minute, DayOfWeek dayOfWeek,int dayOfMonth, int month, string jobId, RecuringMode recuringMode)
        {
            Hour = hour;
            Minute = minute;
            DayOfWeek = dayOfWeek;
            Month = month;
            JobId = jobId;            
            RecuringMode = recuringMode;
        }
        
        public string JobId  {get; set;}
        public int Hour { get; set; }
        public int Minute {get; set;}
        public DayOfWeek DayOfWeek {get; set;}
        public int Month {get; set;}
       
        public int DayOfMonth { get; set;}
        public RecuringMode RecuringMode {get; set;}

        public int[] MonitoringIds { get; set;}
    }

    public class ScheduleJobModel
    {
        public string JobId { get; set;}
        public DateTime Date { get; set;}

        public int[] MonitoringIds { get; set; }
    }

    public enum JobMode
    {
        Schedule,
        Recurring,
    }
    public enum RecuringMode
    {
        Hourly,
        Daily,
        Weekly,
        Monthly
    }
}
