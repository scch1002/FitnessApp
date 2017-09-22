﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorkOut.App.Forms.ViewModel.Interface
{
    public interface IAddWorkoutDefinitionViewModel
    {
        IWorkoutDefinitionViewModel NewWorkoutDefinitionViewModel { get; set; }

        ICommand AddWorkoutDefinition { get; }
    }
}
