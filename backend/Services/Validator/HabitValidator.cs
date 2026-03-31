using System.Diagnostics.CodeAnalysis;
using Dtos.Request.Habit;

namespace Services.Validator;

public static class HabitValidator
{
    public static void CreateHabitValidator(HabitCreateRequest habitCreateRequest)
    {
        if (habitCreateRequest == null)
            throw new Exception("RequestIsNull");

        CheckFields(habitCreateRequest);
    }

    public static void UpdateHabitValidator(HabitUpdateRequest habitUpdateRequest)
    {
        if (habitUpdateRequest == null)
            throw new Exception("RequestIsNull");

        CheckFields(habitUpdateRequest);
    }
    
    private static void CheckFields(HabitCreateRequest habitCreateRequest)
    {
        if (string.IsNullOrEmpty(habitCreateRequest.Title))
            throw new Exception("TitleIsEmpty");
        
        if (habitCreateRequest.Title.Length < 3 ||  habitCreateRequest.Title.Length > 100)
            throw new Exception("TitleMustBeBetween3And100Characters");

        if (!string.IsNullOrEmpty(habitCreateRequest.Description))
        {
            // TODO
        }
    }

    private static void CheckFields(HabitUpdateRequest habitUpdateRequest)
    {
        if (string.IsNullOrEmpty(habitUpdateRequest.Title))
            throw new Exception("TitleIsEmpty");

        if (habitUpdateRequest.Title.Length < 3 || habitUpdateRequest.Title.Length > 100)
            throw new Exception("TitleMustBeBetween3And100Characters");

        if (!string.IsNullOrEmpty(habitUpdateRequest.Description))
        {
            // TODO
        }
    }
}
