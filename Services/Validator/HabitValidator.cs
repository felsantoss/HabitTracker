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
    
    private static void CheckFields(HabitCreateRequest habitCreateRequest)
    {
        if (string.IsNullOrEmpty(habitCreateRequest.Title))
            throw new Exception("TitleIsEmpty");
        
        if (habitCreateRequest.Title.Length < 3 ||  habitCreateRequest.Title.Length > 100)
            throw new Exception("TitleMustBeBetween3And100Characters");

        if (!string.IsNullOrEmpty(habitCreateRequest.Description))
        {
            
        }
    }
}