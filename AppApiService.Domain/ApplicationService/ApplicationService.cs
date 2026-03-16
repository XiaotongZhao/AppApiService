namespace AppApiService.Domain.ApplicationService;

public class ApplicationService : IApplicationService
{
    public int[] OrderStudentNos(int[] studentNos)
    {
        if (studentNos != null)
        {
            var studentNosByOrdered = studentNos.Order().ToArray();
            var studentNoCounts = studentNosByOrdered.Count();
            var studentNosByNewOrder = new int[studentNoCounts];
            var middleCount = studentNoCounts / 2 + studentNoCounts % 2;
            var lastIndex = studentNoCounts - 1;
            var index = 0;
            for (var i = 0; i < middleCount; i++)
            {
                studentNosByNewOrder[index] = studentNosByOrdered[i];
                if (lastIndex >= middleCount)
                    studentNosByNewOrder[++index] = studentNosByOrdered[lastIndex];
                ++index;
                lastIndex--;
            }
            return studentNosByNewOrder;
        }
        else
            throw new Exception($"{nameof(studentNos)} is null !!!!");
    }
}
