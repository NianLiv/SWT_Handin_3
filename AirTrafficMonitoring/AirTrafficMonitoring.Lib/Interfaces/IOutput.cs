namespace AirTrafficMonitoring.Lib.Interfaces
{
    public interface IOutput
    {
       void OutputLine(string line);
       void SetCursorPosition(int left, int top);
       void SetWindowSize(int width, int height);
       void GetLargetsScreenSize(out int width, out int height);
    } 
}
