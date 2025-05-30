

namespace Tsi.AutomatedTestRunner
{
    public class TestResult
    {
        #region ctor
        public TestResult(TestStatus testState, 
            string? message, 
            string? stackTrack,
            Exception exception,
            TimeSpan duration)
        {
            Message = message;
            TestState = testState;
            StackTrace = stackTrack;
            Exception = exception;
            Duration = duration;
        }
        public TestResult(TestStatus testState, 
            string? message, 
            TimeSpan duration)
        {
            Message = message;
            TestState = testState;
            Duration = duration;
        }
        #endregion

        #region static methodes
        /// <summary>
        /// Create insatnce from <see cref="TestResult"/> with given parameters
        /// </summary>
        /// <param name="message"></param>
        /// <param name="duration"></param>
        /// <returns><see cref="TestResult"/> with success TestStatus</returns>
        public static TestResult Success(string? message,
            string? stackTrack,
            TimeSpan duration)
        {
            return new TestResult(TestStatus.Success, 
                message,
                duration);
        }

        /// <summary>
        /// Create insatnce from <see cref="TestResult"/> with given parameters
        /// </summary>
        /// <param name="message"></param>
        /// <param name="duration"></param>
        /// <param name="stackTrack"></param>
        /// <returns><see cref="TestResult"/> with warning <see cref="TestState"/></returns>
        public static TestResult Warning(string? message, 
            TimeSpan duration,
            Exception exception,
            string? stackTrack)
        {
            return new TestResult(TestStatus.Warning, 
                message,
                stackTrack, 
                exception,
                duration);
        }
        /// <summary>
        /// Create insatnce from <see cref="TestResult"/> with given parameters
        /// </summary>
        /// <param name="message"></param>
        /// <param name="duration"></param>
        /// <param name="stackTrack"></param>
        /// <returns><see cref="TestResult"/> with Failed <see cref="TestState"/></returns>
        public static TestResult Failed(string? message,
            TimeSpan duration,
            string? stackTrack,
            Exception exception)
        {
            return new TestResult(TestStatus.Failed, 
                message,stackTrack,
                exception,
                duration);
        }
        /// <summary>
        /// Create insatnce from <see cref="TestResult"/> with given parameters
        /// </summary>
        /// <param name="message"></param>
        /// <param name="duration"></param>
        /// <param name="stackTrack"></param>
        /// <returns><see cref="TestResult"/> with Error <see cref="TestState"/></returns>
        public static TestResult Error(string? message, 
            TimeSpan duration,
            Exception exception,
            string? stackTrack)
        {
            return new TestResult(TestStatus.Success, 
                message,
                stackTrack,
                exception,
                duration);
        }

        /// <summary>
        /// Create insatnce from <see cref="TestResult"/> with given parameters
        /// </summary>
        /// <param name="message"></param>
        /// <param name="duration"></param>
        /// <param name="stackTrack"></param>
        /// <returns><see cref="TestResult"/> with Skipped <see cref="TestState"/></returns>
        public static TestResult Skipped(string? message,            
            string? stackTrack,
            Exception exception,
            TimeSpan duration)
        {
            return new TestResult(TestStatus.Skipped,
                message,
                stackTrack,
                exception,
                duration);
        }
        #endregion

        public string? Message { get; set; }
        public TestStatus TestState { get; set; }
        public string? StackTrace { get; set; }
        public Exception Exception { get; }
        public TimeSpan Duration { get; set; }

    }

    public enum TestStatus
    {
        Success = 0,
        Warning = 1,
        Failed = 2,
        Error = 3,
        Skipped = 4,
    }
}
