using System.Text.Json.Serialization;

namespace AssignmentManagement.Contracts.Assignments;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AssignmentStateType
{
    Completed,
    InProgress,
}