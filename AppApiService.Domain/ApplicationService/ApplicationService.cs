namespace AppApiService.Domain.ApplicationService;

public class ApplicationService : IApplicationService
{
    public int[] OrderStudentNos(int[] studentNos)
    {
        var studentNoCounts = studentNos.Length;
        var middleCount = studentNoCounts / 2 + studentNoCounts % 2;
        var studentNosByOrdered = studentNos.Order().ToList();
        if (studentNosByOrdered != null)
        {
            var studentNosByNewOrder = new List<int>();
            var studentNosFromHeadToMiddle = studentNosByOrdered.Take(middleCount).ToList();
            var studentNosFromTailToMiddle = studentNosByOrdered.TakeLast(studentNoCounts - middleCount).OrderDescending().ToList();
            for (var i = 0; i < middleCount; i++)
            {
                if (i < studentNosFromHeadToMiddle.Count)
                    studentNosByNewOrder.Add(studentNosFromHeadToMiddle[i]);
                if (i < studentNosFromTailToMiddle.Count)
                    studentNosByNewOrder.Add(studentNosFromTailToMiddle[i]);
            }
            return [.. studentNosByNewOrder];
        }
        else
            throw new Exception($"{nameof(studentNos)} is null !!!!");
    }
}
