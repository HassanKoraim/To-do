
using Entities;
using ServicesConstract.DTO;

namespace ServicesConstract
{
    public interface IMissionsService
    {
        List<MissionResponse> GetAllMissions();
        MissionResponse AddMission(MissionAddRequest missionAddRequest);
        MissionResponse UpdateMission();
        bool DeleteMission(int Id);
    }
}
