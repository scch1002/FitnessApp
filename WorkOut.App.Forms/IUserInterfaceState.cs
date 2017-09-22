using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOut.App.Forms
{
    public interface IUserInterfaceState
    {
        App Application { get; set; }
        void ChangeUserInterfaceState(UserInterfaceStates newState, object paramater = null);
    }

    public enum UserInterfaceStates
    {
        Main,
        ScheduleView,
        ScheduleAdd,
        SessionDefinitionView,
        AssignWorkoutDefinition,
        SelectNextSession,
        SessionView,
        WorkoutDefinitionLibraryView,
        WorkoutView,
        SetView,
        AddWorkoutDefinition,
        ViewWorkoutDefinition
    }
}
